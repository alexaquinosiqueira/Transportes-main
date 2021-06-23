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
    public class ExpensesController : Controller
    {
        private readonly ICostcenterRepository _CostcenterRepository;
        private readonly IBankAccountRepository _BankAccountRepository;
        private readonly IDocumentTypeRepository _DocumentTypeRepository;
        private readonly IPaymentFormRepository _PaymentFormRepository;
        private readonly ISupplierRepository _SupplierRepository;
        private readonly IExpenseRepository _ExpenseRepository;
        private readonly IVehicleRepository _VehicleRepository;
        private readonly IMapper _mapper;
        public ExpensesController(IExpenseRepository ExpenseRepository,
            ICostcenterRepository CostcenterRepository,
            IBankAccountRepository BankAccountRepository,
            IDocumentTypeRepository DocumentTypeRepository,
            IPaymentFormRepository PaymentFormRepository,
            ISupplierRepository SupplierRepository,
            IVehicleRepository VehicleRepository,
        IMapper mapper)
        {
            _CostcenterRepository = CostcenterRepository;
            _BankAccountRepository = BankAccountRepository;
            _DocumentTypeRepository = DocumentTypeRepository;
            _PaymentFormRepository = PaymentFormRepository;
            _SupplierRepository = SupplierRepository;
            _ExpenseRepository = ExpenseRepository;
            _VehicleRepository = VehicleRepository;
            _mapper = mapper;
        }

        // GET: Cadastro/Responsibilities
        public async Task<IActionResult> Index()
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);


            var users = await _ExpenseRepository.GetByCompanyId(companyId);

            return View(_mapper.Map<IEnumerable<ExpenseViewModel>>(users));
        }

        // GET: Cadastro/Responsibilities/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var userViewModel = _mapper.Map<ExpenseViewModel>(await _ExpenseRepository.GetById(id));
            if (userViewModel == null)
            {
                return NotFound();
            }

            return View(userViewModel);
        }

        // GET: Cadastro/Responsibilities/Create
        public async Task<IActionResult> Create()
        {
            var costcenters = await _CostcenterRepository.GetAll();
            ViewData["CostCenters"] = _mapper.Map<IEnumerable<CostCenterViewModel>>(costcenters);
            var bankAccounts = await _BankAccountRepository.GetAll();
            ViewData["BankAccounts"] = _mapper.Map<IEnumerable<BankAccountViewModel>>(bankAccounts);
            var documentTypes = await _DocumentTypeRepository.GetAll();
            ViewData["DocumentTypes"] = _mapper.Map<IEnumerable<DocumentTypeViewModel>>(documentTypes);
            var paymentForms = await _PaymentFormRepository.GetAll();
            ViewData["PaymentForms"] = _mapper.Map<IEnumerable<PaymentFormViewModel>>(paymentForms);
            var suppliers = await _SupplierRepository.GetAll();
            ViewData["Suppliers"] = _mapper.Map<IEnumerable<SupplierViewModel>>(suppliers);
            var vehicle = await _VehicleRepository.GetAll();
            ViewData["Vehicle"] = _mapper.Map<IEnumerable<VehicleViewModel>>(vehicle);
            return View();


        }

        // POST: Cadastro/Responsibilities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExpenseViewModel ExpenseViewModel)
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            ExpenseViewModel.Company_Id = companyId;

            ExpenseViewModel.Date = DateTime.Now;

            if (!ModelState.IsValid) return View(ExpenseViewModel);

            var Expense = _mapper.Map<Expense>(ExpenseViewModel);

            if (Expense.Description == null)
                Expense.Description = " ";

            if (Expense.Observation == null)
                Expense.Observation = " "; 

            await _ExpenseRepository.Add(Expense);

            TempData["cls"] = "success";
            TempData["message"] = "Criado com sucesso !!";

            return RedirectToAction(nameof(Index));
        }

        // GET: Cadastro/Responsibilities/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var costcenters = await _CostcenterRepository.GetAll();
            ViewData["CostCenters"] = _mapper.Map<IEnumerable<CostCenterViewModel>>(costcenters);
            var bankAccounts = await _BankAccountRepository.GetAll();
            ViewData["BankAccounts"] = _mapper.Map<IEnumerable<BankAccountViewModel>>(bankAccounts);
            var documentTypes = await _DocumentTypeRepository.GetAll();
            ViewData["DocumentTypes"] = _mapper.Map<IEnumerable<DocumentTypeViewModel>>(documentTypes);
            var paymentForms = await _PaymentFormRepository.GetAll();
            ViewData["PaymentForms"] = _mapper.Map<IEnumerable<PaymentFormViewModel>>(paymentForms);
            var suppliers = await _SupplierRepository.GetAll();
            ViewData["Suppliers"] = _mapper.Map<IEnumerable<SupplierViewModel>>(suppliers);
            var vehicle = await _VehicleRepository.GetAll();
            ViewData["Vehicle"] = _mapper.Map<IEnumerable<VehicleViewModel>>(vehicle);

            var ExpenseViewModel = _mapper.Map<ExpenseViewModel>(await _ExpenseRepository.GetById(id));

            if (ExpenseViewModel == null)
            {
                return NotFound();
            }
            return View(ExpenseViewModel);
        }

        // POST: Cadastro/Responsibilities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ExpenseViewModel ExpenseViewModel)
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            ExpenseViewModel.Company_Id = companyId;

            if (id != ExpenseViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(ExpenseViewModel);

            var Expense = _mapper.Map<Expense>(ExpenseViewModel);
            await _ExpenseRepository.Update(Expense);

            TempData["cls"] = "success";
            TempData["message"] = "Alterado com sucesso !!";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var ExpenseViewModel = _mapper.Map<ExpenseViewModel>(await _ExpenseRepository.GetById(id));

            if (ExpenseViewModel == null)
            {
                return NotFound();
            }
            await _ExpenseRepository.Remove(await _ExpenseRepository.GetById(id));

            TempData["cls"] = "success";
            TempData["message"] = "Deletado com sucesso !!";

            return RedirectToAction(nameof(Index));
        }
    }
}
