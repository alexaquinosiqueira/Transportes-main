using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Transportadora.UI.Site.ViewModels
{
    public class ProfileActionFuncionalityViewModel
    {
        public Guid Profile_Id { get; set; }
        public virtual ProfileViewModel Profile { get; set; }
        public Guid ActionFuncionality_Id { get; set; }
        public virtual ActionFuncionalityViewModel ActionFuncionality { get; set; }
    }
}
