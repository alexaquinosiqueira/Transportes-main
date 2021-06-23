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
    public class BanksController : Controller
    {
        private readonly IBankRepository _BankRepository;
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly IMapper _mapper;
        public BanksController(IBankRepository BankRepository,
                               IBankAccountRepository bankAccountRepository,

                                IMapper mapper)
        {
            _BankRepository = BankRepository;
            _bankAccountRepository = bankAccountRepository;
            _mapper = mapper;
        }

        // GET: Cadastro/Responsibilities
        public async Task<IActionResult> Index()
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            var users = await _BankRepository.GetByCompanyId(companyId);

            return View(_mapper.Map<IEnumerable<BankViewModel>>(users));
        }

        // GET: Cadastro/Responsibilities/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var userViewModel = _mapper.Map<BankViewModel>(await _BankRepository.GetById(id));
            if (userViewModel == null)
            {
                return NotFound();
            }

            return View(userViewModel);
        }

        // GET: Cadastro/Responsibilities/Create
        public async Task< IActionResult> Create()
        {
            var bankaccount = await _bankAccountRepository.GetAll();
            ViewData["BankAccount"] = _mapper.Map<IEnumerable<BankAccountViewModel>>(bankaccount);

            return View();
        }

        // POST: Cadastro/Responsibilities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BankViewModel BankViewModel)
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            BankViewModel.Company_Id = companyId;

            var bankaccount = await _bankAccountRepository.GetAll();
            ViewData["BankAccount"] = _mapper.Map<IEnumerable<BankAccountViewModel>>(bankaccount);

            if (!ModelState.IsValid) return View(BankViewModel);

            var Bank = _mapper.Map<Bank>(BankViewModel);

            await _BankRepository.Add(Bank);

            TempData["cls"] = "success";
            TempData["message"] = "Criado com sucesso !!";

            return RedirectToAction(nameof(Index));
        }

        // GET: Cadastro/Responsibilities/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var BankViewModel = _mapper.Map<BankViewModel>(await _BankRepository.GetById(id));

            var bankaccount = await _bankAccountRepository.GetAll();
            ViewData["BankAccount"] = _mapper.Map<IEnumerable<BankAccountViewModel>>(bankaccount);

            if (BankViewModel == null)
            {
                return NotFound();
            }
            return View(BankViewModel);
        }

        // POST: Cadastro/Responsibilities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, BankViewModel BankViewModel)
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            BankViewModel.Company_Id = companyId;

            if (id != BankViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(BankViewModel);

            var Bank = _mapper.Map<Bank>(BankViewModel);
            await _BankRepository.Update(Bank);

            var bankaccount = await _bankAccountRepository.GetAll();
            ViewData["BankAccount"] = _mapper.Map<IEnumerable< BankAccountViewModel>>(bankaccount);

            TempData["cls"] = "success";
            TempData["message"] = "Editado com sucesso !!";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var BankViewModel = _mapper.Map<BankViewModel>(await _BankRepository.GetById(id));

            if (BankViewModel == null)
            {
                return NotFound();
            }
            await _BankRepository.Remove(await _BankRepository.GetById(id));

            TempData["cls"] = "success";
            TempData["message"] = "Deletado com sucesso !!";

            return RedirectToAction(nameof(Index));
        }
    }
}
