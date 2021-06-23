using System;
using System.Collections.Generic;
namespace Transportadora.Business.Models
{
    public class ActionFuncionality : Entity
    {
        private string _role;
        public string Role
        {
            get { return _role; }
            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new Exception("Role name is required");
                _role = value;
            }
        }

        public string Name { get; set; }
        public Guid Funcionality_Id { get; set; }
        public virtual Funcionality Funcionality { get; set; }
        public virtual ICollection<ProfileActionFuncionality> ProfileActionFuncionalities { get; set; }
    }
}