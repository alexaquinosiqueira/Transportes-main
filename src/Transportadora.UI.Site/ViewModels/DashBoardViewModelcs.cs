using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Transportadora.UI.Site.ViewModels
{
    public class DashBoardViewModelcs
    {
        public int TotalVehicles { get; set; }
        public int TotalEmployees { get; set; }
        public decimal TotalExpenses { get; set; }
        public decimal TotalInvoices { get; set; }
        public IEnumerable<FinancialSettlementViewModel> financialSettlementViewModels { get; set; }
    }
}
