using Microsoft.AspNetCore.Http;
using System;

namespace Transportadora.Business.Models
{
    public class ItensRequisicaoCompra : Entity
    {
        public Guid RequisicaoCompra_Id { get; set; }
        public virtual RequisicaoCompra RequisicaoCompra { get; set; }

        public DateTime Data_Requisicao { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor_Unitario { get; set; }
        public decimal Valor_Total { get; set; }

        public virtual Produto Produto { get; set; }
        public Guid Produto_Id { get; set; }

        public virtual Supplier Supplier { get; set; }
        public Guid Supplier_Id { get; set; }

        public virtual Company Company { get; set; }
        public Guid Company_Id { get; set; }

    }
}
