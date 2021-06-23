using System;

namespace Transportadora.Business.Models
{
    public class UserCompany : Entity
    {
        public Guid Id_User { get; set; }
        public virtual User User { get; set; }
        public Guid Id_Company { get; set; }
        public virtual Company Company { get; set; }
    }
}
