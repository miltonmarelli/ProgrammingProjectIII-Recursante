using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Domain.Models
{
    public class ShoppingCart
    {
        [Key]
        public Guid IdShoppingCart { get; set; }
        public double Impuesto { get; set; } = 0.21;
        public Client Client { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientEmail { get; set; }

        public List<Producto> Productos { get; set; }

        public double Subtotal => CalcularSubtotal();

        public ShoppingCart()
        {
            IdShoppingCart = Guid.NewGuid();
            Productos = new List<Producto>();
        }

        private double CalcularSubtotal()
        {
            double total = 0;
            foreach (var producto in Productos)
            {
                total += producto.PrecioUnitario;
            }
            return total;
        }
    }
}