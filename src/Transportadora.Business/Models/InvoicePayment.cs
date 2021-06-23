using System;

namespace Transportadora.Business.Models
{
    public class InvoicePayment : Entity
    {
        public Guid Invoice_Id { get; set; }
        public virtual Invoice Invoice { get; set; }
        public decimal AmountInvoicePayment { get; set; }
        public DateTime? ConcludedDate { get; set; }
        public Status StatusInvoicePayment { get; set; }
        public DateTime DueDateInvoicePayment { get; set; }
    }
}
