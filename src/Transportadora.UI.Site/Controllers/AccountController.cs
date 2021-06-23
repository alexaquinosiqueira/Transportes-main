using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Transportadora.Business.Interfaces;
using Transportadora.Business.Models;
using Transportadora.UI.Site.ViewModels;
using Transportadora.UI.Site.Helpers;

namespace Transportadora.UI.Site.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        public AccountController(IUserRepository userRepository,
                                 IProfileRepository profileRepository,
                                 ICompanyRepository companyRepository,
                                IMapper mapper)
        {
            _userRepository = userRepository;
            _profileRepository = profileRepository;
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            var companies = await _companyRepository.GetAll();
            ViewData["companies"] = _mapper.Map<IEnumerable<CompanyViewModel>>(companies);
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> GetCompaniesByUserLogin(string userLogin)
        {
            List<CompanyViewModel> companies = new List<CompanyViewModel>();
            var userViewModel = _mapper.Map<UserViewModel>((await _userRepository.Search(u => u.Login.ToUpper().Equals(userLogin.ToUpper()))).FirstOrDefault());
            if (userViewModel != null)
            {

                foreach (var item in userViewModel.UserCompanies)
                {
                    companies.Add(item.Company);
                }
            }
            return Json(companies);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            var user = (await _userRepository.Search(x => x.Login == loginViewModel.Login &&
                                                         x.Password == Helpers.Helpers.ConvertMD5(loginViewModel.Password))).FirstOrDefault();

            var company = await _companyRepository.GetById(loginViewModel.Company_Id);

            if (user != null)
            {
                var functionalities = _profileRepository.GetById(user.ProfileId).Result.ProfileActionFuncionalities.Select(c => c.ActionFuncionality);

                //Create the identity for the user  
                var identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, user.Name),
                    //new Claim(ClaimTypes.Role, string.Join(",", functionalities.Select(r => r.Funcionality.Module.Name + r.Role))),
                    new Claim("CompanyID", company.Id.ToString()),
                    new Claim("CompanyName", company.RazaoSocial)
                }, CookieAuthenticationDefaults.AuthenticationScheme);

                foreach (var funcionality in functionalities)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, funcionality.Role));
                }


                var principal = new ClaimsPrincipal(identity);

                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Home");

            }
            else
            {
                TempData["failed"] = true;
                return View(loginViewModel);
            }

        }
        public IActionResult Logout()
        {
            var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}