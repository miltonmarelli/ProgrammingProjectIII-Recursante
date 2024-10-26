using Proyecto.Application.IServices;
using Proyecto.Domain.Repositories;
using Proyecto.Domain.Models;

namespace Proyecto.Application.Services
{
    public class CommercialInvoiceService : ICommercialInvoiceService
    {
        private readonly ICommercialInvoiceRepository _commercialInvoiceRepository;
        private readonly IUserRepository _userRepository;
        private readonly IShoppingCartService _shoppingCartService;

        public CommercialInvoiceService(
            ICommercialInvoiceRepository commercialInvoiceRepository,
            IUserRepository userRepository,
            IShoppingCartService shoppingCartService)
        {
            _commercialInvoiceRepository = commercialInvoiceRepository;
            _userRepository = userRepository;
            _shoppingCartService = shoppingCartService;
        }

        public CommercialInvoice CreateCommercialInvoiceByClientName(string clientName)
        {
            var shoppingCart = _shoppingCartService.GetShoppingCartByClientName(clientName);

            if (shoppingCart == null)
            {
                throw new Exception("Carrito de compras no encontrado para el cliente");
            }

            
            var invoice = new CommercialInvoice
            {
                ClientName = shoppingCart.Client.Name,
                ClientEmail = shoppingCart.Client.Email,
                FechaEmision = DateTime.UtcNow,
                ShoppingCartId = shoppingCart.IdShoppingCart,
                Total = shoppingCart.Subtotal
            };

            _commercialInvoiceRepository.AddInvoice(invoice);
            return invoice;
        }

        public CommercialInvoice GetInvoiceByClientName(string clientName)
        {
            var client = _userRepository.GetByName(clientName);
            if (client == null)
            {
                throw new Exception("Cliente no encontrado");
            }

            var invoice = _commercialInvoiceRepository.GetInvoiceByClientId(client.Id);
            if (invoice == null)
            {
                throw new Exception("Factura no encontrada para el cliente especificado");
            }

            return invoice;
        }
    }
}