using System.Collections.Generic;

namespace Transportadora.Business.Models
{
    public class ProfileT : Entity
    {
        public string Name { get; set; }
        virtual public IEnumerable<User> Users { get; set; }
        public virtual ICollection<ProfileActionFuncionality> ProfileActionFuncionalities { get; set; }
    }
}
