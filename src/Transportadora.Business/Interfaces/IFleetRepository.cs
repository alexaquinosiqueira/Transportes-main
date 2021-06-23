using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transportadora.Business.Models;

namespace Transportadora.Business.Interfaces
{
    public interface IFleetRepository : IRepository<Fleet>
    {
        Task<IEnumerable<Fleet>> GetByCompanyId(Guid companyId);
    }
}
