using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transportadora.Business.Interfaces;
using Transportadora.Business.Models;
using Transportadora.Data.Context;

namespace Transportadora.Data.Repository
{
    public class ExpensePaymentRepository : Repository<ExpensePayment>, IExpensePaymentRepository
    {
        public ExpensePaymentRepository(TruckDbContext context) : base(context) { }

        public Task<IEnumerable<ExpensePayment>> GetExpensePaymentClosedByCompanyId(Guid companyId)
        {
            return Search(p => p.StatusExpensePayment == Status.closed && p.Expense.Company_Id == companyId);
        }
    }
}
