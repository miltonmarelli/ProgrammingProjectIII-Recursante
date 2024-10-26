using Proyecto.Application.Models.Request;
using Proyecto.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace Proyecto.Application.Models.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}