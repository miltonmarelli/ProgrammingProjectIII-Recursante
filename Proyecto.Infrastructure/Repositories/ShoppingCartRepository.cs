using System;
using System.Linq;
using Proyecto.Domain.Repositories;
using Proyecto.Domain.Models;
using Proyecto.Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Proyecto.Infraestructure.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ProyectoDbContext _context;

        public ShoppingCartRepository(ProyectoDbContext context)
        {
            _context = context;
        }

        public ShoppingCart GetShoppingCartByClientId(int clientId)
        {
            try
            {
                var shoppingCart = _context.ShoppingCarts
                    .Include(sc => sc.Productos)
                    .FirstOrDefault(sc => sc.ClientId == clientId);

                if (shoppingCart == null)
                {
                    throw new ArgumentException($"No se encontro carrito de compras para el cliente con ID {clientId}");
                }

                return shoppingCart;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el carrito de compras del cliente con ID {clientId}", ex);
            }
        }

        public ShoppingCart GetShoppingCartById(Guid shoppingCartId)
        {
            try
            {
                var shoppingCart = _context.ShoppingCarts
                    .Include(sc => sc.Productos)
                    .FirstOrDefault(sc => sc.IdShoppingCart == shoppingCartId);

                if (shoppingCart == null)
                {
                    throw new ArgumentException($"No se encontro carrito de compras con ID {shoppingCartId}");
                }

                return shoppingCart;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el carrito de compras con ID {shoppingCartId}", ex);
            }
        }

        public bool UpdateShoppingCart(ShoppingCart shoppingCart)
        {
            try
            {
                var existingCart = _context.ShoppingCarts.FirstOrDefault(sc => sc.IdShoppingCart == shoppingCart.IdShoppingCart);

                if (existingCart == null)
                {
                    throw new ArgumentException($"El carrito de compras con ID {shoppingCart.IdShoppingCart} no existe.");
                }

                existingCart.Productos = shoppingCart.Productos;
                existingCart.Impuesto = shoppingCart.Impuesto;

                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el carrito de compras", ex);
            }
        }
    }
}
