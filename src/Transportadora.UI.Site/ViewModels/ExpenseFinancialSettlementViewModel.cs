using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Transportadora.UI.Site.ViewModels
{
    public class ExpenseFinancialSettlementViewModel
    {
        public Guid? Id { get; set; }
        public Guid? FinancialSettlement_Id { get; set; }
        public FinancialSettlementViewModel FinancialSettlement { get; set; }
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }
        public decimal Litros { get; set; }
        public decimal Valor_Total {get;set;}
        public decimal Desconto { get; set; }
        public string Arquivo { get; set; }

        public IFormFile ImagemUpload { get; set; }

        public Guid Supplier_Id { get; set; }
        public SupplierViewModel Supplier { get; set; }
        public Guid ExpenseType_Id { get; set; }
        public ExpenseTypeViewModel ExpenseType { get; set; }

        public DateTime ExpenseDate { get; set; }
    }
}
