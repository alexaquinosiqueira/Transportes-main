using System;
using System.Collections.Generic;

namespace Transportadora.Business.Models
{
    public class Bank : Entity
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public virtual ICollection<BankAccount> BankAccounts { get; set; }
        public Guid Company_Id { get; set; }
        public virtual Company Company { get; set; }

    }
}
