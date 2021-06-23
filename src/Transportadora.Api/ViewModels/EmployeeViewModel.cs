using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Transportadora.Api.ViewModels
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
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string NickName { get; set; }
        [DisplayName("Telefone")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string CelPhone { get; set; }
        [DisplayName("Sexo")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public GenreViewModel Genre { get; set; }
        [DisplayName("Data de Nascimento")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
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
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime ResignationDate { get; set; }
        [DisplayName("Ativo")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public bool Active { get; set; }
        [DisplayName("Endereço")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid Address_Id { get; set; }
        public AddressViewModel Address { get; set; }
    }
}
