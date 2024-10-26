using Proyecto.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Application.IServices
{
    public interface ICommercialInvoiceService
    {
        CommercialInvoice CreateCommercialInvoiceByClientName(string clientName);
        CommercialInvoice GetInvoiceByClientName(string clientName);
    }
}

