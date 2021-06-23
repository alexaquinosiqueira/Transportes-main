using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transportadora.Business.Interfaces;
using Transportadora.Business.Models;
using Transportadora.Data.Context;

namespace Transportadora.Data.Repository
{
    public class ExpenseRepository : Repository<Expense>, IExpenseRepository
    {
        public ExpenseRepository(TruckDbContext context) : base(context) { }

        public Task<IEnumerable<Expense>> GetByCompanyId(Guid companyId)
        {
            return Search(x => x.Company_Id == companyId);
        }
    }
}
