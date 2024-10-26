using System;
using Proyecto.Domain.Models;

namespace Proyecto.Domain.Repositories
{
    public interface IShoppingCartRepository
    {
        ShoppingCart GetShoppingCartByClientId(int clientId);
        bool UpdateShoppingCart(ShoppingCart shoppingCart);
        ShoppingCart GetShoppingCartById(Guid shoppingCartId);

    }
}

