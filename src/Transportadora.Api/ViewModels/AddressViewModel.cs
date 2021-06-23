using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Transportadora.Api.ViewModels
{
    public class AddressViewModel
    {
        public AddressViewModel()
        {
            Id = Guid.NewGuid();
        }
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
        [DisplayName("Cidade")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string City { get; set; }
        [DisplayName("Estado")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string State { get; set; }
        [DisplayName("CEP")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string PostalCode { get; set; }
    }
}
