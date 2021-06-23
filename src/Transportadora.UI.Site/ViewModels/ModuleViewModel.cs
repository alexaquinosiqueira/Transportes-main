using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Transportadora.UI.Site.ViewModels
{
    public class ModuleViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int ModuleOrder { get; set; }
        public virtual ICollection<FuncionalityViewModel> Functionalities { get; set; }
    }
}
