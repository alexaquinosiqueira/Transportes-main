using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transportadora.Business.Models;

namespace Transportadora.Business.Interfaces
{
    public interface IExpenseSupplierRepository : IRepository<ExpenseSupplier>
    {
        Task<IEnumerable<ExpenseSupplier>> GetByExpenseSupplier_Id(Guid ExpenseSupplier_Id);
        Task<IEnumerable<ExpenseSupplier>> GetBySupplier_Id(Guid Supplier_Id);
    }
}
