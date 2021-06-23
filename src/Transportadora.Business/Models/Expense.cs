using System;
using System.Collections.Generic;
using System.Text;

namespace Transportadora.Business.Models
{
    public class Expense : Entity
    {

		public Guid? Supplier_Id { get; set; }
		public virtual Supplier Supplier { get; set; }
		public string Description { get; set; }
		public decimal Amount { get; set; }
		public DateTime Date { get; set; }
		public Status Status { get; set; }
		public DateTime DueDate { get; set; }
		public DateTime? SettlementDate { get; set; }

		public Guid CostCenter_Id { get; set; }
		public virtual CostCenter CostCenter { get; set; }
		public Guid BankAccount_Id { get; set; }
		public virtual BankAccount BankAccount { get; set; }
		public Guid DocumentType_Id { get; set; }
		public virtual DocumentType DocumentType { get; set; }
		public int? DocumentNumber { get; set; }
		public Guid PaymentForm_Id { get; set; }
		public virtual PaymentForm PaymentForm { get; set; }
		public string Observation { get; set; }

		public Guid? Vehicle_Id { get; set; }
		public virtual Vehicle Vehicle { get; set; }

		public DateTime? LastSettlementDate { get; set; }
		public decimal PaidAmount { get; set; }

		public virtual ICollection<ExpensePayment> ExpensePayment { get; set; }
		public Guid Company_Id { get; set; }
		public virtual Company Company { get; set; }

	}
}
