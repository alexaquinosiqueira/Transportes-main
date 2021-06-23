using System;

namespace Transportadora.Business.Models
{
    public class ProfileActionFuncionality : Entity
    {
        public Guid Profile_Id { get; set; }
        public virtual ProfileT Profile { get; set; }
        public Guid ActionFuncionality_Id { get; set; }
        public virtual ActionFuncionality ActionFuncionality { get; set; }
    }
}
