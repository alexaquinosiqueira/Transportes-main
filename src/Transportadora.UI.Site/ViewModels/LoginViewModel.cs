using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Transportadora.UI.Site.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Login { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Password { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Empresa")]
        public Guid Company_Id { get; set; }
    }
}
