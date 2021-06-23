using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace Transportadora.Api.Security
{
    public class CurrentUser : IIdentity
    {
        public CurrentUser(string name, Guid idUsuario)
        {
            Name = name;
            IdUsuario = idUsuario;
            IsAuthenticated = true;
            AuthenticationType = "ADM";
        }

        public string AuthenticationType { get; set; }

        public bool IsAuthenticated { get; set; }

        public string Name { get; set; }

        public Guid IdUsuario { get; set; }
    }
}