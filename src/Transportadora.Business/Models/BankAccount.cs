using System;
using System.Collections.Generic;

namespace Transportadora.Business.Models
{
    public class BankAccount : Entity
    {
        public string Description { get; set; }
        public string Branch { get; set; }
        public string Account { get; set; }
        public decimal Overdraft { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal CurrentBalance { get; set; }
        public Guid Bank_Id { get; set; }
        public virtual Bank Bank { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<Expense> Expenses { get; set; }
        public Guid Company_Id { get; set; }
        public virtual Company Company { get; set; }
    }
}
