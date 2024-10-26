using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Proyecto.Domain.Repositories;
using Proyecto.Domain.Models;
using Proyecto.Application.Models.Request;
using Proyecto.Application.IServices;

namespace Proyecto.Infraestructure.Services
{
    public class AutenticacionService : ICustomAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly AutenticacionServiceOptions _options;

        public AutenticacionService(IUserRepository userRepository, IOptions<AutenticacionServiceOptions> options)
        {
            _userRepository = userRepository;
            _options = options.Value;
        }

        private User? ValidateUser(AuthenticationRequest authenticationRequest)
        {
            if (string.IsNullOrEmpty(authenticationRequest.UserName) || string.IsNullOrEmpty(authenticationRequest.Password))
                return null;

            var user = _userRepository.GetByName(authenticationRequest.UserName);

            if (user == null)
                return null;

            switch (user.Role.ToLower())
            {
                case "admin":
                    user = new Admin
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Password = user.Password 
                    };
                    break;

                case "dev":
                    user = new Dev
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Password = user.Password
                    };
                    break;

                case "client":
                    user = new Client
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Password = user.Password
                    };
                    break;

                default:
                    return null; 
            }

            if (user.Password != authenticationRequest.Password)
                return null;

            return user;
        }

        public string Autenticar(AuthenticationRequest authenticationRequest)
        {
            var user = ValidateUser(authenticationRequest);
            if (user == null)
            {
                return "Credenciales no validas";
            }

            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.SecretForKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim("given_name", user.Name),
                new Claim("id", user.Id.ToString()),
                new Claim("role", user.Role)
            };

            var token = new JwtSecurityToken(
                _options.Issuer,
                _options.Audience,
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                credentials);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }

    }

    public class AutenticacionServiceOptions
    {
        public const string AutenticacionService = "AutenticacionService";
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public string? SecretForKey { get; set; }
    }
}
