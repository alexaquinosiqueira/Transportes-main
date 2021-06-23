using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transportadora.Business.Models;

namespace Transportadora.Business.Interfaces
{
    public interface IBankAccountRepository : IRepository<BankAccount>
    {
        Task<IEnumerable<BankAccount>> GetByCompanyId(Guid companyId);
    }
}
