using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Transportadora.UI.Site.ViewModels
{
    public class PaymentFormViewModel
    {
        [Key]
        public Guid Id { get; set; }
        [DisplayName("Descrição")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Description { get; set; }
        [DisplayName("Intervalo")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Interval { get; set; }
        public ICollection<InvoiceViewModel> Invoices { get; set; }
        public ICollection<ExpenseViewModel> Expenses { get; set; }
        public Guid Company_Id { get; set; }
        public CompanyViewModel Company { get; set; }
    }
}
