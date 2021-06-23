using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transportadora.Business.Models;

namespace Transportadora.Business.Interfaces
{
    public interface IParameterRepository : IRepository<Parameter>
    {
        Task<IEnumerable<Parameter>> GetByCompanyId(Guid companyId);
    }
}
