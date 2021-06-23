using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transportadora.Business.Models;

namespace Transportadora.Business.Interfaces
{
    public interface IInvoicePaymentRepository : IRepository<InvoicePayment>
    {
        Task<IEnumerable<InvoicePayment>> GetInvoicePaymentClosedByCompanyId(Guid companyId);
    }
}
