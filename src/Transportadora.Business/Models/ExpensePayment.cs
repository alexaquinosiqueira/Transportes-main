using System;

namespace Transportadora.Business.Models
{
    public class ExpensePayment : Entity
    {
        public Guid Expense_Id { get; set; }
        public virtual Expense Expense { get; set; }
        public decimal AmountExpensePayment { get; set; }
        public decimal Valor_Pago { get; set; }
        public decimal Valor_Juros { get; set; }
        public DateTime? ConcludedDate { get; set; }
        public Status StatusExpensePayment { get; set; }
        public DateTime DueDateExpensePayment { get; set; }
    }
}
