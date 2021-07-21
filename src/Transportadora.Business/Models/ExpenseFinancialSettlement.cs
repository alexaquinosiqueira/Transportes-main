using Microsoft.AspNetCore.Http;
using System;

namespace Transportadora.Business.Models
{
    public class ExpenseFinancialSettlement : Entity
    {
        public Guid FinancialSettlement_Id { get; set; }
        public virtual FinancialSettlement FinancialSettlement { get; set; }
        public decimal Amount { get; set; }

        public virtual Employee Employee { get; set; }
        public Guid? Employee_Id { get; set; }

        public virtual Supplier Supplier { get; set; }
        public Guid Supplier_Id { get; set; }
        public virtual ExpenseType ExpenseType { get; set; }
        public Guid ExpenseType_Id { get; set; }


        public DateTime ExpenseDate { get; set; }

        public decimal Litros { get; set; }
        public decimal Valor_Unitario { get; set; }
        public decimal Valor_Total { get; set; }
        public decimal Consumo { get; set; }
        public string Arquivo { get; set; }

        public decimal Desconto { get; set; }

    }
}
