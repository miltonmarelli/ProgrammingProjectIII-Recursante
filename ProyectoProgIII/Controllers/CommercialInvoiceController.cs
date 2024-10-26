using Microsoft.AspNetCore.Mvc;
using Proyecto.Application.IServices;
using System.Threading.Tasks;

namespace Proyecto.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommercialInvoiceController : ControllerBase
    {
        private readonly ICommercialInvoiceService _commercialInvoiceService;

        public CommercialInvoiceController(ICommercialInvoiceService commercialInvoiceService)
        {
            _commercialInvoiceService = commercialInvoiceService;
        }

        [HttpPost("Create/{clientName}")]
        public IActionResult CreateInvoice(string clientName)
        {
            try
            {
                var invoice = _commercialInvoiceService.CreateCommercialInvoiceByClientName(clientName);
                return Ok(invoice);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        
        [HttpGet("{clientName}")]
        public IActionResult GetInvoice(string clientName)
        {
            try
            {
                var invoice = _commercialInvoiceService.GetInvoiceByClientName(clientName);
                if (invoice == null)
                {
                    return NotFound(new { message = "Factura no encontrada para el cliente especificado" });
                }
                return Ok(invoice);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}