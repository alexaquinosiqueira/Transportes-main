using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transportadora.Business.Interfaces;
using Transportadora.Business.Models;
using Transportadora.Data.Context;

namespace Transportadora.Data.Repository
{
    public class ExpenseSupplierRepository : Repository<ExpenseSupplier>, IExpenseSupplierRepository
    {
        public ExpenseSupplierRepository(TruckDbContext context) : base(context) { }
        public Task<IEnumerable<ExpenseSupplier>> GetByExpenseSupplier_Id(Guid ExpenseSupplier_Id)
        {
            return Search(x => x.Id == ExpenseSupplier_Id);
        }
        public Task<IEnumerable<ExpenseSupplier>> GetBySupplier_Id(Guid Supplier_Id)
        {
            return Search(x => x.Supplier_Id == Supplier_Id);
        }

    }
}
