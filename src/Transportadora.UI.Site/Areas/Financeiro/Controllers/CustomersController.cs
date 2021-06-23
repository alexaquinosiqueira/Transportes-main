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
    public class CustomersController : Controller
    {
        private readonly ICustomerRepository _CustomerRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IFinancialSettlementRepository _FinancialSettlementRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IStateRepository _stateRepository;
        private readonly IMapper _mapper;
        public CustomersController(ICustomerRepository CustomerRepository,
                                IAddressRepository addressRepository,
                                IFinancialSettlementRepository financialSettlementRepository,
                                    ICityRepository cityRepository,
                                IStateRepository stateRepository,
                                IMapper mapper)
        {
            _CustomerRepository = CustomerRepository;
            _addressRepository = addressRepository;
            _cityRepository = cityRepository;
            _FinancialSettlementRepository = financialSettlementRepository;
            _stateRepository = stateRepository;
            _mapper = mapper;
        }

        // GET: Cadastro/Responsibilities
        public async Task<IActionResult> Index()
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);

            var users = await _CustomerRepository.GetByCompanyId(companyId);

            return View(_mapper.Map<IEnumerable<CustomerViewModel>>(users));
        }

        // GET: Cadastro/Responsibilities/Details/5
        public async Task<IActionResult> Details(Guid id)
        {


            var userViewModel = _mapper.Map<CustomerViewModel>(await _CustomerRepository.GetById(id));
            if (userViewModel == null)
            {
                return NotFound();
            }

            return View(userViewModel);
        }

        // GET: Cadastro/Responsibilities/Create
        public async Task<IActionResult> Create()
        {
            var states = await _stateRepository.GetAll();
            ViewData["States"] = _mapper.Map<IEnumerable<StateViewModel>>(states);

            return View();
        }

        // POST: Cadastro/Responsibilities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerViewModel CustomerViewModel)
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            CustomerViewModel.Company_Id = companyId;

            var states = await _stateRepository.GetAll();
            ViewData["States"] = _mapper.Map<IEnumerable<StateViewModel>>(states);

            if (!ModelState.IsValid) return View(CustomerViewModel);

            CustomerViewModel.Address_Id = CustomerViewModel.Address.Id;

            var Customer = _mapper.Map<Customer>(CustomerViewModel);

            await _CustomerRepository.Add(Customer);

            TempData["cls"] = "success";
            TempData["message"] = "Criado com sucesso !!";


            return RedirectToAction(nameof(Index));
        }

        // GET: Cadastro/Responsibilities/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var states = await _stateRepository.GetAll();
            ViewData["States"] = _mapper.Map<IEnumerable<StateViewModel>>(states);

            var CustomerViewModel = _mapper.Map<CustomerViewModel>(await _CustomerRepository.GetById(id));

            var cities = await _cityRepository.Search(c => c.Uf == CustomerViewModel.Address.State_id);
            ViewData["Cities"] = _mapper.Map<IEnumerable<CityViewModel>>(cities);

            if (CustomerViewModel == null)
            {
                return NotFound();
            }
            return View(CustomerViewModel);
        }

        // POST: Cadastro/Responsibilities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CustomerViewModel CustomerViewModel)
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            CustomerViewModel.Company_Id = companyId;

            if (id != CustomerViewModel.Id) return NotFound();

            if (CustomerViewModel.Document == null)
            {
                CustomerViewModel.Document = " ";
            }

            if (!ModelState.IsValid) return View(CustomerViewModel);

            var Customer = _mapper.Map<Customer>(CustomerViewModel);
            await _CustomerRepository.Update(Customer);

            var states = await _stateRepository.GetAll();
            ViewData["States"] = _mapper.Map<IEnumerable<StateViewModel>>(states);

            var cities = await _cityRepository.Search(c => c.Uf == CustomerViewModel.Address.State_id);
            ViewData["Cities"] = _mapper.Map<IEnumerable<CityViewModel>>(cities);

            TempData["cls"] = "success";
            TempData["message"] = "Editado com sucesso !!";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var CustomerViewModel = _mapper.Map<CustomerViewModel>(await _CustomerRepository.GetById(id));

            if (CustomerViewModel == null)
            {
                return NotFound();
            }

            //verifico se tem despesa para esse Customers.
            
            string cAcertos = string.Empty;
            string proximoAcerto = string.Empty;
            int nAcertos = 0;

            var expenser = await _FinancialSettlementRepository.GetAll();
            ViewData["FinancialSettlement"] = _mapper.Map<IEnumerable<FinancialSettlement>>(expenser).ToList();

            foreach (var item in _mapper.Map<IEnumerable<FinancialSettlement>>(expenser).ToList().Where(w => w.Customer_Id == id))
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
                TempData["message"] = "Não é possível excluir esse registro. Existe (m) " + nAcertos + " acerto (s) vinculado (s) a esse cliente.!! " + cAcertos;

                return RedirectToAction(nameof(Index));
            }
            else
            {
                await _CustomerRepository.Remove(await _CustomerRepository.GetById(id));

                TempData["cls"] = "success";
                TempData["message"] = "Deletado com sucesso !!";

                return RedirectToAction(nameof(Index));

            }

        }
    }
}
