using System;
using System.Collections.Generic;

namespace Transportadora.Business.Models
{
    public class VehicleType : Entity
    {
        public string Description { get; set; }
        public VehicleClass VehicleClass { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
        public Guid Company_Id { get; set; }
        public virtual Company Company { get; set; }
    }
}
