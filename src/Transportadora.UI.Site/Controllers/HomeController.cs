using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Transportadora.Business.Interfaces;
using Transportadora.UI.Site.AppServer;
using Transportadora.UI.Site.Models;
using Transportadora.UI.Site.ViewModels;

namespace Transportadora.UI.Site.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IFinancialSettlementRepository _financialSettlementRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IExpensePaymentRepository _expensePaymentRepository;
        private readonly IInvoicePaymentRepository _invoicePaymentRepository;

        private readonly IMapper _mapper;

        public HomeController(IFinancialSettlementRepository financialSettlementRepository,
            IEmployeeRepository employeeRepository,
                              IVehicleRepository vehicleRepository,
                              IExpensePaymentRepository expensePaymentRepository,
                              IInvoicePaymentRepository invoicePaymentRepository,
                              IMapper mapper)
        {
            _financialSettlementRepository = financialSettlementRepository;
            _employeeRepository = employeeRepository;
            _vehicleRepository = vehicleRepository;
            _expensePaymentRepository = expensePaymentRepository;
            _invoicePaymentRepository = invoicePaymentRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            var dashboard = new DashBoardViewModelcs();
            var employees = await _employeeRepository.GetByCompanyId(companyId); ;
            dashboard.TotalEmployees = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees).Count();
            var vehicles = await _vehicleRepository.GetByCompanyId(companyId);
            dashboard.TotalVehicles = _mapper.Map<IEnumerable<VehicleViewModel>>(vehicles).Count();
            var expenses = await _expensePaymentRepository.GetExpensePaymentClosedByCompanyId(companyId);
            dashboard.TotalExpenses = expenses.Sum(x => x.AmountExpensePayment);
            var financialSettlements = await _financialSettlementRepository.GetByCompanyId(companyId);
            var invoices = await _invoicePaymentRepository.GetInvoicePaymentClosedByCompanyId(companyId);
            dashboard.TotalInvoices = invoices.Sum(x => x.AmountInvoicePayment);
            dashboard.financialSettlementViewModels = _mapper.Map<IEnumerable<FinancialSettlementViewModel>>(financialSettlements.ToList());
            return View(dashboard);
        }


        [HttpGet]
        public async Task<IActionResult> AmountTravelTheCurrentYear()
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            var grouped = from p in await _financialSettlementRepository.Search(x => x.TravelDate.Year == DateTime.Now.Year && x.Company_Id == companyId)
                          group p by new { month = p.TravelDate.ToString("MMM", CultureInfo.CreateSpecificCulture("pt-br")), year = p.TravelDate.Year } into d
                          select new { dt = d.Key.month, count = d.Count() };

            return Json(grouped);
        }

        [HttpGet]
        public async Task<IActionResult> AmountTravelByRegion(string region)
        {
            var grouped = from p in await _financialSettlementRepository.Search(x => x.DestinationCity.State.Name == region)
                          group p by new { amount = p.TotalShipping } into d
                          select new { dt = d.Key.amount, count = d.Count() };

            return Json(grouped);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
