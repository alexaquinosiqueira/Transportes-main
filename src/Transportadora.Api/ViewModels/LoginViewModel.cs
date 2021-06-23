using System.ComponentModel.DataAnnotations;

namespace Transportadora.Api.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Login { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Password { get; set; }
    }
}
