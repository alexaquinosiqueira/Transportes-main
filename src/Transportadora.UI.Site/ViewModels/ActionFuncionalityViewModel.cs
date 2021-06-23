using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Transportadora.UI.Site.ViewModels
{
    public class ActionFuncionalityViewModel
    {
        public Guid Id { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }
        public Guid Funcionality_Id { get; set; }
        public virtual ActionFuncionalityViewModel ActionFuncionality { get; set; }
    }
}
