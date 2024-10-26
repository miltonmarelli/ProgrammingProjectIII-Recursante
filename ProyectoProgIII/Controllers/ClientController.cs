using Microsoft.AspNetCore.Mvc;
using Proyecto.Application.Services;
using Proyecto.Application.Models;
using Proyecto.Application.IServices;
using Proyecto.Application.Models.Request;
using Proyecto.Application.Models.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace Proyecto.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IUserService _userService;

        public ClientController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetAllClient")]
        public IActionResult GetAllClient()
        {
            try
            {
                var clients = _userService.GetAllClients();
                return Ok(clients);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los clientes: " + ex.Message);
            }
        }
       
        [HttpGet("GetClientByName")]
        public IActionResult GetByName(string name)
        {
            try
            {
                var user = _userService.GetByName(name);
                if (user == null)
                {
                    return NotFound($"Usuario con nombre '{name}' no encontrado");
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener el cliente: " + ex.Message);
            }
        }

        [HttpGet("GetClientById/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var user = _userService.GetById(id);
                if (user == null)
                {
                    return NotFound($"Usuario con ID '{id}' no encontrado");
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener el cliente por ID: " + ex.Message);
            }
        }

        [HttpPost("CreateClient")]
        public IActionResult Create(UserSaveRequest user)
        {
            try
            {
                var newUser = _userService.Create(user);
                return Ok(newUser.Name);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear el cliente: " + ex.Message);
            }
        }

        [HttpPut("UpdateClient/{id}")]
        public IActionResult UpdateUser(int id, UserSaveRequest user)
        {
            try
            {
                var updatedUser = _userService.UpdateUser(id, user);
                return Ok(updatedUser);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar el cliente: " + ex.Message);
            }
        }

        [HttpDelete("DeleteClient/{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                _userService.DeleteUser(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar el cliente: " + ex.Message);
            }
        }
    }
}