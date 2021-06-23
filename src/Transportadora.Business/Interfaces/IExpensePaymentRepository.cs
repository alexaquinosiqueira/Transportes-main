using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transportadora.Business.Models;

namespace Transportadora.Business.Interfaces
{
    public interface IExpensePaymentRepository : IRepository<ExpensePayment>
    {
        Task<IEnumerable<ExpensePayment>> GetExpensePaymentClosedByCompanyId(Guid companyId);
    }
}
