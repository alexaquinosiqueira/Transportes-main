using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transportadora.Business.Models;

namespace Transportadora.Business.Interfaces
{
    public interface IBankRepository : IRepository<Bank>
    {
        Task<IEnumerable<Bank>> GetByCompanyId(Guid companyId);
    }
}
