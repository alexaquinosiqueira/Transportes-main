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
using Transportadora.Data.Context;
using Transportadora.UI.Site.AppServer;
using Transportadora.UI.Site.ViewModels;

namespace Transportadora.UI.Site.Areas.Cadastro.Controllers
{
    [Area("Cadastro")]
    public class ResponsibilitiesController : Controller
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IResponsibilityRepository _responsibilityRepository;
        private readonly IMapper _mapper;
        public ResponsibilitiesController(IResponsibilityRepository responsibilityRepository,
                                ICompanyRepository companyRepository,
                                IMapper mapper)
        {
            _companyRepository = companyRepository;
            _responsibilityRepository = responsibilityRepository;
            _mapper = mapper;
        }

        // GET: Cadastro/Responsibilities
        public async Task<IActionResult> Index()
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);

            var users = await _responsibilityRepository.GetByCompanyId(companyId);

            return View(_mapper.Map<IEnumerable<ResponsibilityViewModel>>(users));
        }

        // GET: Cadastro/Responsibilities/Details/5
        public async Task<IActionResult> Details(Guid id)
        {


            var userViewModel = _mapper.Map<ResponsibilityViewModel>(await _responsibilityRepository.GetById(id));
            if (userViewModel == null)
            {
                return NotFound();
            }

            return View(userViewModel);
        }

        // GET: Cadastro/Responsibilities/Create
        public async Task<IActionResult> Create()
        {
            var companies = await _companyRepository.GetAll();
            ViewData["companies"] = _mapper.Map<IEnumerable<CompanyViewModel>>(companies);
            return View();
        }

        // POST: Cadastro/Responsibilities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ResponsibilityViewModel responsibilityViewModel)
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            responsibilityViewModel.Company_Id = companyId;


            foreach (var item in responsibilityViewModel.responsa)
            {

            }

            if (!ModelState.IsValid) return View(responsibilityViewModel);

            var responsibility = _mapper.Map<Responsibility>(responsibilityViewModel);

            await _responsibilityRepository.Add(responsibility);

            return RedirectToAction(nameof(Index));
        }

        public void Importacao( [FromBody] int Ids)
        {
            string result = Ids.ToString();
            
        } 

        // GET: Cadastro/Responsibilities/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var companies = await _companyRepository.GetAll();
            ViewData["companies"] = _mapper.Map<IEnumerable<CompanyViewModel>>(companies);

            var responsibilityViewModel = _mapper.Map<ResponsibilityViewModel>(await _responsibilityRepository.GetById(id));

            if (responsibilityViewModel == null)
            {
                return NotFound();
            }
            return View(responsibilityViewModel);
        }

        // POST: Cadastro/Responsibilities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ResponsibilityViewModel responsibilityViewModel)
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            responsibilityViewModel.Company_Id = companyId;

            if (id != responsibilityViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(responsibilityViewModel);

            var responsibility = _mapper.Map<Responsibility>(responsibilityViewModel);
            await _responsibilityRepository.Update(responsibility);


            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var responsibilityViewModel = _mapper.Map<ResponsibilityViewModel>(await _responsibilityRepository.GetById(id));

            if (responsibilityViewModel == null)
            {
                return NotFound();
            }
            await _responsibilityRepository.Remove(await _responsibilityRepository.GetById(id));

            TempData["cls"] = "success";
            TempData["message"] = "Deletado com sucesso !!";

            return RedirectToAction(nameof(Index));
        }
    }
}
