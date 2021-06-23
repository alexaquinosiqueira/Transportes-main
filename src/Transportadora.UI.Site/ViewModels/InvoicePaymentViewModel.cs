using System;
using System.Collections.Generic;
using System.Text;

namespace Transportadora.UI.Site.ViewModels
{
    public class InvoicePaymentViewModel
    {
        public Guid Id { get; set; }
        public Guid Invoice_Id { get; set; }
        public InvoiceViewModel Invoice { get; set; }
        public decimal AmountInvoicePayment { get; set; }
        public DateTime? ConcludedDate { get; set; }
        public StatusViewModel StatusInvoicePayment { get; set; }
        public DateTime DueDateInvoicePayment { get; set; }
    }
}
