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
    public class InvoicesController : Controller
    {
        private readonly ICostcenterRepository _CostcenterRepository;
        private readonly IBankAccountRepository _BankAccountRepository;
        private readonly IDocumentTypeRepository _DocumentTypeRepository;
        private readonly IPaymentFormRepository _PaymentFormRepository;
        private readonly ICustomerRepository _CustomerRepository;
        private readonly IInvoiceRepository _InvoiceRepository;
        private readonly IMapper _mapper;
        public InvoicesController(IInvoiceRepository InvoiceRepository,
                        ICostcenterRepository CostcenterRepository,
                        IBankAccountRepository BankAccountRepository,
                        IDocumentTypeRepository DocumentTypeRepository,
                        IPaymentFormRepository PaymentFormRepository,
                        ICustomerRepository CustomerRepository,
                                IMapper mapper)
        {
            _CostcenterRepository = CostcenterRepository;
            _BankAccountRepository = BankAccountRepository;
            _DocumentTypeRepository = DocumentTypeRepository;
            _PaymentFormRepository = PaymentFormRepository;
            _CustomerRepository = CustomerRepository;
            _InvoiceRepository = InvoiceRepository;
            _mapper = mapper;
        }

        // GET: Cadastro/Responsibilities
        public async Task<IActionResult> Index()
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            var users = await _InvoiceRepository.GetByCompanyId(companyId);

            return View(_mapper.Map<IEnumerable<InvoiceViewModel>>(users));
        }

        // GET: Cadastro/Responsibilities/Details/5
        public async Task<IActionResult> Details(Guid id)
        {


            var userViewModel = _mapper.Map<InvoiceViewModel>(await _InvoiceRepository.GetById(id));
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
            var customers = await _CustomerRepository.GetAll();
            ViewData["Customers"] = _mapper.Map<IEnumerable<CustomerViewModel>>(customers);
            return View();
        }

        // POST: Cadastro/Responsibilities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InvoiceViewModel InvoiceViewModel)
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            InvoiceViewModel.Company_Id = companyId;

            InvoiceViewModel.Date = DateTime.Now;

            if (!ModelState.IsValid) return View(InvoiceViewModel);

            if (InvoiceViewModel.Observation == null)
                InvoiceViewModel.Observation = "Conforme documento N.º " + InvoiceViewModel.DocumentNumber;

            var Invoice = _mapper.Map<Invoice>(InvoiceViewModel);

            await _InvoiceRepository.Add(Invoice);

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
            var customers = await _CustomerRepository.GetAll();
            ViewData["Customers"] = _mapper.Map<IEnumerable<CustomerViewModel>>(customers);

            var InvoiceViewModel = _mapper.Map<InvoiceViewModel>(await _InvoiceRepository.GetById(id));

            if (InvoiceViewModel == null)
            {
                return NotFound();
            }
            return View(InvoiceViewModel);
        }

        // POST: Cadastro/Responsibilities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, InvoiceViewModel InvoiceViewModel)
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            InvoiceViewModel.Company_Id = companyId;

            if (id != InvoiceViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(InvoiceViewModel);

            var Invoice = _mapper.Map<Invoice>(InvoiceViewModel);
            await _InvoiceRepository.Update(Invoice);

            TempData["cls"] = "success";
            TempData["message"] = "Alterado com sucesso !!";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var InvoiceViewModel = _mapper.Map<InvoiceViewModel>(await _InvoiceRepository.GetById(id));

            if (InvoiceViewModel == null)
            {
                return NotFound();
            }
            await _InvoiceRepository.Remove(await _InvoiceRepository.GetById(id));

            TempData["cls"] = "success";
            TempData["message"] = "Deletado com sucesso !!";

            return RedirectToAction(nameof(Index));
        }
    }
}
