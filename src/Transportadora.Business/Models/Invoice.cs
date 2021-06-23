using System;
using System.Collections.Generic;

namespace Transportadora.Business.Models
{
    public class Invoice : Entity
    {
        public Guid? Customer_Id { get; set; }
        public virtual Customer? Customer { get; set; }
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

        public DateTime? LastSettlementDate { get; set; }
        public decimal PaidAmount { get; set; }

        public virtual ICollection<InvoicePayment> InvoicePayments { get; set; }
        public Guid Company_Id { get; set; }
        public virtual Company Company { get; set; }

    }
}
