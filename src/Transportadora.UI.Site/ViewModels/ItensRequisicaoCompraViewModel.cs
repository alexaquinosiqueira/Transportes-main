using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Transportadora.UI.Site.ViewModels
{
    public class ItensRequisicaoCompraViewModel
    {
        [Key]
        public Guid? Id { get; set; }
        public Guid? RequisicaoCompra_Id { get; set; }
        public RequisicaoCompraViewModel RequisicaoCompra { get; set; }
        [DataType(DataType.Currency)]

        public DateTime Data_Requisicao { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor_Unitario { get; set; }
        public decimal Valor_Total { get; set; }
        public Guid Produto_Id { get; set; }
        public ProdutoViewModel Produto { get; set; }

        public Guid Supplier_Id { get; set; }
        public SupplierViewModel Supplier { get; set; }

        public Guid Company_Id { get; set; }
        public CompanyViewModel Company { get; set; }
    }
}
