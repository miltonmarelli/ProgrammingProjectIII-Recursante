using Proyecto.Domain.Models;

namespace Proyecto.Application.IServices
{
    public interface IProductoService
    {
        IEnumerable<Producto?> GetProductList();
        Producto? GetById(Guid id);
        bool CreateProduct(Producto producto);
        bool UpdateProduct(Producto producto);
        bool DeleteProduct(Guid id);
        bool ApplyDiscount(Guid id, double percentage);
    }
}
