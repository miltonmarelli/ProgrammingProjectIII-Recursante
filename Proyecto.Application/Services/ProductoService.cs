
using Proyecto.Domain.Models;
using Proyecto.Application.IServices;
using Proyecto.Domain.Repositories;
namespace Proyecto.Application.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _repository;

        public ProductoService(IProductoRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Producto?> GetProductList()
        {
            return _repository.GetProductList();
        }

        public Producto? GetById(Guid id)
        {
            return _repository.GetById(id);
        }

        public bool CreateProduct(Producto producto)
        {
            try
            {
                return _repository.CreateProduct(producto);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el producto", ex);
            }
        }

        public bool UpdateProduct(Producto producto)
        {
            try
            {
                var existingProduct = _repository.GetById(producto.Id);
                if (existingProduct == null) return false;

                existingProduct.Marca = producto.Marca;
                existingProduct.Descripcion = producto.Descripcion;
                existingProduct.PrecioUnitario = producto.PrecioUnitario;
                existingProduct.Stock = producto.Stock;

                return _repository.UpdateProduct(existingProduct);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el producto con ID {producto.Id}", ex);
            }
        }

        public bool DeleteProduct(Guid id)
        {
            try
            {
                var product = _repository.GetById(id);
                if (product == null) return false;

                return _repository.DeleteProduct(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar el producto con ID {id}", ex);
            }
        }

        public bool ApplyDiscount(Guid id, double percentage)
        {
            try
            {
                var product = _repository.GetById(id);
                if (product == null) return false;

                product.PrecioUnitario *= (1 - (percentage / 100));
                return _repository.UpdateProduct(product);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al aplicar el descuento al producto con ID {id}", ex);
            }
        }
    }
}