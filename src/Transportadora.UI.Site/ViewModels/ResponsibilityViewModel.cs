using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Transportadora.UI.Site.ViewModels
{
    public class ResponsibilityViewModel
    {
        [Key]
        public Guid Id { get; set; }
        [DisplayName("Descrição")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Description { get; set; }
        [DisplayName("Ativo?")]
        public bool Active { get; set; }
        [DisplayName("Empresa")]
        public Guid Company_Id { get; set; }
        public CompanyViewModel Company { get; set; }

        public List<dynamic> responsa { get; set; }


        public class Importacao
        {
            public List<dados_a_importar> Dados { get; set; }
        }

        public class dados_a_importar
        {
            string Description { get; set; }
            string Active { get; set; }
            string Company_Id { get; set; }
        }
    }
}
