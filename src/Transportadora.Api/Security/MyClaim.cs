using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Transportadora.Api.Security
{
    public class MyClaim : ClaimsPrincipal
    {
        public MyClaim(string name, Guid idUsuario) : base(new CurrentUser(name, idUsuario))
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

        public IEnumerable<int> Clientes { get; set; }
    }
}