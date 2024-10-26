using System;
using Proyecto.Domain.Models;

namespace Proyecto.Domain.Repositories
{

    public interface ICommercialInvoiceRepository
    {
        void AddInvoice(CommercialInvoice invoice);
        CommercialInvoice GetInvoiceByClientId(int clientId);
    }
}