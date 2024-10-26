using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proyecto.Domain.Models
{

    public class CommercialInvoice
    {
        [Key]
        public Guid IdOrden { get; set; }
        public DateTime FechaEmision { get; set; }

        public string ClientName { get; set; }
        public string ClientEmail { get; set; }

        public Guid ShoppingCartId { get; set; }

        public double Total { get; set; }

        public CommercialInvoice()
        {
            IdOrden = Guid.NewGuid();
        }
    }
}

