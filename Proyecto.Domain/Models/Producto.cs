using System.ComponentModel.DataAnnotations;

namespace Proyecto.Domain.Models
{
    public class Producto
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(50)]
        public string Marca { get; set; }
        [Required]
        [MaxLength(100)]
        public string Descripcion { get; set; }
        public double PrecioUnitario { get; set; }
        public double Descuento { get; set; }  = 0;
        public int? Stock { get; set; }
        public bool Activo { get; set; }

        public string Image { get; set; }
        public List<ShoppingCart> ShoppingCarts { get; set; }  

        public Producto()
        {
            ShoppingCarts = new List<ShoppingCart>();
        }
    }
}
