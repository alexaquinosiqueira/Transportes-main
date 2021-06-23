using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Transportadora.UI.Site.ViewModels
{
    public class ParameterViewModel
    {
        public Guid Id { get; set; }
        public Guid Id_CostCenter { get; set; }
        public CostCenterViewModel CostCenter { get; set; }
        public Guid Id_BankAccount { get; set; }
        public BankAccountViewModel BankAccount { get; set; }
        public Guid Id_DocumentType { get; set; }
        public DocumentTypeViewModel DocumentType { get; set; }
        public Guid Id_PaymentForm { get; set; }
        public PaymentFormViewModel PaymentForm { get; set; }
        public Guid Id_Company { get; set; }
        public CompanyViewModel Company { get; set; }
        public bool ParameterAtive { get; set; }
        public int StateOrigin_Id { get; set; }
        public StateViewModel StateOrigin { get; set; }
        public int CityOrigin_Id { get; set; }
        public CityViewModel CityOrigin { get; set; }

    }
}
