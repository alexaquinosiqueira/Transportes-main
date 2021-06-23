using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Web;

namespace Transportadora.UI.Site.AppServer
{
    public class IdentityManager
    {
        public ClaimsPrincipal User { get; set; }
        public IdentityManager(ClaimsPrincipal user)
        {
            User = user;
        }
        public bool IsAuthenticated { get { return User.Identity.IsAuthenticated; } }
        public string UserName { get { return User.FindFirst(ClaimTypes.Name).Value; } }
        public string UserId { get { return User.FindFirst(ClaimTypes.NameIdentifier).Value; } }
        public string CompanyID
        {
            get
            {
                var val = User.FindFirst("CompanyID");
                return val != null ? val.Value : "";
            }
        }
        public string GetValueByKey(string key)
        {
            var val = User.FindFirst(key);
            return val != null ? val.Value : "";
        }
    }
}