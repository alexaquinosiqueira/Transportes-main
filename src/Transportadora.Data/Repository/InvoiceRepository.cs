using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transportadora.Business.Interfaces;
using Transportadora.Business.Models;
using Transportadora.Data.Context;

namespace Transportadora.Data.Repository
{
    public class InvoiceRepository : Repository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(TruckDbContext context) : base(context) { }

        public Task<IEnumerable<Invoice>> GetByCompanyId(Guid companyId)
        {
            return Search(x => x.Company_Id == companyId);
        }

        public Task<IEnumerable<Invoice>> GetByDocumento(string documento)
        {
            return Search(x => x.Description.Contains(documento));
        }

    }
}
