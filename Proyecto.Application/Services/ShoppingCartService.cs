using System;
using Proyecto.Application.IServices;
using Proyecto.Domain.Models;
using Proyecto.Domain.Repositories;
using Proyecto.Application.Models.Dtos;

namespace Proyecto.Application.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IProductoRepository _productoRepository;
        private readonly IUserRepository _userRepository;

        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, IProductoRepository productoRepository, IUserRepository userRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _productoRepository = productoRepository;
            _userRepository = userRepository;
        }

        public ShoppingCartDto GetShoppingCartByClientName(string clientName)
        {
            try
            {
                var client = _userRepository.GetByName(clientName);
                if (client == null)
                {
                    throw new InvalidOperationException($"No se encontro un cliente con el nombre {clientName}");
                }

                var shoppingCart = _shoppingCartRepository.GetShoppingCartByClientId(client.Id);
                if (shoppingCart == null)
                {
                    throw new InvalidOperationException($"No se encontro un carrito de compras para el cliente con ID {client.Id}");
                }

                return new ShoppingCartDto
                {
                    IdShoppingCart = shoppingCart.IdShoppingCart,
                    Impuesto = shoppingCart.Impuesto,
                    ClientId = shoppingCart.ClientId,
                    Productos = shoppingCart.Productos.Select(p => p.Descripcion).ToList(),
                    Subtotal = shoppingCart.Subtotal,
                    Client = new UserDto
                    {
                        Name = client.Name,
                        Email = client.Email,
                    }
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el carrito de compras", ex);
            }
        }

        public ShoppingCart GetShoppingCartById(Guid shoppingCartId)
        {
            return _shoppingCartRepository.GetShoppingCartById(shoppingCartId);
        }

        public bool AddProductoToCart(string clientName, Guid productId)
        {
            try
            {
                var client = _userRepository.GetByName(clientName);
                if (client == null)
                {
                    Console.WriteLine($"Cliente no encontrado: {clientName}");
                    return false;
                }

                var shoppingCart = _shoppingCartRepository.GetShoppingCartByClientId(client.Id);
                if (shoppingCart == null)
                {
                    Console.WriteLine($"Carrito no encontrado para el cliente: {client.Id}");
                    return false;
                }

                var product = _productoRepository.GetById(productId);
                if (product == null)
                {
                    Console.WriteLine($"Producto no encontrado o inactivo: {productId}");
                    return false;
                }

                if (shoppingCart.Productos.Any(p => p.Id == productId))
                {
                    Console.WriteLine($"El producto {productId} ya está en el carrito.");
                    return false;
                }

                shoppingCart.Productos.Add(product);
                _shoppingCartRepository.UpdateShoppingCart(shoppingCart);
                Console.WriteLine($"Producto {productId} agregado al carrito del cliente {clientName}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar producto al carrito: {ex.Message}");
                throw new Exception("Error al agregar el producto al carrito", ex);
            }
        }

        public bool RemoveProductoFromCart(string clientName, Guid productId)
        {
            try
            {
                var client = _userRepository.GetByName(clientName);
                if (client == null) return false;

                var shoppingCart = _shoppingCartRepository.GetShoppingCartByClientId(client.Id);
                var product = _productoRepository.GetById(productId);
                if (shoppingCart == null || product == null) return false;

                shoppingCart.Productos.Remove(product);
                _shoppingCartRepository.UpdateShoppingCart(shoppingCart);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el producto del carrito", ex);
            }
        }
    }
}
