using System;
using System.Collections.Generic;

namespace Transportadora.Business.Models
{
    public class Module : Entity
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new Exception("Name is required");
                _name = value;
            }
        }
        public int ModuleOrder { get; set; }

        public virtual ICollection<Funcionality> Functionalities { get; set; }
    }
}
