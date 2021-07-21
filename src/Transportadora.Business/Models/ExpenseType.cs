using System;
using System.Collections.Generic;

namespace Transportadora.Business.Models
{
    public class ExpenseType : Entity
    {
        public string Description { get; set; }
        public bool Active { get; set; }
        public string Segmento { get; set; }
        public Guid Company_Id { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<ExpenseFinancialSettlement> ExpenseFinancialSettlements { get; set; }
        public virtual ICollection<ExpenseSupplier> ExpenseSupplier { get; set; }

    }
}
