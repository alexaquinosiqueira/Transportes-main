using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Transportadora.UI.Site.ViewModels
{
    public class ExpenseSupplierViewModel
    {
        public Guid? Id { get; set; }

        public Guid? Supplier_Id { get; set; }
        public SupplierViewModel Supplier { get; set; }
        [DataType(DataType.Currency)]

        public Guid? ExpenseType_Id { get; set; }
        public ExpenseTypeViewModel ExpenseType { get; set; }

    }
}
