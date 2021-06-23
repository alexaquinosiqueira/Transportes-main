using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Transportadora.UI.Site.ViewModels
{
    public class VehicleViewModel
    {
        //public VehicleViewModel()
        //{
        //    Id = Guid.NewGuid();
        //}
        [Key]
        public Guid? Id { get; set; }
        [DisplayName("Tipo do Veículo")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid VehicleType_Id { get; set; }
        [DisplayName("Tipo do Veículo")]
        public VehicleTypeViewModel VehicleType { get; set; }
        [DisplayName("Frota")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid Fleet_Id { get; set; }
        [DisplayName("Frota")]
        public FleetViewModel Fleet { get; set; }
        [DisplayName("Descrição")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Description { get; set; }
        [DisplayName("Placa")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string VehicleLicensePlate { get; set; }
        [DisplayName("Ano")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Year { get; set; }
        [DisplayName("Cor")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Color { get; set; }
        [DisplayName("Marca")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Brand { get; set; }
        [DisplayName("Modelo")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]

        public string Model { get; set; }
        [DisplayName("Chassi")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal chassi { get; set; }
        [DisplayName("Renavam")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal renavam { get; set; }
        [DisplayName("KM")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal km { get; set; }
        [DisplayName("Tanque")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal tank { get; set; }
        [DisplayName("Média")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Range(0, 9999999999.999)]
        public decimal average { get; set; }
        [DisplayName("Litragem")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal litragem { get; set; }
        [DisplayName("Motorista")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid Employee_Id { get; set; }
        [DisplayName("Motorista")]
        public EmployeeViewModel Employee { get; set; }
        [DisplayName("Cidade")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int City_Id { get; set; }
        [DisplayName("Cidade")]
        public CityViewModel City { get; set; }
        public Guid Company_Id { get; set; }
        public CompanyViewModel Company { get; set; }
    }
}
