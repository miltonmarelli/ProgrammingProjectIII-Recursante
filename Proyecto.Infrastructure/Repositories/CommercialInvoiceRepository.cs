using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Proyecto.Domain.Repositories;
using Proyecto.Domain.Models;
using Proyecto.Infraestructure.Context;

namespace Proyecto.Infraestructure.Repositories
{
    public class CommercialInvoiceRepository : ICommercialInvoiceRepository
    {
        private readonly ProyectoDbContext _context;

        public CommercialInvoiceRepository(ProyectoDbContext context)
        {
            _context = context;
        }

        public void AddInvoice(CommercialInvoice invoice)
        {
            _context.CommercialInvoices.Add(invoice);
            _context.SaveChanges();
        }

        public CommercialInvoice GetInvoiceByClientId(int clientId)
        {
            var shoppingCart = _context.ShoppingCarts.FirstOrDefault(sc => sc.ClientId == clientId);
            if (shoppingCart == null)
            {
                throw new Exception("Carrito de compras no encontrado para el cliente.");
            }

            return _context.CommercialInvoices.FirstOrDefault(ci => ci.ShoppingCartId == shoppingCart.IdShoppingCart);
        }

    }
}
