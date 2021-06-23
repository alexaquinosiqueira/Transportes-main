using System;

namespace Transportadora.Business.Models
{
    public class PaymentMethod : Entity
    {
        public string Description { get; set; }
        public bool Active { get; set; }
        public Guid Company_Id { get; set; }
        public virtual Company Company { get; set; }

    }
}
