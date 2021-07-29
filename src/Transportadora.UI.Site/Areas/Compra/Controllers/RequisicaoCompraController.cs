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
    [Area("Compra")]
    public class RequisicaoCompraController : Controller
    {
        private readonly IRequisicaoCompraRepository _requisicaoCompraRepository;
        private readonly IProdutoRepository _ProdutoRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IParameterRepository _parameterRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ICostcenterRepository _costcenterRepository;


        private readonly IMapper _mapper;
        public RequisicaoCompraController(IRequisicaoCompraRepository RequisicaoCompraRepository,
            IProdutoRepository ProdutoRepository,
            IParameterRepository parameterRepository,
            IVehicleRepository vehicleRepository,
            ICustomerRepository customerRepository,
            ICostcenterRepository costcenterRepository,

        IMapper mapper)
        {
            _requisicaoCompraRepository = RequisicaoCompraRepository;
            _ProdutoRepository = ProdutoRepository;
            _parameterRepository = parameterRepository;
            _customerRepository = customerRepository;
            _vehicleRepository = vehicleRepository;
            _costcenterRepository = costcenterRepository;

            _mapper = mapper;
        }

        // GET: Cadastro/Responsibilities
        [Authorize(Roles = RolesConfig.RequisicaoCompraVisualization)]

        public async Task<IActionResult> Index()
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            var users = await _requisicaoCompraRepository.GetByCompanyId(companyId);

            return View(_mapper.Map<IEnumerable<RequisicaoCompraViewModel>>(users));
        }

        // GET: Cadastro/Responsibilities/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var userViewModel = _mapper.Map<RequisicaoCompraViewModel>(await _requisicaoCompraRepository.GetById(id));
            if (userViewModel == null)
            {
                return NotFound();
            }

            return View(userViewModel);
        }

        // GET: Cadastro/Responsibilities/Create
        [Authorize(Roles = RolesConfig.RequisicaoCompraAdd)]
        public async Task<IActionResult> Create()
        {
            IdentityManager identityManager = new IdentityManager(User);

            var companyId = Guid.Parse(identityManager.CompanyID);

            var parameter = await _parameterRepository.GetByCompanyId(companyId);
            var parameterViewModel = _mapper.Map<IEnumerable<ParameterViewModel>>(parameter).FirstOrDefault();
            RequisicaoCompraViewModel requisicaocompra = new RequisicaoCompraViewModel();
            requisicaocompra.Data_Requisicao = DateTime.Now;
            requisicaocompra.Code = GetSequentialCode();
            requisicaocompra.Solicitante = identityManager.UserName;

            var vehicle = await _vehicleRepository.GetByCompanyId(companyId);
            ViewData["Vehicle"] = _mapper.Map<IEnumerable<VehicleViewModel>>(vehicle);

            var produto = await _ProdutoRepository.GetByCompanyId(companyId);
            ViewData["Produto"] = _mapper.Map<IEnumerable<ProdutoViewModel>>(produto);

            var costcenter = await _costcenterRepository.GetByCompanyId(companyId);
            ViewData["CostCenter"] = _mapper.Map<IEnumerable<CostCenterViewModel>>(costcenter);

            return View(requisicaocompra);
        }
        private string GetSequentialCode()
        {
            return string.Format($"{DateTime.Now.Year}{DateTime.Now.Month}{DateTime.Now.Day}{DateTime.Now.Hour}{DateTime.Now.Minute}{DateTime.Now.Second}{DateTime.Now.Millisecond}");
        }


        // POST: Cadastro/Responsibilities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RolesConfig.RequisicaoCompraAdd)]

        public async Task<IActionResult> Create(RequisicaoCompraViewModel requisicaoCompraViewModel)
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            requisicaoCompraViewModel.Company_Id = companyId;

            var vehicle = await _vehicleRepository.GetByCompanyId(companyId);
            ViewData["Vehicle"] = _mapper.Map<IEnumerable<VehicleViewModel>>(vehicle);

            var produto = await _ProdutoRepository.GetByCompanyId(companyId);
            ViewData["Produto"] = _mapper.Map<IEnumerable<ProdutoViewModel>>(produto);

            var costcenter = await _costcenterRepository.GetByCompanyId(companyId);
            ViewData["CostCenter"] = _mapper.Map<IEnumerable<CostCenterViewModel>>(costcenter);

            requisicaoCompraViewModel.Solicitante = identityManager.UserName;
            var requisicaoCompra = _mapper.Map<RequisicaoCompra>(requisicaoCompraViewModel);

            if (!ModelState.IsValid)
            {
                TempData["cls"] = "warning";
                TempData["message"] = "Verifique os campos obrigatórios !!";

                return View(requisicaoCompraViewModel);
            }

            await _requisicaoCompraRepository.Add(requisicaoCompra);

            if (requisicaoCompra.ItensRequisicaoCompras.Count == 0)
            {
                TempData["cls"] = "warning";
                TempData["message"] = "ATENÇÃO: Requisição gerada sem produto para cotação";
            }
            else
            {
                TempData["cls"] = "success";
                TempData["message"] = "Criado com sucesso !!";
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Cadastro/Responsibilities/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var RequisicaoCompraViewModel = _mapper.Map<RequisicaoCompraViewModel>(await _requisicaoCompraRepository.GetById(id));

            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);

            var vehicles = await _vehicleRepository.GetByCompanyId(companyId);
            ViewData["Vehicles"] = _mapper.Map<IEnumerable<VehicleViewModel>>(vehicles);

            var costcenter = await _costcenterRepository.GetByCompanyId(companyId);
            ViewData["CostCenter"] = _mapper.Map<IEnumerable<CostCenterViewModel>>(costcenter);

            if (RequisicaoCompraViewModel == null)
                return NotFound();

            return View(RequisicaoCompraViewModel);
        }

        // POST: Cadastro/Responsibilities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RolesConfig.RequisicaoCompraEdit)]

        public async Task<IActionResult> Edit(Guid id, RequisicaoCompraViewModel RequisicaoCompraViewModel)
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            RequisicaoCompraViewModel.Company_Id = companyId;


            if (id != RequisicaoCompraViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(RequisicaoCompraViewModel);

            var RequisicaoCompra = _mapper.Map<RequisicaoCompra>(RequisicaoCompraViewModel);
            await _requisicaoCompraRepository.Update(RequisicaoCompra);

            var vehicles = await _vehicleRepository.GetByCompanyId(companyId);
            ViewData["Vehicles"] = _mapper.Map<IEnumerable<VehicleViewModel>>(vehicles);

            var costcenter = await _costcenterRepository.GetByCompanyId(companyId);
            ViewData["CostCenter"] = _mapper.Map<IEnumerable<CostCenterViewModel>>(costcenter);

            TempData["cls"] = "success";
            TempData["message"] = "Editado com sucesso !!";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var RequisicaoCompraViewModel = _mapper.Map<RequisicaoCompraViewModel>(await _requisicaoCompraRepository.GetById(id));

            if (RequisicaoCompraViewModel == null)
            {
                return NotFound();
            }
            await _requisicaoCompraRepository.Remove(await _requisicaoCompraRepository.GetById(id));

            TempData["cls"] = "success";
            TempData["message"] = "Deletado com sucesso !!";

            return RedirectToAction(nameof(Index));
        }
    }
}
