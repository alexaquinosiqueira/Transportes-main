using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Transportadora.UI.Site.ViewModels
{
    public class UserCompanyViewModel {
        public Guid Id { get; set; }
        public Guid Id_User { get; set; }
        public UserViewModel User { get; set; }
        public Guid Id_Company { get; set; }
        public CompanyViewModel Company { get; set; }
    }
}
