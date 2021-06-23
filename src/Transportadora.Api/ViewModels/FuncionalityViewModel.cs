using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Transportadora.Api.ViewModels
{
    public class FuncionalityViewModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Guid Module_Id { get; set; }
        public virtual ICollection<ActionFuncionalityViewModel> Actions { get; set; }
        public virtual ModuleViewModel Module { get; set; }
    }
}
