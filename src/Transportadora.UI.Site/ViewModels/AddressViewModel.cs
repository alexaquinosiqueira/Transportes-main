using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Transportadora.UI.Site.ViewModels
{
    public class AddressViewModel
    {
        //public AddressViewModel()
        //{
        //    Id = Guid.NewGuid();
        //}
        [Key]
        public Guid Id { get; set; }
        [DisplayName("Endereço")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string AddressLine { get; set; }
        [DisplayName("Número")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Number { get; set; }
        [DisplayName("Bairro")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Neighborhood { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int City_Id { get; set; }
        [DisplayName("Cidade")]
        public CityViewModel City { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int State_id { get; set; }
        [DisplayName("Estado")]
        public StateViewModel State { get; set; }

        [DisplayName("CEP")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string PostalCode { get; set; }
    }
}
