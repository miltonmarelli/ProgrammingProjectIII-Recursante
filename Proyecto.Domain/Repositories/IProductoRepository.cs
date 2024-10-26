using Proyecto.Domain.Models;

namespace Proyecto.Domain.Repositories
{
    public interface IProductoRepository
    {
        IEnumerable<Producto?> GetProductList();
        Producto? GetById(Guid id);
        bool CreateProduct(Producto producto);
        bool UpdateProduct(Producto producto);
        bool DeleteProduct(Guid id);
        bool ApplyDiscount(Guid id, double percentage);
    }
}
