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
    public class BankAccountsController : Controller
    {
        private readonly IBankAccountRepository _BankAccountRepository;
        private readonly IBankRepository _BankRepository;
        private readonly IMapper _mapper;
        public BankAccountsController(IBankAccountRepository BankAccountRepository,
                                IBankRepository BankRepository,
                                IMapper mapper)
        {
            _BankAccountRepository = BankAccountRepository;
            _BankRepository = BankRepository;
            _mapper = mapper;
        }

        // GET: Cadastro/Responsibilities
        public async Task<IActionResult> Index()
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            var users = await _BankAccountRepository.GetByCompanyId(companyId);

            return View(_mapper.Map<IEnumerable<BankAccountViewModel>>(users));
        }

        // GET: Cadastro/Responsibilities/Details/5
        public async Task<IActionResult> Details(Guid id)
        {


            var userViewModel = _mapper.Map<BankAccountViewModel>(await _BankAccountRepository.GetById(id));
            if (userViewModel == null)
            {
                return NotFound();
            }

            return View(userViewModel);
        }

        // GET: Cadastro/Responsibilities/Create
        public async Task<IActionResult> Create()
        {
            var banks = await _BankRepository.GetAll();
            ViewData["Banks"] = _mapper.Map<IEnumerable<BankViewModel>>(banks);
            return View();
        }

        // POST: Cadastro/Responsibilities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BankAccountViewModel BankAccountViewModel)
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            BankAccountViewModel.Company_Id = companyId;

            if (!ModelState.IsValid) return View(BankAccountViewModel);

            var BankAccount = _mapper.Map<BankAccount>(BankAccountViewModel);

            await _BankAccountRepository.Add(BankAccount);

            return RedirectToAction(nameof(Index));
        }

        // GET: Cadastro/Responsibilities/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var banks = await _BankRepository.GetAll();
            ViewData["Banks"] = _mapper.Map<IEnumerable<BankViewModel>>(banks);

            var BankAccountViewModel = _mapper.Map<BankAccountViewModel>(await _BankAccountRepository.GetById(id));

            if (BankAccountViewModel == null)
            {
                return NotFound();
            }
            return View(BankAccountViewModel);
        }

        // POST: Cadastro/Responsibilities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, BankAccountViewModel BankAccountViewModel)
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            BankAccountViewModel.Company_Id = companyId;

            if (id != BankAccountViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(BankAccountViewModel);

            var BankAccount = _mapper.Map<BankAccount>(BankAccountViewModel);
            await _BankAccountRepository.Update(BankAccount);


            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var BankAccountViewModel = _mapper.Map<BankAccountViewModel>(await _BankAccountRepository.GetById(id));

            if (BankAccountViewModel == null)
            {
                return NotFound();
            }
            await _BankAccountRepository.Remove(await _BankAccountRepository.GetById(id));

            TempData["cls"] = "success";
            TempData["message"] = "Deletado com sucesso !!";

            return RedirectToAction(nameof(Index));
        }
    }
}
