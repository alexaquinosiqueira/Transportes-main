using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Transportadora.UI.Site.ViewModels
{
    public class ProdutoViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public string Codigo { get; set; }
        [DisplayName("Código Produto")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]

        public int Quantidade { get; set; }
        [DisplayName("Quantidade")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]

        public int Qtde_minima { get; set; }
        [DisplayName("Quantidade Minima")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]

        public string Description { get; set; }
        [DisplayName("Descrição Produto")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]

        public bool Active { get; set; }
        [DisplayName("Ativo?")]

        public Guid Company_Id { get; set; }
        public CompanyViewModel Company { get; set; }

        public Guid Supplier_Id { get; set; }
        public SupplierViewModel Supplier { get; set; }

    }
}
