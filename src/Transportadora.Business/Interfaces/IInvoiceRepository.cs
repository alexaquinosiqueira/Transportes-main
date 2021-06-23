using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transportadora.Business.Models;

namespace Transportadora.Business.Interfaces
{
    public interface IInvoiceRepository : IRepository<Invoice>
    {
        Task<IEnumerable<Invoice>> GetByCompanyId(Guid companyId);
        Task<IEnumerable<Invoice>> GetByDocumento(string documento);
    }
}
