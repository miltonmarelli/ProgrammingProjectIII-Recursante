using Microsoft.AspNetCore.Mvc;
using Proyecto.Application.Services;
using Proyecto.Application.Models;
using Proyecto.Application.IServices;
using Proyecto.Application.Models.Request;
using Proyecto.Application.Models.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace Proyecto.Web.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }

        
        [HttpGet("GetAllAdmin")]
        public IActionResult GetAllAdmins()
        {
            try
            {
                var admins = _userService.GetAllAdmins();
                return Ok(admins);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los administradores: " + ex.Message);
            }
        }
        
        [HttpGet("GetAdminByName")]
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
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener el administrador: " + ex.Message);
            }
        }
        
        [HttpGet("GetAdminById/{id}")]
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
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener el administrador por ID: " + ex.Message);
            }
        }
        
        [HttpPost("CreateAdmin")]
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
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear el administrador: " + ex.Message);
            }
        }

        [HttpPut("UpdateAdmin/{id}")]
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
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar el administrador: " + ex.Message);
            }
        }

        [HttpDelete("DeleteAdmin/{id}")]
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
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar el administrador: " + ex.Message);
            }
        }
    }
}