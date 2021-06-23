using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Transportadora.Api.ViewModels;
using Transportadora.Business.Interfaces;

namespace Transportadora.Api.Security
{
    public class BasicAuthFilter : IAuthorizationFilter
    {
        private readonly string _realm = "Transp";
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public BasicAuthFilter(IUserRepository userRepository,
                                IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                PathString path = context.HttpContext.Request.Path;
                if (path.Equals("/usuario/login"))
                {
                    return;
                }

                string authHeader = context.HttpContext.Request.Headers["Authorization"];
                if (authHeader != null)
                {
                    AuthenticationHeaderValue authHeaderValue = AuthenticationHeaderValue.Parse(authHeader);
                    if (authHeaderValue.Scheme.Equals(AuthenticationSchemes.Basic.ToString(), StringComparison.OrdinalIgnoreCase))
                    {
                        string[] credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authHeaderValue.Parameter ?? string.Empty)).Split(':');
                        if (credentials.Length == 2)
                        {
                            if (await CheckPassword(context, credentials[0], credentials[1]))
                            {
                                return;
                            }
                        }
                    }
                }

                ReturnUnauthorizedResult(context);
            }
            catch (FormatException)
            {
                ReturnUnauthorizedResult(context);
            }
        }

        private async Task<bool> CheckPassword(AuthorizationFilterContext context, string username, string password)
        {
            var users = await _userRepository.Search(x => x.Login == username && x.Password == password);
            var user = _mapper.Map<UserViewModel>(users.FirstOrDefault());

            if (user == null)
            {
                return false;
            }

            context.HttpContext.User = new MyClaim(user.Name, user.Id);
            return true;
        }

        private void ReturnUnauthorizedResult(AuthorizationFilterContext context)
        {
            // Return 401 and a basic authentication challenge (causes browser to show login dialog)
            context.HttpContext.Response.Headers["WWW-Authenticate"] = $"Basic realm=\"{_realm}\"";
            context.Result = new UnauthorizedResult();
        }
    }
}