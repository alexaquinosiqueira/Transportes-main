using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Transportadora.UI.Site.ViewModels
{
    public class InsuranceViewModel
    {
        public InsuranceViewModel()
        {
            Id = Guid.NewGuid();
        }
        [Key]
        public Guid Id { get; set; }
        [DisplayName("Veículo")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid Vehicle_Id { get; set; }
        [DisplayName("Veículo")]
        public VehicleViewModel Vehicle { get; set; }
        [DisplayName("Seguradora")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid InsuranceCompany_Id { get; set; }
        [DisplayName("Seguradora")]
        public InsuranceCompanyViewModel InsuranceCompany { get; set; }
        [DisplayName("Apólice")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Apolice { get; set; }
        [DisplayName("Data Início")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartDate { get; set; }
        [DisplayName("Data Fim")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EndDate { get; set; }
        [DisplayName("Colisão")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Colision { get; set; }
        [DisplayName("Roubo")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Stole { get; set; }
        [DisplayName("Explosão")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Explosion { get; set; }
        [DisplayName("Danos Materiais")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal MaterialTheft { get; set; }
        [DisplayName("Terceiros")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal ThirdDamage { get; set; }
        [DisplayName("Raio")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Thunderbolt { get; set; }
        [DisplayName("Valor")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Value { get; set; }
        [DisplayName("Situação do Seguro")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid InsuranceSituation_Id { get; set; }
        [DisplayName("Situação do Seguro")]
        public InsuranceSituationViewModel InsuranceSituation { get; set; }
        public Guid Company_Id { get; set; }
        public CompanyViewModel Company { get; set; }
    }
}
