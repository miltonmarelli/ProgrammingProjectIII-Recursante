using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto.Application.IServices;
using Proyecto.Application.Models.Request;

namespace Proyecto.Web.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DevController : ControllerBase
    {
        private readonly IUserService _userService;

        public DevController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetAllDev")]
        public IActionResult GetAllDev()
        {
            try
            {
                var devs = _userService.GetAllDevs();
                return Ok(devs);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los desarrolladores: " + ex.Message);
            }
        }

        [HttpGet("GetDevByName")]
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
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener el desarrollador: " + ex.Message);
            }
        }

        [HttpGet("GetDevById/{id}")]
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
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener el desarrollador: " + ex.Message);
            }
        }

        [HttpPost("CreateDev")]
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
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear el desarrollador: " + ex.Message);
            }
        }

        [HttpPut("UpdateDev/{id}")]
        public IActionResult UpdateUser(int id, UserSaveRequest user)
        {
            try
            {
                var updatedUser = _userService.UpdateUser(id, user);
                return Ok(updatedUser);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar el desarrollador: " + ex.Message);
            }
        }

        [HttpDelete("DeleteDev/{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                _userService.DeleteUser(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar el desarrollador: " + ex.Message);
            }
        }
    }
}