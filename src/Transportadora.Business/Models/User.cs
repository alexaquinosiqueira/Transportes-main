using System;
using System.Collections.Generic;

namespace Transportadora.Business.Models
{
    public class User : Entity
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool Active { get; set; }
        public Guid ProfileId { get; set; }
        public virtual ProfileT Profile { get; set; }
        public virtual IEnumerable<UserCompany> UserCompanies { get; set; }

    }
}
