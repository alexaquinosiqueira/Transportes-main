using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Transportadora.Api.ViewModels
{
    public class VehicleViewModel
    {
        public VehicleViewModel()
        {
            Id = Guid.NewGuid();
        }
        [Key]
        public Guid Id { get; set; }
        [DisplayName("Tipo do Veículo")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid VehicleType_Id { get; set; }
        public VehicleTypeViewModel VehicleType { get; set; }
        [DisplayName("Frota")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid Fleet_Id { get; set; }
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
    }
}
