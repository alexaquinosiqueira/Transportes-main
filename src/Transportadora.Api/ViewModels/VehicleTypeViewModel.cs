using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Transportadora.Business.Models;

namespace Transportadora.Api.ViewModels
{
    public class VehicleTypeViewModel
    {
        public Guid Id { get; set; }
        [DisplayName("Descrição")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Description { get; set; }
        [DisplayName("Espécie")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public VehicleClassViewModel VehicleClass { get; set; }
    }
}
