using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Application.Models.Dtos
{
    public class ShoppingCartDto
    {
        public Guid IdShoppingCart { get; set; }
        public double Impuesto { get; set; }
        public int ClientId { get; set; }
        public UserDto Client { get; set; }
        public List<string> Productos { get; set; }
        public double Subtotal { get; set; }
    }
}
