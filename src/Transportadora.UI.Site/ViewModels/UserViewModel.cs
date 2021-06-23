using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Transportadora.UI.Site.ViewModels
{
    public class UserViewModel
    {
        [Key]
        public Guid Id { get; set; }
        [DisplayName("Nome")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Name { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Login { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Senha")]
        public string Password { get; set; }
        [DisplayName("Data de Expiração")]
        public DateTime ExpirationDate { get; set; }
        [DisplayName("Ativo?")]
        public bool Active { get; set; }
        [DisplayName("Perfil")]
        public Guid ProfileId { get; set; }
        public ProfileViewModel Profile { get; set; }
        public IEnumerable<UserCompanyViewModel> UserCompanies { get; set; }
        public Guid Company_Id { get; set; }
        public CompanyViewModel Company { get; set; }

    }
}