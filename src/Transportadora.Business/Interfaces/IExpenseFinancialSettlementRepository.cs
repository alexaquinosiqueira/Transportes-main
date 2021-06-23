using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transportadora.Business.Models;

namespace Transportadora.Business.Interfaces
{
    public interface IExpenseFinancialSettlementRepository : IRepository<ExpenseFinancialSettlement>
    {
        Task<IEnumerable<ExpenseFinancialSettlement>> GetByFinancialSettlement_Id(Guid FinancialSettlement_Id);
    }
}
