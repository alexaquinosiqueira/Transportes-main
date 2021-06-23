using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transportadora.Business.Models;

namespace Transportadora.Business.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<IEnumerable<Employee>> GetByCompanyId(Guid companyId);
    }
}
