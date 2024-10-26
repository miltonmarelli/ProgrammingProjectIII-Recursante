using Microsoft.AspNetCore.Mvc;
using Proyecto.Application.IServices;
using Proyecto.Domain.Models;
using System;
using System.Collections.Generic;
using Proyecto.Application.Models.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace ProyectoProgIII.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet("GetProductList")]
        public ActionResult<IEnumerable<Producto?>> GetProductList()
        {
            try
            {
                var productList = _productoService.GetProductList();
                return Ok(productList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener la lista de productos: " + ex.Message);
            }
        }

        [HttpGet("GetProductById/{id}")]
        public ActionResult<Producto?> GetById(Guid id)
        {
            try
            {
                var product = _productoService.GetById(id);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener el producto: " + ex.Message);
            }
        }
        
        [HttpPost("CreateProduct")]
        public ActionResult CreateProduct(CreateProductDto productDto)
        {
            try
            {
                var producto = new Producto
                {
                    Id = Guid.NewGuid(),
                    Marca = productDto.Marca,
                    Descripcion = productDto.Descripcion,
                    PrecioUnitario = productDto.PrecioUnitario,
                    Descuento = productDto.Descuento,
                    Stock = productDto.Stock,
                    Activo = true, 
                    Image = productDto.Image
                };

                var success = _productoService.CreateProduct(producto);
                if (!success)
                {
                    return Conflict("Ya existe un producto con el mismo ID");
                }

                return CreatedAtAction(nameof(GetById), new { id = producto.Id }, producto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear el producto: " + ex.Message);
            }
        }
        
        [HttpPut("UpdateProduct/{id}")]
        public ActionResult UpdateProduct(Guid id, Producto producto)
        {
            try
            {
                if (id != producto.Id)
                {
                    return BadRequest("ID de producto no valido");
                }
                var success = _productoService.UpdateProduct(producto);
                if (!success)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar el producto: " + ex.Message);
            }
        }
       
        [HttpDelete("DeleteProduct/{id}")]
        public ActionResult DeleteProduct(Guid id)
        {
            try
            {
                var success = _productoService.DeleteProduct(id);
                if (!success)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar el producto: " + ex.Message);
            }
        }
        
        [HttpPost("ApplyDiscount/{id}")]
        public ActionResult ApplyDiscount(Guid id, double percentage)
        {
            try
            {
                var success = _productoService.ApplyDiscount(id, percentage);
                if (!success)
                {
                    return NotFound();
                }
                return Ok("El descuento se aplico correctamente");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al aplicar descuento: " + ex.Message);
            }
        }
    }
}