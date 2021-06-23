using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transportadora.Business.Interfaces;
using Transportadora.Business.Models;
using Transportadora.Data.Context;

namespace Transportadora.Data.Repository
{
    public class ExpenseTypeRepository : Repository<ExpenseType>, IExpenseTypeRepository
    {
        public ExpenseTypeRepository(TruckDbContext context) : base(context) { }

        public Task<IEnumerable<ExpenseType>> GetByCompanyId(Guid companyId)
        {
            return Search(x => x.Company_Id == companyId);
        }
    }
}
