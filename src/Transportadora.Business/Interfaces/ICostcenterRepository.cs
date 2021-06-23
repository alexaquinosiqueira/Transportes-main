using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transportadora.Business.Models;

namespace Transportadora.Business.Interfaces
{
    public interface ICostcenterRepository : IRepository<CostCenter>
    {
        Task<IEnumerable<CostCenter>> GetByCompanyId(Guid companyId);
    }
}
