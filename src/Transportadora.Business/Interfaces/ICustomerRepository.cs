using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transportadora.Business.Models;

namespace Transportadora.Business.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<IEnumerable<Customer>> GetByCompanyId(Guid companyId);
    }
}
