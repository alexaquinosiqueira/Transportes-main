using System;
using System.Collections.Generic;

namespace Transportadora.Business.Models
{
    public class RequisicaoCompra : Entity
    {
        public DateTime Data_Requisicao { get; set; }
        public string Code { get; set; }
        public string Solicitante { get; set; }
        public Guid Solicitado_Para { get; set; }
        public string Tipo_Requisicao { get; set; }
        public string Prioridade { get; set; }
        public string Observacao { get; set; }
        public Guid Company_Id { get; set; }
        public virtual Company Company { get; set; }
        public Guid CostCenter_Id { get; set; }
        public virtual CostCenter CostCenter { get; set; }

        public virtual Vehicle Vehicle { get; set; }

        public virtual ICollection<ItensRequisicaoCompra> ItensRequisicaoCompras { get; set; }

    }
}
