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
    public class InsuranceSituationsController : Controller
    {
        private readonly IInsuranceSituationRepository _InsuranceSituationRepository;
        private readonly IMapper _mapper;
        public InsuranceSituationsController(IInsuranceSituationRepository InsuranceSituationRepository,
                                IMapper mapper)
        {
            _InsuranceSituationRepository = InsuranceSituationRepository;
            _mapper = mapper;
        }

        // GET: Cadastro/Responsibilities
        public async Task<IActionResult> Index()
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            var users = await _InsuranceSituationRepository.GetByCompanyId(companyId);

            return View(_mapper.Map<IEnumerable<InsuranceSituationViewModel>>(users));
        }

        // GET: Cadastro/Responsibilities/Details/5
        public async Task<IActionResult> Details(Guid id)
        {


            var userViewModel = _mapper.Map<InsuranceSituationViewModel>(await _InsuranceSituationRepository.GetById(id));
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
        public async Task<IActionResult> Create(InsuranceSituationViewModel InsuranceSituationViewModel)
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            InsuranceSituationViewModel.Company_Id = companyId;

            if (!ModelState.IsValid) return View(InsuranceSituationViewModel);

            var InsuranceSituation = _mapper.Map<InsuranceSituation>(InsuranceSituationViewModel);

            await _InsuranceSituationRepository.Add(InsuranceSituation);

            return RedirectToAction(nameof(Index));
        }

        // GET: Cadastro/Responsibilities/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var InsuranceSituationViewModel = _mapper.Map<InsuranceSituationViewModel>(await _InsuranceSituationRepository.GetById(id));

            if (InsuranceSituationViewModel == null)
            {
                return NotFound();
            }
            return View(InsuranceSituationViewModel);
        }

        // POST: Cadastro/Responsibilities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, InsuranceSituationViewModel InsuranceSituationViewModel)
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            InsuranceSituationViewModel.Company_Id = companyId;

            if (id != InsuranceSituationViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(InsuranceSituationViewModel);

            var InsuranceSituation = _mapper.Map<InsuranceSituation>(InsuranceSituationViewModel);
            await _InsuranceSituationRepository.Update(InsuranceSituation);


            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var InsuranceSituationViewModel = _mapper.Map<InsuranceSituationViewModel>(await _InsuranceSituationRepository.GetById(id));

            if (InsuranceSituationViewModel == null)
            {
                return NotFound();
            }
            await _InsuranceSituationRepository.Remove(await _InsuranceSituationRepository.GetById(id));

            TempData["cls"] = "success";
            TempData["message"] = "Deletado com sucesso !!";

            return RedirectToAction(nameof(Index));
        }
    }
}
