using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Transportadora.UI.Site.ViewModels
{
    public class InsuranceSituationViewModel
    {
        [Key]
        public Guid Id { get; set; }
        [DisplayName("Descrição")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Description { get; set; }
        [DisplayName("Ativo?")]
        public bool Active { get; set; }
        public Guid Company_Id { get; set; }
        public CompanyViewModel Company { get; set; }
    }
}
