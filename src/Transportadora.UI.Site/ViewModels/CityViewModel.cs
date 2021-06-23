using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Transportadora.UI.Site.ViewModels
{
    public class CityViewModel
    {
        public int Id { get; set; }
        [DisplayName("Id?")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Name { get; set; }
        public int Uf { get; set; }
        public StateViewModel State { get; set; }

        public ICollection<FinancialSettlementViewModel> FinancialSettlementsCityOrigin { get; set; }
        public ICollection<FinancialSettlementViewModel> FinancialSettlementsDestinationCity { get; set; }
    }
}
