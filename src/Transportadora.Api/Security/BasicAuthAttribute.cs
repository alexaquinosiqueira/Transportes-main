using Microsoft.AspNetCore.Mvc;
using System;

namespace Transportadora.Api.Security
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class BasicAuthAttribute : TypeFilterAttribute
    {
        public BasicAuthAttribute(string realm = @"Transp") : base(typeof(BasicAuthFilter))
        {
            Arguments = new object[] { realm };
        }
    }
}