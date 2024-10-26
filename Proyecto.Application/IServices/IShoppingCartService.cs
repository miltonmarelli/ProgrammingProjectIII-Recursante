using Proyecto.Application.Models.Dtos;
using Proyecto.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Application.IServices
{
    public interface IShoppingCartService
    {
        ShoppingCartDto GetShoppingCartByClientName(string clientName);

        ShoppingCart GetShoppingCartById(Guid shoppingCartId);
        bool AddProductoToCart(string clientName, Guid productId);
        bool RemoveProductoFromCart(string clientName, Guid productId);
    }
}

