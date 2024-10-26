using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Application.Models.Dtos
{
    public class CreateProductDto
    {
        public string Marca { get; set; }
        public string Descripcion { get; set; }
        public double PrecioUnitario { get; set; }
        public double Descuento { get; set; }
        public int Stock { get; set; }
        public string Image { get; set; }
    }
}
