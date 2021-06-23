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

namespace Transportadora.UI.Site.Areas.Seguro.Controllers
{
    [Area("Seguro")]
    public class InsuranceCompaniesController : Controller
    {
        private readonly IInsuranceCompanyRepository _InsuranceCompanyRepository;
        private readonly IMapper _mapper;
        public InsuranceCompaniesController(IInsuranceCompanyRepository InsuranceCompanyRepository,
                                IMapper mapper)
        {
            _InsuranceCompanyRepository = InsuranceCompanyRepository;
            _mapper = mapper;
        }

        // GET: Cadastro/Responsibilities
        public async Task<IActionResult> Index()
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);

            var users = await _InsuranceCompanyRepository.GetByCompanyId(companyId);

            return View(_mapper.Map<IEnumerable<InsuranceCompanyViewModel>>(users));
        }

        // GET: Cadastro/Responsibilities/Details/5
        public async Task<IActionResult> Details(Guid id)
        {


            var userViewModel = _mapper.Map<InsuranceCompanyViewModel>(await _InsuranceCompanyRepository.GetById(id));
            if (userViewModel == null)
            {
                return NotFound();
            }

            return View(userViewModel);
        }

        // GET: Cadastro/Responsibilities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cadastro/Responsibilities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InsuranceCompanyViewModel InsuranceCompanyViewModel)
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            InsuranceCompanyViewModel.Company_Id = companyId;

            if (!ModelState.IsValid) return View(InsuranceCompanyViewModel);

            var InsuranceCompany = _mapper.Map<InsuranceCompany>(InsuranceCompanyViewModel);

            await _InsuranceCompanyRepository.Add(InsuranceCompany);

            return RedirectToAction(nameof(Index));
        }

        // GET: Cadastro/Responsibilities/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var InsuranceCompanyViewModel = _mapper.Map<InsuranceCompanyViewModel>(await _InsuranceCompanyRepository.GetById(id));

            if (InsuranceCompanyViewModel == null)
            {
                return NotFound();
            }
            return View(InsuranceCompanyViewModel);
        }

        // POST: Cadastro/Responsibilities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, InsuranceCompanyViewModel InsuranceCompanyViewModel)
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            InsuranceCompanyViewModel.Company_Id = companyId;

            if (id != InsuranceCompanyViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(InsuranceCompanyViewModel);

            var InsuranceCompany = _mapper.Map<InsuranceCompany>(InsuranceCompanyViewModel);
            await _InsuranceCompanyRepository.Update(InsuranceCompany);


            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var InsuranceCompanyViewModel = _mapper.Map<InsuranceCompanyViewModel>(await _InsuranceCompanyRepository.GetById(id));

            if (InsuranceCompanyViewModel == null)
            {
                return NotFound();
            }
            await _InsuranceCompanyRepository.Remove(await _InsuranceCompanyRepository.GetById(id));

            TempData["cls"] = "success";
            TempData["message"] = "Deletado com sucesso !!";

            return RedirectToAction(nameof(Index));
        }
    }

}
