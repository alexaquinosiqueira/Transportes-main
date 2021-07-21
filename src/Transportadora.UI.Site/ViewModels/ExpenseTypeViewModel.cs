using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Transportadora.UI.Site.ViewModels
{
    public class ExpenseTypeViewModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public string Segmento { get; set; }
        public Guid Company_Id { get; set; }
        public virtual CompanyViewModel Company { get; set; }
        public ICollection<ExpenseFinancialSettlementViewModel> ExpenseFinancialSettlements { get; set; }
    }
}
