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
    public class FleetsController : Controller
    {
        private readonly IFleetRepository _fleetRepository;
        private readonly IFinancialSettlementRepository _FinancialSettlementRepository;
        private readonly IMapper _mapper;
        public FleetsController(IFleetRepository fleetRepository,
                                IFinancialSettlementRepository financialSettlementRepository,
                                IMapper mapper)
        {
            _fleetRepository = fleetRepository;
            _FinancialSettlementRepository = financialSettlementRepository;
            _mapper = mapper;
        }

        // GET: Cadastro/Responsibilities
        public async Task<IActionResult> Index()
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            var users = await _fleetRepository.GetByCompanyId(companyId);

            return View(_mapper.Map<IEnumerable<FleetViewModel>>(users));
        }

        // GET: Cadastro/Responsibilities/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var userViewModel = _mapper.Map<FleetViewModel>(await _fleetRepository.GetById(id));
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
        public async Task<IActionResult> Create(FleetViewModel FleetViewModel)
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            FleetViewModel.Company_Id = companyId;

            if (!ModelState.IsValid) return View(FleetViewModel);

            var fleet = _mapper.Map<Fleet>(FleetViewModel);

            await _fleetRepository.Add(fleet);

            TempData["cls"] = "success";
            TempData["message"] = "Criado com sucesso !!";

            return RedirectToAction(nameof(Index));
        }

        // GET: Cadastro/Responsibilities/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var FleetViewModel = _mapper.Map<FleetViewModel>(await _fleetRepository.GetById(id));

            if (FleetViewModel == null)
            {
                return NotFound();
            }
            return View(FleetViewModel);
        }

        // POST: Cadastro/Responsibilities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, FleetViewModel FleetViewModel)
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            FleetViewModel.Company_Id = companyId;

            if (id != FleetViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(FleetViewModel);

            var fleet = _mapper.Map<Fleet>(FleetViewModel);
            await _fleetRepository.Update(fleet);

            TempData["cls"] = "success";
            TempData["message"] = "Editado com sucesso !!";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var FleetViewModel = _mapper.Map<FleetViewModel>(await _fleetRepository.GetById(id));

            if (FleetViewModel == null)
            {
                return NotFound();
            }


            //verifico se tem despesa para essa frota.
            string cAcertos = string.Empty;
            string proximoAcerto = string.Empty;
            int nAcertos = 0;

            var expenser = await _FinancialSettlementRepository.GetAll();
            ViewData["FinancialSettlement"] = _mapper.Map<IEnumerable<FinancialSettlement>>(expenser).ToList();

            foreach (var item in _mapper.Map<IEnumerable<FinancialSettlement>>(expenser).ToList().Where(w => w.Fleet_Id == id))
            {
                if (proximoAcerto != item.Code)
                {
                    cAcertos = cAcertos + " - " + Environment.NewLine + item.Code;
                    nAcertos = nAcertos + 1;
                    proximoAcerto = item.Code;
                }
            }

            if (nAcertos > 0)
            {
                TempData["cls"] = "danger";
                TempData["message"] = "Não é possível excluir esse registro. Existe (m) " + nAcertos + " acerto (s) vinculado (s) a essa frota.!! " + cAcertos;

                return RedirectToAction(nameof(Index));
            }
            else
            {
                await _fleetRepository.Remove(await _fleetRepository.GetById(id));

                TempData["cls"] = "success";
                TempData["message"] = "Deletado com sucesso !!";

                return RedirectToAction(nameof(Index));
            }

        }
    }
}
