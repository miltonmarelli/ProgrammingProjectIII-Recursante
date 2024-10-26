using Proyecto.Application.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Application.IServices
{
    public interface ICustomAuthenticationService
    {
        string Autenticar(AuthenticationRequest authenticationRequest);
    }
}
