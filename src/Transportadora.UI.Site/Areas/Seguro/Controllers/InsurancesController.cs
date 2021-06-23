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
    public class InsurancesController : Controller
    {
        private readonly IInsuranceRepository _InsuranceRepository;
        private readonly IInsuranceCompanyRepository _InsuranceCompanyRepository;
        private readonly IVehicleRepository _VehicleRepository;
        private readonly IInsuranceSituationRepository _InsuranceSituationRepository;
        private readonly IMapper _mapper;
        public InsurancesController(IInsuranceRepository InsuranceRepository,
                                IInsuranceCompanyRepository InsuranceCompanyRepository,
                                IVehicleRepository VehicleRepository,
                                IInsuranceSituationRepository InsuranceSituationRepository,
                                IMapper mapper)
        {

            _InsuranceRepository = InsuranceRepository;
            _InsuranceCompanyRepository = InsuranceCompanyRepository;
            _VehicleRepository = VehicleRepository;
            _InsuranceSituationRepository = InsuranceSituationRepository;
            _mapper = mapper;
        }

        // GET: Cadastro/Responsibilities
        public async Task<IActionResult> Index()
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);

            var users = await _InsuranceRepository.GetByCompanyId(companyId);

            return View(_mapper.Map<IEnumerable<InsuranceViewModel>>(users));
        }

        // GET: Cadastro/Responsibilities/Details/5
        public async Task<IActionResult> Details(Guid id)
        {


            var userViewModel = _mapper.Map<InsuranceViewModel>(await _InsuranceRepository.GetById(id));
            if (userViewModel == null)
            {
                return NotFound();
            }

            return View(userViewModel);
        }

        // GET: Cadastro/Responsibilities/Create
        public async Task<IActionResult> Create()
        {
            var insuranceCompanies = await _InsuranceCompanyRepository.GetAll();
            ViewData["InsuranceCompanies"] = _mapper.Map<IEnumerable<InsuranceCompanyViewModel>>(insuranceCompanies);
            var insuranceSituations = await _InsuranceSituationRepository.GetAll();
            ViewData["InsuranceSituations"] = _mapper.Map<IEnumerable<InsuranceSituationViewModel>>(insuranceSituations);
            var vehicle = await _VehicleRepository.GetAll();
            ViewData["Vehicles"] = _mapper.Map<IEnumerable<VehicleViewModel>>(vehicle);
            return View();
        }

        // POST: Cadastro/Responsibilities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InsuranceViewModel InsuranceViewModel)
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            InsuranceViewModel.Company_Id = companyId;

            if (!ModelState.IsValid) return View(InsuranceViewModel);

            var Insurance = _mapper.Map<Insurance>(InsuranceViewModel);

            await _InsuranceRepository.Add(Insurance);

            return RedirectToAction(nameof(Index));
        }

        // GET: Cadastro/Responsibilities/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var insuranceCompanies = await _InsuranceCompanyRepository.GetAll();
            ViewData["InsuranceCompanies"] = _mapper.Map<IEnumerable<InsuranceCompanyViewModel>>(insuranceCompanies);
            var insuranceSituations = await _InsuranceSituationRepository.GetAll();
            ViewData["InsuranceSituations"] = _mapper.Map<IEnumerable<InsuranceSituationViewModel>>(insuranceSituations);
            var vehicle = await _VehicleRepository.GetAll();
            ViewData["Vehicles"] = _mapper.Map<IEnumerable<VehicleViewModel>>(vehicle);
            var InsuranceViewModel = _mapper.Map<InsuranceViewModel>(await _InsuranceRepository.GetById(id));

            if (InsuranceViewModel == null)
            {
                return NotFound();
            }
            return View(InsuranceViewModel);
        }

        // POST: Cadastro/Responsibilities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, InsuranceViewModel InsuranceViewModel)
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            InsuranceViewModel.Company_Id = companyId;

            if (id != InsuranceViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(InsuranceViewModel);

            var Insurance = _mapper.Map<Insurance>(InsuranceViewModel);
            await _InsuranceRepository.Update(Insurance);


            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var InsuranceViewModel = _mapper.Map<InsuranceViewModel>(await _InsuranceRepository.GetById(id));

            if (InsuranceViewModel == null)
            {
                return NotFound();
            }
            await _InsuranceRepository.Remove(await _InsuranceRepository.GetById(id));

            TempData["cls"] = "success";
            TempData["message"] = "Deletado com sucesso !!";

            return RedirectToAction(nameof(Index));
        }
    }
}
