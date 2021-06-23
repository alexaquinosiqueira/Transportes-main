using System.Collections.Generic;

namespace Transportadora.Business.Models
{
    public class City : Entity
    {
        public int  Id { get; set; }
        public string Name { get; set; }
        public int Uf { get; set; }
        public virtual State State { get; set; }

        public virtual ICollection<FinancialSettlement> FinancialSettlementsCityOrigin { get; set; }
        public virtual ICollection<FinancialSettlement> FinancialSettlementsDestinationCity { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
    }
}
