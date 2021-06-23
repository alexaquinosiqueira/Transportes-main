using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transportadora.Business.Models;

namespace Transportadora.Business.Interfaces
{
    public interface IExpenseRepository : IRepository<Expense>
    {
        Task<IEnumerable<Expense>> GetByCompanyId(Guid companyId);
    }
}
