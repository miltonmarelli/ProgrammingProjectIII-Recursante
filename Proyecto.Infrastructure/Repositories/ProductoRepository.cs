
using Proyecto.Domain.Repositories;
using Proyecto.Domain.Models;
using Proyecto.Infraestructure.Context;

namespace Proyecto.Infraestructure.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly ProyectoDbContext _context;

        public ProductoRepository(ProyectoDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Producto?> GetProductList()
        {
            try
            {
                return _context.Productos.Where(p => p.Activo).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de productos", ex);
            }
        }

        public Producto? GetById(Guid id)
        {
            try
            {
                var product = _context.Productos.FirstOrDefault(x => x.Id == id);
                if (product == null)
                {
                    Console.WriteLine($"Producto con ID {id} no encontrado.");
                }
                else if (!product.Activo)
                {
                    Console.WriteLine($"Producto con ID {id} esta inactivo.");
                }
                return product?.Activo == true ? product : null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el producto: {ex.Message}");
                throw new Exception("Error al obtener el producto", ex);
            }
        }

        public bool CreateProduct(Producto producto)
        {
            try
            {
                _context.Productos.Add(producto);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el producto", ex);
            }
        }

        public bool UpdateProduct(Producto producto)
        {
            var prod = _context.Productos.FirstOrDefault(x => x.Id == producto.Id && x.Activo);
            if (prod == null)
            {
                throw new ArgumentException("El producto no existe o no esta activo");
            }

            try
            {
                prod.Descripcion = producto.Descripcion;
                prod.Stock = producto.Stock;
                prod.PrecioUnitario = producto.PrecioUnitario;
                prod.Marca = producto.Marca;
                prod.Descuento = producto.Descuento;
                prod.Image = producto.Image;

                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el producto", ex);
            }
        }

        public bool DeleteProduct(Guid id)
        {
            try
            {
                var prod = _context.Productos.FirstOrDefault(x => x.Id == id && x.Activo);

                if (prod == null)
                {
                    return false;
                }

                prod.Activo = false;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el producto", ex);
            }
        }

        public bool ApplyDiscount(Guid id, double percentage)
        {
            try
            {
                var product = _context.Productos.FirstOrDefault(x => x.Id == id && x.Activo);

                if (product == null)
                {
                    return false;
                }

                product.PrecioUnitario *= (1 - (percentage / 100));

                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al aplicar el descuento", ex);
            }
        }
    }
}
