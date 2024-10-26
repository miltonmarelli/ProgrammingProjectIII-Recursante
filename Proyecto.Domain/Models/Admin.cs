using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Domain.Models
{
    public class Admin : User
    {
        public Admin()
        {
            Role = "admin";
        }
    }
}
