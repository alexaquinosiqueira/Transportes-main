using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Transportadora.Business.Interfaces;
using Transportadora.Business.Models;
using Transportadora.Data.Context;
using Transportadora.UI.Site.AppServer;
using Transportadora.UI.Site.Controllers;
using Transportadora.UI.Site.ViewModels;

namespace Transportadora.UI.Site.Areas.Cadastro.Controllers
{
    [Area("Cadastro")]
    public class EmployeesController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IResponsibilityRepository _responsibilityRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IStateRepository _stateRepository;
        private readonly IFinancialSettlementRepository _FinancialSettlementRepository;
        private readonly IMapper _mapper;
        public EmployeesController(IEmployeeRepository employeeRepository,
                                    IResponsibilityRepository responsibilityRepository,
                                    IAddressRepository addressRepository,
                                    IFinancialSettlementRepository financialSettlementRepository,
                                    ICityRepository cityRepository,
                                IStateRepository stateRepository,
                                IMapper mapper)                                
        {
            _employeeRepository = employeeRepository;
            _cityRepository = cityRepository;
            _FinancialSettlementRepository = financialSettlementRepository;
            _stateRepository = stateRepository;
            _responsibilityRepository = responsibilityRepository;
            _addressRepository = addressRepository;
            _mapper = mapper;
        }

        // GET: Cadastro/Responsibilities
        public async Task<IActionResult> Index()
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            var users = await _employeeRepository.GetByCompanyId(companyId);

            //var employee = await _employeeRepository.GetByCompanyId(companyId);

            return View(_mapper.Map<IEnumerable<EmployeeViewModel>>(users));
        }

        // GET: Cadastro/Responsibilities/Details/5
        public async Task<IActionResult> Details(Guid id)
        {


            var employeeViewModel = _mapper.Map<EmployeeViewModel>(await _employeeRepository.GetById(id));
            if (employeeViewModel == null)
            {
                return NotFound();
            }

            return View(employeeViewModel);
        }

        // GET: Cadastro/Responsibilities/Create
        public async Task<IActionResult> Create()
        {
            var responsibilities = await _responsibilityRepository.GetAll();
            ViewData["responsibilities"] = _mapper.Map<IEnumerable<ResponsibilityViewModel>>(responsibilities);

            var states = await _stateRepository.GetAll();
            ViewData["States"] = _mapper.Map<IEnumerable<StateViewModel>>(states);
            return View();
        }

        // POST: Cadastro/Responsibilities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel EmployeeViewModel)
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            EmployeeViewModel.Company_Id = companyId;

            var responsibilities = await _responsibilityRepository.GetAll();
            ViewData["responsibilities"] = _mapper.Map<IEnumerable<ResponsibilityViewModel>>(responsibilities);

            var states = await _stateRepository.GetAll();
            ViewData["States"] = _mapper.Map<IEnumerable<StateViewModel>>(states);

            var cities = await _cityRepository.Search(c => c.Uf == EmployeeViewModel.Address.State_id);
            ViewData["Cities"] = _mapper.Map<IEnumerable<CityViewModel>>(cities);

            if (EmployeeViewModel.Address != null && EmployeeViewModel.Address.AddressLine == null)
            {
                EmployeeViewModel.Address.PostalCode = "32649-270";
                EmployeeViewModel.Address.AddressLine = "Avenida Fausto Ribeiro da Silva";
                EmployeeViewModel.Address.City_Id = 1636;
                EmployeeViewModel.Address.Number = "80";
                EmployeeViewModel.Address.Neighborhood = "Cidade Verde";
                EmployeeViewModel.Address.State_id = 11;
            }

            if (EmployeeViewModel.UFRG == null)
                EmployeeViewModel.UFRG = "999";

            if (EmployeeViewModel.NomeMae == null)
                EmployeeViewModel.NomeMae = ".";

            if (EmployeeViewModel.RG == null)
            {
                EmployeeViewModel.RG = "99";
                EmployeeViewModel.DataEmissao = DateTime.Now;
                EmployeeViewModel.OrgaoExpedidor = "mgmg";
            }

            if (EmployeeViewModel.CNH == null)
            {
                EmployeeViewModel.CNH = "990";
                EmployeeViewModel.DataCNH = DateTime.Now;
                EmployeeViewModel.TipoCNH = 2;
                EmployeeViewModel.ResignationDate = DateTime.Now;
            }

            if (EmployeeViewModel.CPF == null)
            {
                EmployeeViewModel.CPF = "999";
                EmployeeViewModel.AdmissionDate = DateTime.Now;
                EmployeeViewModel.NumeroINSS = "999";              
            }

            //if (!ModelState.IsValid) return View(EmployeeViewModel);

            var employee = _mapper.Map<Employee>(EmployeeViewModel);

            var imgPrefix = Guid.NewGuid() + "_";


            if (EmployeeViewModel.ImagemUpload != null)
            {
                if (!await FileUpload(EmployeeViewModel.ImagemUpload, imgPrefix))
                {
                    return View(EmployeeViewModel);
                }

                employee.Imagem = imgPrefix + EmployeeViewModel.ImagemUpload.FileName;

            }

            await _employeeRepository.Add(employee);

            TempData["cls"] = "success";
            TempData["message"] = "Criado com sucesso !!";

            //if (!OperacaoValida()) return View(EmployeeViewModel);

            return RedirectToAction(nameof(Index));
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

        // GET: Cadastro/Responsibilities/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var EmployeeViewModel = _mapper.Map<EmployeeViewModel>(await _employeeRepository.GetById(id));

            var responsibilities = await _responsibilityRepository.GetAll();
            ViewData["responsibilities"] = _mapper.Map<IEnumerable<ResponsibilityViewModel>>(responsibilities);

            var states = await _stateRepository.GetAll();
            ViewData["States"] = _mapper.Map<IEnumerable<StateViewModel>>(states);

            var cities = await _cityRepository.Search(c => c.Uf == EmployeeViewModel.Address.State_id);
            ViewData["Cities"] = _mapper.Map<IEnumerable<CityViewModel>>(cities);

            if (EmployeeViewModel == null)
            {
                return NotFound();
            }
            return View(EmployeeViewModel);
        }

        // POST: Cadastro/Responsibilities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, EmployeeViewModel EmployeeViewModel)
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            EmployeeViewModel.Company_Id = companyId;

            if (id != EmployeeViewModel.Id) return NotFound();

            if(!ModelState.IsValid)
            {
                var responsibilities1 = await _responsibilityRepository.GetAll();
                ViewData["responsibilities"] = _mapper.Map<IEnumerable<ResponsibilityViewModel>>(responsibilities1);

                var states1 = await _stateRepository.GetAll();
                ViewData["States"] = _mapper.Map<IEnumerable<StateViewModel>>(states1);

                var cities1 = await _cityRepository.Search(c => c.Uf == EmployeeViewModel.Address.State_id);
                ViewData["Cities"] = _mapper.Map<IEnumerable<CityViewModel>>(cities1);

                TempData["cls"] = "warning";
                TempData["message"] = "Verifique campos obrigatórios";

                return View(EmployeeViewModel);
            }

            var employee = _mapper.Map<Employee>(EmployeeViewModel);

            await _employeeRepository.Update(employee);

            var responsibilities = await _responsibilityRepository.GetAll();
            ViewData["responsibilities"] = _mapper.Map<IEnumerable<ResponsibilityViewModel>>(responsibilities);

            var states = await _stateRepository.GetAll();
            ViewData["States"] = _mapper.Map<IEnumerable<StateViewModel>>(states);

            var cities = await _cityRepository.Search(c => c.Uf == EmployeeViewModel.Address.State_id);
            ViewData["Cities"] = _mapper.Map<IEnumerable<CityViewModel>>(cities);

            TempData["cls"] = "success";
            TempData["message"] = "Editado com sucesso !!";

            //if (!OperacaoValida()) return View(EmployeeViewModel);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var EmployeeViewModel = _mapper.Map<EmployeeViewModel>(await _employeeRepository.GetById(id));

            if (EmployeeViewModel == null)
            {
                return NotFound();
            }


            //verifico se tem despesa para essa frota.
            string cAcertos = string.Empty;
            string proximoAcerto = string.Empty;
            int nAcertos = 0;

            var expenser = await _FinancialSettlementRepository.GetAll();
            ViewData["FinancialSettlement"] = _mapper.Map<IEnumerable<FinancialSettlement>>(expenser).ToList();

            foreach (var item in _mapper.Map<IEnumerable<FinancialSettlement>>(expenser).ToList().Where(w => w.Employee_Id == id))
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
                TempData["message"] = "Não é possível excluir esse registro. Existe (m) " + nAcertos + " acerto (s) vinculado (s) a esse funcionário!! " + cAcertos;

                return RedirectToAction(nameof(Index));
            }
            else
            {
                await _employeeRepository.Remove(await _employeeRepository.GetById(id));

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
    }
}
