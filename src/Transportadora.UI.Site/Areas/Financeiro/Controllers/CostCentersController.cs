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
using Transportadora.UI.Site.Data;
using Transportadora.UI.Site.ViewModels;

namespace Transportadora.UI.Site.Areas.Financeiro.Controllers
{
    [Area("Financeiro")]
    public class CostCentersController : Controller
    {
        private readonly ICostcenterRepository _CostCenterRepository;
        private readonly IMapper _mapper;
        public CostCentersController(ICostcenterRepository CostCenterRepository,
                                IMapper mapper)
        {
            _CostCenterRepository = CostCenterRepository;
            _mapper = mapper;
        }

        // GET: Cadastro/Responsibilities
        public async Task<IActionResult> Index()
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);

            var users = await _CostCenterRepository.GetByCompanyId(companyId);

            return View(_mapper.Map<IEnumerable<CostCenterViewModel>>(users));
        }

        // GET: Cadastro/Responsibilities/Details/5
        public async Task<IActionResult> Details(Guid id)
        {


            var userViewModel = _mapper.Map<CostCenterViewModel>(await _CostCenterRepository.GetById(id));
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
        public async Task<IActionResult> Create(CostCenterViewModel CostCenterViewModel)
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            CostCenterViewModel.Company_Id = companyId;

            if (!ModelState.IsValid) return View(CostCenterViewModel);

            var CostCenter = _mapper.Map<CostCenter>(CostCenterViewModel);

            await _CostCenterRepository.Add(CostCenter);

            TempData["cls"] = "success";
            TempData["message"] = "Criado com sucesso !!";

            return RedirectToAction(nameof(Index));
        }

        // GET: Cadastro/Responsibilities/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var CostCenterViewModel = _mapper.Map<CostCenterViewModel>(await _CostCenterRepository.GetById(id));

            if (CostCenterViewModel == null)
            {
                return NotFound();
            }
            return View(CostCenterViewModel);
        }

        // POST: Cadastro/Responsibilities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CostCenterViewModel CostCenterViewModel)
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            CostCenterViewModel.Company_Id = companyId;

            if (id != CostCenterViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(CostCenterViewModel);

            var CostCenter = _mapper.Map<CostCenter>(CostCenterViewModel);
            await _CostCenterRepository.Update(CostCenter);

            TempData["cls"] = "success";
            TempData["message"] = "Editado com sucesso !!";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var CostCenterViewModel = _mapper.Map<CostCenterViewModel>(await _CostCenterRepository.GetById(id));

            if (CostCenterViewModel == null)
            {
                return NotFound();
            }
            await _CostCenterRepository.Remove(await _CostCenterRepository.GetById(id));

            TempData["cls"] = "success";
            TempData["message"] = "Deletado com sucesso !!";

            return RedirectToAction(nameof(Index));
        }
    }
}
