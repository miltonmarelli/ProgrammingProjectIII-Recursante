using Microsoft.AspNetCore.Mvc;
using Proyecto.Application.IServices;
using Proyecto.Infraestructure.Services;
using Proyecto.Application.Models.Request;

namespace ProyectoProgIII.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IConfiguration _config;
        private readonly ICustomAuthenticationService _customAuthenticationService;

        public AuthenticationController(IConfiguration config, ICustomAuthenticationService autenticacionService)
        {
            _config = config; 
            _customAuthenticationService = autenticacionService;
        }

        [HttpPost("authenticate")]
        public ActionResult<string> Autenticar(AuthenticationRequest authenticationRequest)
        {
            string token = _customAuthenticationService.Autenticar(authenticationRequest); 

            return Ok(token);
        }
    }
}
