using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transportadora.Business.Models;

namespace Transportadora.Business.Interfaces
{
    public interface IInsuranceRepository : IRepository<Insurance>
    {
        Task<IEnumerable<Insurance>> GetByCompanyId(Guid companyId);
    }
}
