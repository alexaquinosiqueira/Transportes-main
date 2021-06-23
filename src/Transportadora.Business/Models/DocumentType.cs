using System;
using System.Collections.Generic;

namespace Transportadora.Business.Models
{
    public class DocumentType : Entity
    {
        public string Description { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<Expense> Expenses { get; set; }
        public Guid Company_Id { get; set; }
        public virtual Company Company { get; set; }
    }
}
