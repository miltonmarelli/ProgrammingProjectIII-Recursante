using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto.Application.IServices;
using Proyecto.Domain.Models;
using System;

namespace ProyectoProgIII.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        [HttpGet("GetShoppingCart/{clientName}")]
        public ActionResult GetShoppingCart(string clientName)
        {
            try
            {
                var shoppingCart = _shoppingCartService.GetShoppingCartByClientName(clientName);
                if (shoppingCart == null || shoppingCart.Productos == null || !shoppingCart.Productos.Any())
                {
                    return Ok($"El cliente {clientName} no tiene productos en su carrito.");
                }
                return Ok(shoppingCart);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener el carrito de compras: " + ex.Message);
            }
        }
      
        [HttpPost("AddProductToCart/{clientName}/{productId}")]
        public ActionResult AddProductToCart(string clientName, Guid productId)
        {
            try
            {
                var success = _shoppingCartService.AddProductoToCart(clientName, productId);
                if (!success)
                {
                    return NotFound("Producto o carrito de compras no encontrado");
                }
                return Ok("Producto agregado al carrito exitosamente");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al agregar producto al carrito: " + ex.Message);
            }
        }
        
        
        [HttpPost("RemoveProductFromCart/{clientName}/{productId}")]
        public ActionResult RemoveProductFromCart(string clientName, Guid productId)
        {
            try
            {
                var success = _shoppingCartService.RemoveProductoFromCart(clientName, productId);
                if (!success)
                {
                    return NotFound("Producto o carrito de compras no encontrado");
                }
                return Ok("Producto removido del carrito exitosamente");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al remover producto del carrito: " + ex.Message);
            }
        }
    }
}