using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transportadora.Business.Models;

namespace Transportadora.Business.Interfaces
{
    public interface IResponsibilityRepository : IRepository<Responsibility>
    {
        Task<IEnumerable<Responsibility>> GetByCompanyId(Guid companyId);
    }
}
