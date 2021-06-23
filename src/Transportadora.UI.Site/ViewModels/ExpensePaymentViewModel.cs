using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Transportadora.UI.Site.ViewModels
{
    public class ExpensePaymentViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid Expense_Id { get; set; }
        public virtual ExpenseViewModel Expense { get; set; }
        public decimal AmountExpensePayment { get; set; }
        public DateTime? ConcludedDate { get; set; }
        public StatusViewModel StatusExpensePayment { get; set; }
        public DateTime DueDateExpensePayment { get; set; }
    }
}
