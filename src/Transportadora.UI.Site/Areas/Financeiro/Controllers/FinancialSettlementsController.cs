using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Transportadora.Business.Interfaces;
using Transportadora.Business.Models;
using Transportadora.UI.Site.AppServer;
using Transportadora.UI.Site.Configurations;
using Transportadora.UI.Site.ViewModels;

namespace Transportadora.UI.Site.Areas.Financeiro.Controllers
{
    [Area("Financeiro")]
    public class FinancialSettlementsController : Controller
    {
        private readonly IFinancialSettlementRepository _financialSettlementRepository;
        private readonly IExpenseFinancialSettlementRepository _expenseFinancialSettlementRepository;
        private readonly IFleetRepository _fleetRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IStateRepository _stateRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IParameterRepository _parameterRepository;
        private readonly IInvoiceRepository _InvoiceRepository;
        private readonly IExpenseRepository _ExpenseRepository;
        private readonly IExpenseTypeRepository _expenseTypeRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IExpenseSupplierRepository _expenseSupplierRepository;
        private readonly IMapper _mapper;

        public FinancialSettlementsController(IFinancialSettlementRepository financialSettlementRepository,
                                IExpenseFinancialSettlementRepository expenseFinancialSettlementRepository,
                                IFleetRepository fleetRepository,
                                IVehicleRepository vehicleRepository,
                                IEmployeeRepository employeeRepository,
                                ICityRepository cityRepository,
                                IStateRepository stateRepository,
                                ISupplierRepository supplierRepository,
                                IParameterRepository parameterRepository,
                                IInvoiceRepository invoiceRepository,
                                IExpenseRepository expenseRepository,
                                IExpenseTypeRepository expenseTypeRepository,
                                ICustomerRepository customerRepository,
                                IExpenseSupplierRepository expenseSupplierRepository,
                                IMapper mapper)
        {
            _financialSettlementRepository = financialSettlementRepository;
            _expenseFinancialSettlementRepository = expenseFinancialSettlementRepository;
            _fleetRepository = fleetRepository;
            _vehicleRepository = vehicleRepository;
            _employeeRepository = employeeRepository;
            _cityRepository = cityRepository;
            _stateRepository = stateRepository;
            _supplierRepository = supplierRepository;
            _parameterRepository = parameterRepository;
            _InvoiceRepository = invoiceRepository;
            _ExpenseRepository = expenseRepository;
            _expenseTypeRepository = expenseTypeRepository;
            _expenseSupplierRepository = expenseSupplierRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        // GET: Financeiro/FinancialSettlements
        [Authorize(Roles = RolesConfig.GestaoAcertosListagem)]
        public async Task<IActionResult> Index()
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            var financialSettlements = await _financialSettlementRepository.GetByCompanyId(companyId);

            return View(_mapper.Map<IEnumerable<FinancialSettlementViewModel>>(financialSettlements));
        }

        // GET: Financeiro/FinancialSettlements/Details/5
        [Authorize(Roles = RolesConfig.GestaoAcertosListagem)]
        public async Task<IActionResult> Details(Guid id)
        {
            var financialSettlementViewModel = _mapper.Map<FinancialSettlementViewModel>(await _financialSettlementRepository.GetById(id));
            if (financialSettlementViewModel == null)
            {
                return NotFound();
            }

            return View(financialSettlementViewModel);
        }

        // GET: Financeiro/FinancialSettlements/Create
        [Authorize(Roles = RolesConfig.GestaoAcertosAdicionar)]
        public async Task<IActionResult> Create()
        {
            IdentityManager identityManager = new IdentityManager(User);

            var companyId = Guid.Parse(identityManager.CompanyID);
            var vehicles = await _vehicleRepository.GetByCompanyId(companyId);
            ViewData["Vehicles"] = _mapper.Map<IEnumerable<VehicleViewModel>>(vehicles);
            var fleets = await _fleetRepository.GetByCompanyId(companyId);
            ViewData["Fleets"] = _mapper.Map<IEnumerable<FleetViewModel>>(fleets);
            var employees = await _employeeRepository.GetByCompanyId(companyId);
            ViewData["Employees"] = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
            var states = await _stateRepository.GetAll();
            ViewData["States"] = _mapper.Map<IEnumerable<StateViewModel>>(states);
            var suppliers = await _supplierRepository.GetByCompanyId(companyId);
            ViewData["Suppliers"] = _mapper.Map<IEnumerable<SupplierViewModel>>(suppliers);
            var expenseType = await _expenseTypeRepository.GetByCompanyId(companyId);
            ViewData["ExpenseTypes"] = _mapper.Map<IEnumerable<ExpenseTypeViewModel>>(expenseType);
            var customers = await _customerRepository.GetByCompanyId(companyId);
            ViewData["Customers"] = _mapper.Map<IEnumerable<CustomerViewModel>>(customers);

            var parameter = await _parameterRepository.GetByCompanyId(companyId);
            var parameterViewModel = _mapper.Map<IEnumerable<ParameterViewModel>>(parameter).FirstOrDefault();

            FinancialSettlementViewModel financialSettlement = new FinancialSettlementViewModel();
            financialSettlement.Date = DateTime.Now;
            //financialSettlement.TravelDate = DateTime.Now;
            financialSettlement.Code = GetSequentialCode();
            if (parameterViewModel != null)
            {
                financialSettlement.CityOrigin_Id = parameterViewModel.CityOrigin_Id;
            }

            return View(financialSettlement);
        }

        private async Task<bool> FileUpload(IFormFile file, string imgPrefix)
        {
            if (file.Length <= 0) return false;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens", imgPrefix + file.FileName);

            if (System.IO.File.Exists(path))
            {
                ModelState.AddModelError(string.Empty, "Já existe um arquivo com esse nome!");
                return false;
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return true;
        }

        // POST: Financeiro/FinancialSettlements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RolesConfig.GestaoAcertosAdicionar)]
        public async Task<IActionResult> Create(FinancialSettlementViewModel financialSettlementViewModel)
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            financialSettlementViewModel.Company_Id = companyId;

            var vehicles = await _vehicleRepository.GetByCompanyId(companyId);
            ViewData["Vehicles"] = _mapper.Map<IEnumerable<VehicleViewModel>>(vehicles);
            var fleets = await _fleetRepository.GetByCompanyId(companyId);
            ViewData["Fleets"] = _mapper.Map<IEnumerable<FleetViewModel>>(fleets);
            var employees = await _employeeRepository.GetByCompanyId(companyId);
            ViewData["Employees"] = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
            var states = await _stateRepository.GetAll();
            ViewData["States"] = _mapper.Map<IEnumerable<StateViewModel>>(states);
            var suppliers = await _supplierRepository.GetByCompanyId(companyId);
            ViewData["Suppliers"] = _mapper.Map<IEnumerable<SupplierViewModel>>(suppliers);
            var expenseType = await _expenseTypeRepository.GetByCompanyId(companyId);
            ViewData["ExpenseTypes"] = _mapper.Map<IEnumerable<ExpenseTypeViewModel>>(expenseType);
            var customers = await _customerRepository.GetByCompanyId(companyId);
            ViewData["Customers"] = _mapper.Map<IEnumerable<CustomerViewModel>>(customers);

            var financialSettlement = _mapper.Map<FinancialSettlement>(financialSettlementViewModel);

            var imgPrefix = Guid.NewGuid() + "_";

            if (financialSettlement.Description == null)
                financialSettlement.Description = " ";

            if (financialSettlementViewModel.ImagemUpload != null)
            {
                if (!await FileUpload(financialSettlementViewModel.ImagemUpload, imgPrefix))
                {
                    return View(financialSettlementViewModel);
                }
                financialSettlement.Imagem = imgPrefix + financialSettlementViewModel.ImagemUpload.FileName;
            }

            await _financialSettlementRepository.Add(financialSettlement);

            await GerarReceita(financialSettlementViewModel);
            await GerarDespesa(financialSettlementViewModel);

            TempData["cls"] = "success";
            TempData["message"] = "Criado com sucesso !!";




            return RedirectToAction(nameof(Index));
        }

        private async Task GerarReceita(FinancialSettlementViewModel financialSettlement)
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            var parameter = await _parameterRepository.GetByCompanyId(companyId);
            var parameterViewModel = _mapper.Map<IEnumerable<ParameterViewModel>>(parameter).FirstOrDefault();

            if (parameterViewModel != null && financialSettlement != null)
            {
                InvoicePaymentViewModel advance = new InvoicePaymentViewModel()
                {
                    AmountInvoicePayment = financialSettlement.Advance,
                    ConcludedDate = DateTime.Now,
                    DueDateInvoicePayment = DateTime.Now,
                    StatusInvoicePayment = StatusViewModel.closed
                };

                InvoicePaymentViewModel incoming = new InvoicePaymentViewModel()
                {
                    AmountInvoicePayment = financialSettlement.TotalShipping - financialSettlement.Advance,
                    ConcludedDate = DateTime.Now.AddDays(15),
                    DueDateInvoicePayment = DateTime.Now.AddDays(15),
                    StatusInvoicePayment = StatusViewModel.opened
                };

                List<InvoicePaymentViewModel> invoicePaymentsViewModels = new List<InvoicePaymentViewModel>
                {
                    advance,
                    incoming
                };

                InvoiceViewModel InvoiceViewModel = new InvoiceViewModel()
                {
                    Id = new Guid(),
                    Description = financialSettlement.Code,
                    Observation = financialSettlement.Code,
                    Amount = financialSettlement.TotalShipping,
                    Date = financialSettlement.TravelDate,
                    DueDate = financialSettlement.TravelDate,
                    CostCenter_Id = parameterViewModel.Id_CostCenter,
                    BankAccount_Id = parameterViewModel.Id_BankAccount,
                    DocumentType_Id = parameterViewModel.Id_DocumentType,
                    PaymentForm_Id = parameterViewModel.Id_PaymentForm,
                    //DocumentNumber =Convert.ToInt32(financialSettlement.Code),
                    Customer_Id = financialSettlement.Customer_Id,
                    PaidAmount = financialSettlement.Advance,
                    InvoicePayments = invoicePaymentsViewModels,
                    Company_Id = companyId
                };

                var Invoice = _mapper.Map<Invoice>(InvoiceViewModel);

                await _InvoiceRepository.Add(Invoice);
            }
        }

        private async Task GerarDespesa(FinancialSettlementViewModel financialSettlement)
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            var parameter = await _parameterRepository.GetByCompanyId(companyId);
            var parameterViewModel = _mapper.Map<IEnumerable<ParameterViewModel>>(parameter).FirstOrDefault();

            string Cdesc = string.Empty;
            string Cdesc2 = string.Empty;

            if (parameterViewModel != null && financialSettlement != null && financialSettlement.ExpenseFinancialSettlements != null)
            {
                foreach (var expenseFinancial in financialSettlement.ExpenseFinancialSettlements)
                {

                    if (expenseFinancial.Litros > 0)
                        Cdesc = "Combustivel - Diesel ";
                    if (expenseFinancial.Litros == 0 && expenseFinancial.Arquivo != "Performance Motorista")
                        Cdesc = "Outras despesas ";
                    if (expenseFinancial.Litros == 0 && expenseFinancial.Arquivo == "Performance Motorista")
                        Cdesc = "Performance do Motorista ";

                    ExpensePaymentViewModel expensePayment = new ExpensePaymentViewModel()
                    {
                        AmountExpensePayment = expenseFinancial.Valor_Total,
                        ConcludedDate = financialSettlement.TravelDate,
                        DueDateExpensePayment = financialSettlement.TravelDate,
                        StatusExpensePayment = StatusViewModel.closed
                    };

                    List<ExpensePaymentViewModel> expensesPaymentsViewModels = new List<ExpensePaymentViewModel>
                    {
                        expensePayment
                    };
                    ExpenseViewModel expenseViewModel = new ExpenseViewModel()
                    {


                        Id = new Guid(),
                        Description = Cdesc + " - " + financialSettlement.Code,
                        Observation = financialSettlement.Code,
                        Amount = expenseFinancial.Valor_Total - expenseFinancial.Desconto,
                        Date = financialSettlement.TravelDate,
                        DueDate = financialSettlement.TravelDate,
                        CostCenter_Id = parameterViewModel.Id_CostCenter,
                        BankAccount_Id = parameterViewModel.Id_BankAccount,
                        DocumentType_Id = parameterViewModel.Id_DocumentType,
                        PaymentForm_Id = parameterViewModel.Id_PaymentForm,
                        //DocumentNumber =Convert.ToInt32(financialSettlement.Code),
                        Supplier_Id = expenseFinancial.Supplier_Id,
                        PaidAmount = financialSettlement.Advance,
                        Vehicle_Id = financialSettlement.Vehicle_Id,
                        ExpensePayment = expensesPaymentsViewModels,
                        Company_Id = companyId,
                        Id_Acerto = financialSettlement.Id

                    };

                    var expense = _mapper.Map<Expense>(expenseViewModel);

                    await _ExpenseRepository.Add(expense);
                }

            }

        }

        private async Task AtualizarDespesa(FinancialSettlementViewModel financialSettlement)
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            var parameter = await _parameterRepository.GetByCompanyId(companyId);
            var parameterViewModel = _mapper.Map<IEnumerable<ParameterViewModel>>(parameter).FirstOrDefault();

            string Cdesc = string.Empty;
            string Cdesc2 = string.Empty;

            if (parameterViewModel != null && financialSettlement != null && financialSettlement.ExpenseFinancialSettlements != null)
            {
                foreach (var expenseFinancial in financialSettlement.ExpenseFinancialSettlements)
                {

                    if (expenseFinancial.Litros > 0)
                        Cdesc = "Combustivel - Diesel ";
                    if (expenseFinancial.Litros == 0 && expenseFinancial.Arquivo != "Performance Motorista")
                        Cdesc = "Outras despesas ";
                    if (expenseFinancial.Litros == 0 && expenseFinancial.Arquivo == "Performance Motorista")
                        Cdesc = "Performance do Motorista ";

                    ExpensePaymentViewModel expensePayment = new ExpensePaymentViewModel()
                    {
                        AmountExpensePayment = expenseFinancial.Valor_Total,
                        ConcludedDate = financialSettlement.TravelDate,
                        DueDateExpensePayment = financialSettlement.TravelDate,
                        StatusExpensePayment = StatusViewModel.closed
                    };

                    List<ExpensePaymentViewModel> expensesPaymentsViewModels = new List<ExpensePaymentViewModel>
                    {
                        expensePayment
                    };
                    ExpenseViewModel expenseViewModel = new ExpenseViewModel()
                    {


                        Id = new Guid(),
                        Description = Cdesc + " - " + financialSettlement.Code,
                        Observation = financialSettlement.Code,
                        Amount = expenseFinancial.Valor_Total - expenseFinancial.Desconto,
                        Date = financialSettlement.TravelDate,
                        DueDate = financialSettlement.TravelDate,
                        CostCenter_Id = parameterViewModel.Id_CostCenter,
                        BankAccount_Id = parameterViewModel.Id_BankAccount,
                        DocumentType_Id = parameterViewModel.Id_DocumentType,
                        PaymentForm_Id = parameterViewModel.Id_PaymentForm,
                        //DocumentNumber =Convert.ToInt32(financialSettlement.Code),
                        Supplier_Id = expenseFinancial.Supplier_Id,
                        PaidAmount = financialSettlement.Advance,
                        Vehicle_Id = financialSettlement.Vehicle_Id,
                        ExpensePayment = expensesPaymentsViewModels,
                        Company_Id = companyId,
                        Id_Acerto = financialSettlement.Id

                    };

                    var expense = _mapper.Map<Expense>(expenseViewModel);

                    await _ExpenseRepository.Update(expense);
                }

            }

        }


        // GET: Financeiro/FinancialSettlements/Edit/5
        [Authorize(Roles = RolesConfig.GestaoAcertosEditar)]
        public async Task<IActionResult> Edit(Guid id)
        {
            IdentityManager identityManager = new IdentityManager(User);

            var companyId = Guid.Parse(identityManager.CompanyID);
            var vehicles = await _vehicleRepository.GetByCompanyId(companyId);
            ViewData["Vehicles"] = _mapper.Map<IEnumerable<VehicleViewModel>>(vehicles);
            var fleets = await _fleetRepository.GetByCompanyId(companyId);
            ViewData["Fleets"] = _mapper.Map<IEnumerable<FleetViewModel>>(fleets);
            var employees = await _employeeRepository.GetByCompanyId(companyId);
            ViewData["Employees"] = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
            var states = await _stateRepository.GetAll();
            ViewData["States"] = _mapper.Map<IEnumerable<StateViewModel>>(states);
            var suppliers = await _supplierRepository.GetByCompanyId(companyId);
            ViewData["Suppliers"] = _mapper.Map<IEnumerable<SupplierViewModel>>(suppliers);
            var expenseType = await _expenseTypeRepository.GetByCompanyId(companyId);
            ViewData["ExpenseTypes"] = _mapper.Map<IEnumerable<ExpenseTypeViewModel>>(expenseType);
            var customers = await _customerRepository.GetByCompanyId(companyId);
            ViewData["Customers"] = _mapper.Map<IEnumerable<CustomerViewModel>>(customers);

            var financialSettlementViewModel = _mapper.Map<FinancialSettlementViewModel>(await _financialSettlementRepository.GetByIdDetached(id));

            var CitiesOrigin = await _cityRepository.Search(c => c.Uf == financialSettlementViewModel.CityOrigin.State.Id);
            ViewData["CitiesOrigin"] = _mapper.Map<IEnumerable<CityViewModel>>(CitiesOrigin);

            var DestinationCities = await _cityRepository.Search(c => c.Uf == financialSettlementViewModel.DestinationCity.State.Id);
            ViewData["DestinationCities"] = _mapper.Map<IEnumerable<CityViewModel>>(DestinationCities);

            if (financialSettlementViewModel == null)
            {
                return NotFound();
            }
            return View(financialSettlementViewModel);
        }


        [Authorize(Roles = RolesConfig.GestaoAcertosEditar)]
        public async Task<IActionResult> Visualizacao(string code)
        {
            IdentityManager identityManager = new IdentityManager(User);

            var companyId = Guid.Parse(identityManager.CompanyID);
            var vehicles = await _vehicleRepository.GetByCompanyId(companyId);
            ViewData["Vehicles"] = _mapper.Map<IEnumerable<VehicleViewModel>>(vehicles);
            var fleets = await _fleetRepository.GetByCompanyId(companyId);
            ViewData["Fleets"] = _mapper.Map<IEnumerable<FleetViewModel>>(fleets);
            var employees = await _employeeRepository.GetByCompanyId(companyId);
            ViewData["Employees"] = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
            var states = await _stateRepository.GetAll();
            ViewData["States"] = _mapper.Map<IEnumerable<StateViewModel>>(states);
            var suppliers = await _supplierRepository.GetByCompanyId(companyId);
            ViewData["Suppliers"] = _mapper.Map<IEnumerable<SupplierViewModel>>(suppliers);
            var expenseType = await _expenseTypeRepository.GetByCompanyId(companyId);
            ViewData["ExpenseTypes"] = _mapper.Map<IEnumerable<ExpenseTypeViewModel>>(expenseType);
            var customers = await _customerRepository.GetByCompanyId(companyId);
            ViewData["Customers"] = _mapper.Map<IEnumerable<CustomerViewModel>>(customers);

            var acertos = await _financialSettlementRepository.GetAll();

            acertos.ToList();
            Guid id = default(Guid);

            foreach (var item in acertos)
            {
                if (item.Company_Id == companyId)
                {
                    if (item.Code == code)
                    {
                        id = item.Id;
                    }
                }
            }

            var financialSettlementViewModel = _mapper.Map<FinancialSettlementViewModel>(await _financialSettlementRepository.GetByIdDetached(id));

            var CitiesOrigin = await _cityRepository.Search(c => c.Uf == financialSettlementViewModel.CityOrigin.State.Id);
            ViewData["CitiesOrigin"] = _mapper.Map<IEnumerable<CityViewModel>>(CitiesOrigin);

            var DestinationCities = await _cityRepository.Search(c => c.Uf == financialSettlementViewModel.DestinationCity.State.Id);
            ViewData["DestinationCities"] = _mapper.Map<IEnumerable<CityViewModel>>(DestinationCities);

            if (financialSettlementViewModel == null)
            {
                return NotFound();
            }
            return View(financialSettlementViewModel);
        }









        // POST: Financeiro/FinancialSettlements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RolesConfig.GestaoAcertosEditar)]
        public async Task<IActionResult> Edit(Guid id, FinancialSettlementViewModel financialSettlementViewModel)
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            financialSettlementViewModel.Company_Id = companyId;

            if (id != financialSettlementViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(financialSettlementViewModel);

            var Expense = _mapper.Map<FinancialSettlement>(financialSettlementViewModel);


            if (Expense.Description == null)
                Expense.Description = " ";

            await _financialSettlementRepository.Update(Expense);

            await AtualizarDespesa(financialSettlementViewModel);


            var vehicles = await _vehicleRepository.GetByCompanyId(companyId);
            ViewData["Vehicles"] = _mapper.Map<IEnumerable<VehicleViewModel>>(vehicles);
            var fleets = await _fleetRepository.GetByCompanyId(companyId);
            ViewData["Fleets"] = _mapper.Map<IEnumerable<FleetViewModel>>(fleets);
            var employees = await _employeeRepository.GetByCompanyId(companyId);
            ViewData["Employees"] = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
            var states = await _stateRepository.GetAll();
            ViewData["States"] = _mapper.Map<IEnumerable<StateViewModel>>(states);
            var suppliers = await _supplierRepository.GetByCompanyId(companyId);
            ViewData["Suppliers"] = _mapper.Map<IEnumerable<SupplierViewModel>>(suppliers);
            var expenseType = await _expenseTypeRepository.GetByCompanyId(companyId);
            ViewData["ExpenseTypes"] = _mapper.Map<IEnumerable<ExpenseTypeViewModel>>(expenseType);
            var customers = await _customerRepository.GetByCompanyId(companyId);
            ViewData["Customers"] = _mapper.Map<IEnumerable<CustomerViewModel>>(customers);

            TempData["cls"] = "success";
            TempData["message"] = "Editado com sucesso !!";

            return RedirectToAction("Index");
        }

        // GET: Financeiro/FinancialSettlements/Delete/5
        [HttpGet]
        [Authorize(Roles = RolesConfig.GestaoAcertosRemover)]
        public async Task<IActionResult> Delete(Guid id)
        {

            var financialSettlementViewModel = _mapper.Map<FinancialSettlementViewModel>(await _financialSettlementRepository.GetById(id));

            if (financialSettlementViewModel == null)
            {
                return NotFound();
            }

            //verifico se tem lançamento para esse acerto.

            string cAcertos = string.Empty;
            string proximoAcerto = string.Empty;
            int nAcertos = 0;

            var expenserfin = await _expenseFinancialSettlementRepository.GetAll();
            ViewData["ExpenseFinancialSettlement"] = _mapper.Map<IEnumerable<ExpenseFinancialSettlement>>(expenserfin).ToList();

            var expenser = await _ExpenseRepository.GetAll();
            ViewData["ExpenseRepository"] = _mapper.Map<IEnumerable<Expense>>(expenser).ToList();


            foreach (var item in _mapper.Map<IEnumerable<ExpenseFinancialSettlement>>(expenserfin).ToList().Where(w => w.FinancialSettlement_Id == id))
            {
                cAcertos = cAcertos + " - " + Environment.NewLine + item.Valor_Total;
                nAcertos = nAcertos + 1;
            }

            if (nAcertos > 0)
            {
                TempData["cls"] = "danger";
                TempData["message"] = "Não é possível excluir esse registro. Existe (m) " + nAcertos + " lançamento (s) vinculado (s) a esse acerto.!! " + cAcertos;

                return RedirectToAction(nameof(Index));
            }
            else
            {
                await _financialSettlementRepository.Remove(await _financialSettlementRepository.GetById(id));

                TempData["cls"] = "success";
                TempData["message"] = "Deletado com sucesso !!";

                return RedirectToAction(nameof(Index));
            }

        }
        [HttpGet]
        public async Task<IActionResult> GetCityByStateId(int StateId, string q = "")
        {
            if (q == null)
                q = "";

            var cities = await _cityRepository.Search(c => c.Uf == StateId && c.Name.Contains(q));
            var valuesReturn = cities.Select(c =>
                new
                {
                    id = c.Id,
                    text = c.Name
                }
            );
            return Json(valuesReturn);
        }

        [HttpGet]
        public async Task<IActionResult> GetVehicleId(Guid frota)
        {
            var veiculo = await _vehicleRepository.Search(c => c.Fleet_Id == frota);

            var valuesReturn = veiculo.Select(c =>
                new
                {
                    id = c.Id,
                    text = c.VehicleLicensePlate + " - " + c.Description,
                    idmoto = c.Employee_Id,
                }
            ).OrderBy(o => o.text);

            return Json(valuesReturn);
        }
        public async Task<IActionResult> GetVehicleIdP(Guid frota)
        {
            var veiculo = await _vehicleRepository.Search(c => c.Id == frota);

            var valuesReturn = veiculo.Select(c =>
                new
                {
                    idmoto = c.Employee_Id,
                }
            ).OrderBy(o => o.idmoto);

            return Json(valuesReturn);
        }
        public async Task<IActionResult> GetMotoristaId(Guid idmoto)
        {
            var motorista = await _employeeRepository.Search(c => c.Id == idmoto);
            var valuesReturn = motorista.Select(c =>
                new
                {
                    id = c.Id,
                    text = c.Name
                }
            ).OrderBy(o => o.text);
            return Json(valuesReturn);
        }
        public async Task<IActionResult> GetVehicleAll()
        {
            var veiculo = await _vehicleRepository.GetAll();
            var valuesReturn = veiculo.Select(c =>
                new
                {
                    id = c.Id,
                    text = c.VehicleLicensePlate + " - " + c.Description,
                    idmotor = c.Employee_Id,
                }
            );
            return Json(valuesReturn);
        }
        public async Task<IActionResult> GetMotoristaAll()
        {
            var motoista = await _employeeRepository.GetAll();
            var valuesReturn = motoista.Select(c =>
                new
                {
                    id = c.Id,
                    text = c.Name
                }
            );
            return Json(valuesReturn);
        }
        public async Task<IActionResult> GetFornecedor_Tipo(Guid Id_Tipo_Despesa)
        {

            var fornecedor = await _expenseSupplierRepository.Search(c => c.ExpenseType_Id == Id_Tipo_Despesa);
            var valuesReturn = fornecedor.Select(c =>
                new
                {
                    id = c.Supplier_Id,
                    text = c.Supplier.Name
                }
            ).OrderBy(o => o.text).Distinct();
            return Json(valuesReturn);
        }
        public async Task<IActionResult> GetSegmento(Guid Id_Tipo_Despesa)
        {
            var segmento = await _expenseTypeRepository.Search(c => c.Id == Id_Tipo_Despesa);
            var valuesReturn = segmento.Select(c =>
                new
                {
                    segmento = c.Segmento
                }
            );
            return Json(valuesReturn);
        }

        public async Task<IActionResult> Getempl()
        {
            var veiculo = await _vehicleRepository.GetAll();
            var valuesReturn = veiculo.Select(c =>
                new
                {
                    id = c.Id,
                    text = c.VehicleLicensePlate + " - " + c.Description,
                }
            );
            return Json(valuesReturn);
        }

        [HttpGet]
        public async Task<IActionResult> AverageFuelConsumption(decimal distance, Guid vehicleId)
        {
            decimal averageFuelConsumption = 0.0M;
            var vehicle = await _vehicleRepository.GetById(vehicleId);
            if (vehicle != null)
            {
                averageFuelConsumption = distance / vehicle.average;
            }

            return Json(averageFuelConsumption);
        }
        private string GetSequentialCode()
        {
            return string.Format($"{DateTime.Now.Year}{DateTime.Now.Month}{DateTime.Now.Day}{DateTime.Now.Hour}{DateTime.Now.Minute}{DateTime.Now.Second}{DateTime.Now.Millisecond}");
        }


    }
}
