using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Transportadora.UI.Site.ViewModels
{
    public class RequisicaoCompraViewModel
    {
        [Key]
        public Guid Id { get; set; }
        [DisplayName("Código Requisicao")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]

        public string Observacao { get; set; }
        [DisplayName("Observação")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]

        public DateTime Data_Requisicao { get; set; }
        [DisplayName("Data Requisição")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Code { get; set; }
        [DisplayName("Numero Requisição")]
        public string Solicitante { get; set; }
        [DisplayName("Solicitado por ")]
        public Guid Solicitado_Para { get; set; }
        [DisplayName("Solicitado Para ")]
        public string Tipo_Requisicao { get; set; }
        [DisplayName("Tipo Requisição")]
        public string Prioridade { get; set; }
        [DisplayName("Prioridade")]

        public Guid Company_Id { get; set; }
        public CompanyViewModel Company { get; set; }

        public Guid CostCenter_Id { get; set; }
        public CostCenterViewModel CostCenter { get; set; }

        public VehicleViewModel Vehicle { get; set; }

        public ICollection<ItensRequisicaoCompraViewModel> ItensRequisicaoCompras { get; set; }
    }
}
