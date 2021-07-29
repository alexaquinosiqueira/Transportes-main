using System;
using System.Collections.Generic;

namespace Transportadora.Business.Models
{
    public class Produto : Entity
    {
        public string Codigo { get; set; }
        public int Quantidade { get; set; }        
        public int Qtde_minima { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public Guid Company_Id { get; set; }
        public virtual Company Company { get; set; }
        public Guid Supplier_Id { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
