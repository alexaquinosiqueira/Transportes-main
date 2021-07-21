using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Transportadora.UI.Site.ViewModels
{
    public class EmployeeViewModel
    {
        public EmployeeViewModel()
        {
            Id = Guid.NewGuid();
        }
        [Key]
        public Guid Id { get; set; }
        [DisplayName("Nome")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Name { get; set; }
        [DisplayName("Apelido")]
        public string NickName { get; set; }
        [DisplayName("Telefone")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]

        public string CelPhone { get; set; }
        //[DisplayFormat(ApplyFormatInEditMode = true, CelPhone = "{0:(##) #####-####}")]

        [DisplayName("Sexo")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public GenreViewModel Genre { get; set; }
        [DisplayName("Data de Nascimento")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }
        [DisplayName("Estado Civil")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public MaritalStatusViewModel MaritalStatus { get; set; }
        [DisplayName("Cargo")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid Responsibility_Id { get; set; }
        public ResponsibilityViewModel Responsibility { get; set; }
        [DisplayName("Data de Admissão")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime AdmissionDate { get; set; }
        [DisplayName("Salário")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal Salary { get; set; }
        [DisplayName("Data de Demissão")]
        public DateTime? ResignationDate { get; set; }
        [DisplayName("Ativo")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public bool Active { get; set; }
        [DisplayName("Endereço")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        
        public Guid Address_Id { get; set; }
        public AddressViewModel Address { get; set; }

        [DisplayName("Imagem do Produto")]
        public IFormFile ImagemUpload { get; set; }
        public string Imagem { get; set; }

        public IEnumerable<ResponsibilityViewModel> Responsibilities { get; set; }
        public Guid Company_Id { get; set; }
        public CompanyViewModel Company { get; set; }
        [DisplayName("CNH")]
        public string CNH { get; set; }
        public int TipoCNH { get; set; }
        [DisplayName("Data CNH")]
        public DateTime DataCNH { get; set; }
        [DisplayName("RG")]
        public string RG { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Orgão Expedidor")]
        public string OrgaoExpedidor { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("UFRG")]
        public string UFRG { get; set; }
        [DisplayName("Data Emissão")]
        public DateTime DataEmissao { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("CPF")]
        public string CPF { get; set; }
        [DisplayName("Número INSS")]
        public string NumeroINSS { get; set; }
        [DisplayName("Nome Pai")]
        public string NomePai { get; set; }
        [DisplayName("Nome Mãe")]
        public string NomeMae { get; set; }
        [DisplayName("Nome Filho 1")]
        public string Filho1 { get; set; }
        [DisplayName("Nome Filho 2")]
        public string Filho2 { get; set; }
        [DisplayName("Nome Filho 3")]
        public string Filho3 { get; set; }
        [DisplayName("Nome Filho 4")]
        public string Filho4 { get; set; }
        [DisplayName("Nome Filho 5")]
        public string Filho5 { get; set; }
        [DisplayName("Data Nascimento Filho 1")]
        public DateTime? DtNascimentoFilho1 { get; set; }
        [DisplayName("Data Nascimento Filho 2")]
        public DateTime? DtNascimentoFilho2 { get; set; }
        [DisplayName("Data Nascimento Filho 3")]
        public DateTime? DtNascimentoFilho3 { get; set; }
        [DisplayName("Data Nascimento Filho 4")]
        public DateTime? DtNascimentoFilho4 { get; set; }
        [DisplayName("Data Nascimento Filho 5")]
        public DateTime? DtNascimentoFilho5 { get; set; }
    }
}
