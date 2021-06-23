using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transportadora.Business.Models;

namespace Transportadora.Business.Interfaces
{
    public interface IExpenseTypeRepository : IRepository<ExpenseType>
    {
        Task<IEnumerable<ExpenseType>> GetByCompanyId(Guid companyId);
    }
}
