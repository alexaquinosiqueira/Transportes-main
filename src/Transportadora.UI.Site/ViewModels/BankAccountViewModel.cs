using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Transportadora.UI.Site.ViewModels
{
    public class BankAccountViewModel
    {
        [Key]
        public Guid Id { get; set; }
        [DisplayName("Descrição")]
        public string Description { get; set; }
        [DisplayName("Agência")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Branch { get; set; }
        [DisplayName("Conta")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Account { get; set; }
        [DisplayName("Cheque especial")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Overdraft { get; set; }
        [DisplayName("Saldo Inicial")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal OpeningBalance { get; set; }
        [DisplayName("Saldo Atual")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal CurrentBalance { get; set; }
        [DisplayName("Banco")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid Bank_Id { get; set; }
        [DisplayName("Banco")]
        public BankViewModel Bank { get; set; }
        public Guid Company_Id { get; set; }
        public CompanyViewModel Company { get; set; }
    }
}
