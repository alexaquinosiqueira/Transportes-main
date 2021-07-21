using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Transportadora.Business.Models;
using Transportadora.Data.Context;
using Transportadora.Business.Interfaces;
using AutoMapper;
using Transportadora.UI.Site.AppServer;
using Transportadora.UI.Site.ViewModels;

namespace Transportadora.UI.Site.Areas.Financeiro.Controllers
{
    [Area("Financeiro")]
    public class ExpenseTypesController : Controller
    {
        private readonly IExpenseTypeRepository _expenseTypeRepository;
        private readonly IMapper _mapper;
        public ExpenseTypesController(IExpenseTypeRepository expenseTypeRepository,
                                IMapper mapper)
        {
            _expenseTypeRepository = expenseTypeRepository;
            _mapper = mapper;
        }

        // GET: Financeiro/ExpenseTypes
        public async Task<IActionResult> Index()
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            var expenseTypes = await _expenseTypeRepository.GetByCompanyId(companyId);

            return View(_mapper.Map<IEnumerable<ExpenseTypeViewModel>>(expenseTypes));
        }

        // GET: Financeiro/ExpenseTypes/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var expenseViewModel = _mapper.Map<ExpenseTypeViewModel>(await _expenseTypeRepository.GetById(id));
            if (expenseViewModel == null)
            {
                return NotFound();
            }

            return View(expenseViewModel);

        }

        // GET: Financeiro/ExpenseTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Financeiro/ExpenseTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExpenseTypeViewModel expenseTypeViewModel)
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            expenseTypeViewModel.Company_Id = companyId;

            expenseTypeViewModel.Description = expenseTypeViewModel.Description.ToUpper();

            if (!ModelState.IsValid) return View(expenseTypeViewModel);

            var expenseType = _mapper.Map<ExpenseType>(expenseTypeViewModel);

            await _expenseTypeRepository.Add(expenseType);


            TempData["cls"] = "success";
            TempData["message"] = "Criado com sucesso !!";

            return RedirectToAction(nameof(Index));
        }

        // GET: Financeiro/ExpenseTypes/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var expenseType = _mapper.Map<ExpenseTypeViewModel>(await _expenseTypeRepository.GetById(id));

            if (expenseType == null)
            {
                return NotFound();
            }
            return View(expenseType);
        }

        // POST: Financeiro/ExpenseTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ExpenseTypeViewModel expenseTypeViewModel)
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            expenseTypeViewModel.Company_Id = companyId;

            expenseTypeViewModel.Description = expenseTypeViewModel.Description.ToUpper();

            if (id != expenseTypeViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(expenseTypeViewModel);

            var expenseType = _mapper.Map<ExpenseType>(expenseTypeViewModel);
            await _expenseTypeRepository.Update(expenseType);

            TempData["cls"] = "success";
            TempData["message"] = "Editado com sucesso !!";

            return RedirectToAction("Index");
        }

        // GET: Financeiro/ExpenseTypes/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var expenseType = await _expenseTypeRepository.GetById(id);

            if (expenseType == null)
            {
                return NotFound();
            }
            await _expenseTypeRepository.Remove(expenseType);

            TempData["cls"] = "success";
            TempData["message"] = "Deletado com sucesso !!";

            return RedirectToAction(nameof(Index));
        }

    }
}
