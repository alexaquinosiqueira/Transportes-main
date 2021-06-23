using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transportadora.Business.Interfaces;
using Transportadora.Business.Models;
using Transportadora.Data.Context;

namespace Transportadora.Data.Repository
{
    public class ExpenseFinancialSettlementRepository : Repository<ExpenseFinancialSettlement>, IExpenseFinancialSettlementRepository
    {
        public ExpenseFinancialSettlementRepository(TruckDbContext context) : base(context) { }
        public Task<IEnumerable<ExpenseFinancialSettlement>> GetByFinancialSettlement_Id(Guid FinancialSettlement_Id)
        {
            return Search(x => x.FinancialSettlement_Id == FinancialSettlement_Id);
        }

    }
}
