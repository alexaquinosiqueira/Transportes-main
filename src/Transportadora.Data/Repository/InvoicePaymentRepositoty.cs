using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transportadora.Business.Interfaces;
using Transportadora.Business.Models;
using Transportadora.Data.Context;

namespace Transportadora.Data.Repository
{
    public class InvoicePaymentRepository : Repository<InvoicePayment>, IInvoicePaymentRepository
    {
        public InvoicePaymentRepository(TruckDbContext context) : base(context) { }

        public Task<IEnumerable<InvoicePayment>> GetInvoicePaymentClosedByCompanyId(Guid companyId)
        {
            return Search(p => p.StatusInvoicePayment == Status.closed && p.Invoice.Company_Id == companyId);
        }
    }
}
