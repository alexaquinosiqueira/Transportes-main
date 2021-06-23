using System;
using System.Collections.Generic;

namespace Transportadora.Business.Models
{
    public class Fleet : Entity
    {
        public string Description { get; set; }
        public bool Active { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
        public virtual ICollection<FinancialSettlement> FinancialSettlements { get; set; }
        public Guid Company_Id { get; set; }
        public virtual Company Company { get; set; }
    }
}
