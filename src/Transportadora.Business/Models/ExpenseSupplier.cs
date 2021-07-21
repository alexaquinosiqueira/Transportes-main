using System;
using System.Collections.Generic;

namespace Transportadora.Business.Models
{
    public class ExpenseSupplier : Entity
    {
        public Guid ExpenseType_Id { get; set; }
        public virtual ExpenseType ExpenseType { get; set; }

        public Guid Supplier_Id { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
