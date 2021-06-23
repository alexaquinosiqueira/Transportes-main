using System;
using System.Collections.Generic;
namespace Transportadora.Business.Models
{
    public class Funcionality : Entity, IComparable
    {
        private string _code;
        private string _name;

        public string Code
        {
            get { return _code; }
            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new Exception("Funcionality Code is required");
                _code = value;
            }
        }
        public string Name
        {
            get { return _name; }
            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new Exception("Funcionality Name is required");
                _name = value;
            }
        }

        public virtual ICollection<ActionFuncionality> Actions { get; set; }
        public Guid Module_Id { get; set; }
        public virtual Module Module { get; set; }

        public Funcionality()
        {
            Actions = new List<ActionFuncionality>();
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Funcionality otherWCMFuncionality = obj as Funcionality;
            if (otherWCMFuncionality != null)
            {
                if (this._name == otherWCMFuncionality._name)
                {
                    return this._code.CompareTo(otherWCMFuncionality._code);
                }

                return this._name.CompareTo(otherWCMFuncionality._name);
            }
            else
                throw new ArgumentException("Object is not a WCMFuncionality");
        }

    }
}
