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
    public class SuppliersController : Controller
    {
        private readonly ISupplierRepository _SupplierRepository;
        private readonly IExpenseFinancialSettlementRepository _ExpenseFinancialSettlementRepository;
        private readonly IExpenseTypeRepository _expenseTypeRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IStateRepository _stateRepository;
        private readonly IMapper _mapper;
        public SuppliersController(ISupplierRepository SupplierRepository,
                                IAddressRepository addressRepository,
                                IExpenseFinancialSettlementRepository expenseFinancialSettlementRepository,
                                    ICityRepository cityRepository,
                                IExpenseTypeRepository expenseTypeRepository,
                                IStateRepository stateRepository,
                                IMapper mapper)
        {
            _SupplierRepository = SupplierRepository;
            _addressRepository = addressRepository;
            _cityRepository = cityRepository;
            _ExpenseFinancialSettlementRepository = expenseFinancialSettlementRepository;
            _expenseTypeRepository = expenseTypeRepository;
            _stateRepository = stateRepository;
            _mapper = mapper;
        }

        // GET: Cadastro/Responsibilities
        public async Task<IActionResult> Index()
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);

            var users = await _SupplierRepository.GetByCompanyId(companyId);

            return View(_mapper.Map<IEnumerable<SupplierViewModel>>(users));
        }

        // GET: Cadastro/Responsibilities/Details/5
        public async Task<IActionResult> Details(Guid id)
        {


            var userViewModel = _mapper.Map<SupplierViewModel>(await _SupplierRepository.GetById(id));



            if (userViewModel == null)
            {
                return NotFound();
            }

            return View(userViewModel);
        }

        // GET: Cadastro/Responsibilities/Create
        public async Task <IActionResult> Create()
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);

            var states = await _stateRepository.GetAll();
            ViewData["States"] = _mapper.Map<IEnumerable<StateViewModel>>(states);

            var expenseType = await _expenseTypeRepository.GetByCompanyId(companyId);
            ViewData["ExpenseTypes"] = _mapper.Map<IEnumerable<ExpenseTypeViewModel>>(expenseType);

            return View();
        }

        // POST: Cadastro/Responsibilities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SupplierViewModel SupplierViewModel)
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            SupplierViewModel.Company_Id = companyId;

            var states = await _stateRepository.GetAll();
            ViewData["States"] = _mapper.Map<IEnumerable<StateViewModel>>(states);

            var expenseType = await _expenseTypeRepository.GetByCompanyId(companyId);
            ViewData["ExpenseTypes"] = _mapper.Map<IEnumerable<ExpenseTypeViewModel>>(expenseType);

            if (!ModelState.IsValid) return View(SupplierViewModel);

            var Supplier = _mapper.Map<Supplier>(SupplierViewModel);

            await _SupplierRepository.Add(Supplier);

            TempData["cls"] = "success";
            TempData["message"] = "Criado com sucesso !!";

            return RedirectToAction(nameof(Index));
        }

        // GET: Cadastro/Responsibilities/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            IdentityManager identityManager = new IdentityManager(User);

            var companyId = Guid.Parse(identityManager.CompanyID);

            var SupplierViewModel = _mapper.Map<SupplierViewModel>(await _SupplierRepository.GetById(id));

            var states = await _stateRepository.GetAll();
            ViewData["States"] = _mapper.Map<IEnumerable<StateViewModel>>(states);

            var cities = await _cityRepository.Search(c => c.Uf == SupplierViewModel.Address.State_id);
            ViewData["Cities"] = _mapper.Map<IEnumerable<CityViewModel>>(cities);

            var expenseType = await _expenseTypeRepository.GetByCompanyId(companyId);
            ViewData["ExpenseTypes"] = _mapper.Map<IEnumerable<ExpenseTypeViewModel>>(expenseType);


            if (SupplierViewModel == null)
            {
                return NotFound();
            }
            return View(SupplierViewModel);
        }

        // POST: Cadastro/Responsibilities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, SupplierViewModel SupplierViewModel)
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            SupplierViewModel.Company_Id = companyId;

            if (id != SupplierViewModel.Id) return NotFound();

            var states = await _stateRepository.GetAll();
            var cities = await _cityRepository.Search(c => c.Uf == SupplierViewModel.Address.State_id);


            if (!ModelState.IsValid)
            {
                var expenseType = await _expenseTypeRepository.GetByCompanyId(companyId);
                ViewData["ExpenseTypes"] = _mapper.Map<IEnumerable<ExpenseTypeViewModel>>(expenseType);

                ViewData["States"] = _mapper.Map<IEnumerable<StateViewModel>>(states);

                ViewData["Cities"] = _mapper.Map<IEnumerable<CityViewModel>>(cities);

                TempData["cls"] = "warning";
                TempData["message"] = "Existem campos obrigatórios vazios !!";

            }

            if (!ModelState.IsValid) return View(SupplierViewModel);

            var Supplier = _mapper.Map<Supplier>(SupplierViewModel);

            if (Supplier.Document == null)
                Supplier.Document = " ";

            await _SupplierRepository.Update(Supplier);

            ViewData["States"] = _mapper.Map<IEnumerable<StateViewModel>>(states);

            ViewData["Cities"] = _mapper.Map<IEnumerable<CityViewModel>>(cities);


            TempData["cls"] = "success";
            TempData["message"] = "Editado com sucesso !!";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var SupplierViewModel = _mapper.Map<SupplierViewModel>(await _SupplierRepository.GetById(id));

            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);

            if (SupplierViewModel == null)
            {
                return NotFound();
            }


            var expenser = await _ExpenseFinancialSettlementRepository.GetAll();
            ViewData["Expenser"] = _mapper.Map<IEnumerable<ExpenseFinancialSettlement>>(expenser).ToList();

            var expenseType = await _expenseTypeRepository.GetByCompanyId(companyId);
            ViewData["ExpenseTypes"] = _mapper.Map<IEnumerable<ExpenseTypeViewModel>>(expenseType);

            string cAcertos = string.Empty;
            string proximoAcerto = string.Empty;
            int nAcertos = 0;

            foreach (var item in _mapper.Map<IEnumerable<ExpenseFinancialSettlement>>(expenser).ToList().Where(w=> w.Supplier_Id == id))
            {
                if (proximoAcerto != item.FinancialSettlement.Code)
                {
                    cAcertos = cAcertos + " - " + Environment.NewLine + item.FinancialSettlement.Code;
                    nAcertos = nAcertos + 1;
                    proximoAcerto = item.FinancialSettlement.Code;
                }
            }
            
            //verifico se tem despesa para esse fornecedor.
            if (nAcertos > 0 )
            {
                TempData["cls"] = "danger";
                TempData["message"] = "Não é possível excluir esse registro. Existe (m) " + nAcertos + " acerto (s) vinculado (s) a esse fornecedor.!! " + cAcertos;

                return RedirectToAction(nameof(Index));
            }
            else
            {
                await _SupplierRepository.Remove(await _SupplierRepository.GetById(id));


                TempData["cls"] = "success";
                TempData["message"] = "Deletado com sucesso !!";

                return RedirectToAction(nameof(Index));

            }

        }
    }
}
