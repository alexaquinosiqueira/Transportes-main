using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Transportadora.Business.Interfaces;
using Transportadora.Business.Models;
using Transportadora.UI.Site.AppServer;
using Transportadora.UI.Site.ViewModels;

namespace Transportadora.UI.Site.Controllers
{
    [Area("Portal")]
    public class UsersController : Controller
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UsersController(IUserRepository userRepository,
                                IProfileRepository profileRepository,
                                ICompanyRepository companyRepository,
                                IMapper mapper)
        {
            _companyRepository = companyRepository;
            _profileRepository = profileRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }     

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAll();
            
            return View(_mapper.Map<IEnumerable<UserViewModel>>(users));
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(Guid id)
        {


            var userViewModel = _mapper.Map<UserViewModel>(await _userRepository.GetById(id));
            if (userViewModel == null)
            {
                return NotFound();
            }

            return View(userViewModel);
        }

        // GET: Users/Create
        public async Task<IActionResult> Create()
        {
            var profiles = await _profileRepository.GetAll();
            var companies = await _companyRepository.GetAll();
            ViewData["profiles"] = _mapper.Map<IEnumerable<ProfileViewModel>>(profiles);
            ViewData["companies"] = new MultiSelectList(_mapper.Map<IEnumerable<CompanyViewModel>>(companies).ToList(), "Id", "RazaoSocial");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserViewModel userViewModel, Guid[] companies)
        {
            userViewModel.Password = Helpers.Helpers.ConvertMD5(userViewModel.Password);

            IdentityManager identityManager = new IdentityManager(User);
            var companyId = identityManager.CompanyID;

            if (!ModelState.IsValid) return View(userViewModel);

            List<UserCompanyViewModel> userCompanies = new List<UserCompanyViewModel>();

            foreach (var company in companies)
            {
                UserCompanyViewModel userCompanyViewModel = new UserCompanyViewModel() { 
                    User = userViewModel, 
                    Id_Company = company 
                };

                userCompanies.Add(userCompanyViewModel);
            }
            userViewModel.UserCompanies = userCompanies;

            var user = _mapper.Map<User>(userViewModel);

            await _userRepository.Add(user);

            return RedirectToAction(nameof(Index));
            
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {

            var userViewModel = _mapper.Map<UserViewModel>(await _userRepository.GetById(id));
            var profiles = await _profileRepository.GetAll();
            var companies = await _companyRepository.GetAll();
            ViewData["profiles"] = _mapper.Map<IEnumerable<ProfileViewModel>>(profiles);
            ViewData["companies"] = new MultiSelectList(_mapper.Map<IEnumerable<CompanyViewModel>>(companies).ToList(), "Id", "RazaoSocial");
            if (userViewModel == null)
            {
                return NotFound();
            }
            return View(userViewModel);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UserViewModel userViewModel)
        {
            userViewModel.Password = Helpers.Helpers.ConvertMD5(userViewModel.Password);
            if (id != userViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(userViewModel);

            var user = _mapper.Map<User>(userViewModel);
            await _userRepository.Update(user);


            return RedirectToAction("Index");
        }



        // POST: Users/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userViewModel = _mapper.Map<UserViewModel>(await _userRepository.GetById(id));

            if (userViewModel == null)
            {
                return NotFound();
            }
            await _userRepository.Remove(await _userRepository.GetById(id));

            TempData["cls"] = "success";
            TempData["message"] = "Deletado com sucesso !!";

            return RedirectToAction(nameof(Index));
        }

    }
}
