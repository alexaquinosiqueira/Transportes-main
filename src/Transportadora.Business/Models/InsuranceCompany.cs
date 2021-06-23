using System;
using System.Collections.Generic;

namespace Transportadora.Business.Models
{
    public class InsuranceCompany : Entity
    {
        public string Description { get; set; }
        public bool Active { get; set; }
        public virtual ICollection<Insurance> Insurances { get; set; }
        public Guid Company_Id { get; set; }
        public virtual Company Company { get; set; }
    }
}
