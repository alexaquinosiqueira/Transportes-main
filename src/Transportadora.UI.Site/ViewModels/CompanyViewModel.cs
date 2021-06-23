using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Transportadora.UI.Site.ViewModels
{
    public class CompanyViewModel
    {
        [Key]
        public Guid Id { get; set; }
        [DisplayName("Ativo?")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public bool Status { get; set; }
        [DisplayName("Razão Social")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string RazaoSocial { get; set; }
        [DisplayName("CNPJ")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string CNPJ { get; set; }
        [DisplayName("Telefone")]
        public string Telefone { get; set; }
        [DisplayName("E-Mail")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Email { get; set; }
        [DisplayName("Contato")]
        public string Contato { get; set; }
        [DisplayName("Insc. Estadual")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string InscEstatudal { get; set; }
        [DisplayName("Insc. Municipal")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string InscMunicipal { get; set; }
        public Guid Address_Id { get; set; }
        [JsonIgnore]
        public AddressViewModel Address { get; set; }

    }
}
