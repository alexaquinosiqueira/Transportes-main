using System;
using System.Collections.Generic;

namespace Transportadora.Business.Models
{
    public class Responsibility : Entity
    {
        public string Description { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public bool Active { get; set; }
        public Guid Company_Id { get; set; }
        public virtual Company Company { get; set; }

    }
}
