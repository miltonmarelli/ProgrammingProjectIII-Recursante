using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Domain.Models
{
    public class Client : User
    {
        public Client()
        {
            Role = "client";
        }

        public Guid ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }

        public List<CommercialInvoice> CommercialInvoices { get; set; }
    }
}
