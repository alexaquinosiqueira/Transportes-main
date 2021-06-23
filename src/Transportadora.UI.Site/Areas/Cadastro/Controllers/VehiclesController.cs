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
using Transportadora.Data.Context;
using Transportadora.UI.Site.AppServer;
using Transportadora.UI.Site.ViewModels;

namespace Transportadora.UI.Site.Areas.Cadastro.Controllers
{
    [Area("Cadastro")]
    public class VehiclesController : Controller
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IVehicleTypeRepository _vehicleTypeRepository;
        private readonly IFleetRepository _fleetRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IFinancialSettlementRepository _FinancialSettlementRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IStateRepository _stateRepository;
        private readonly IMapper _mapper;
        public VehiclesController(IVehicleRepository vehicleRepository,
                                  IVehicleTypeRepository vehicleTypeRepository,
                                  IFleetRepository fleetRepository,
                                IFinancialSettlementRepository financialSettlementRepository,
                                  IEmployeeRepository employeeRepository,
                                ICityRepository cityRepository,
                                IStateRepository stateRepository,
                                IMapper mapper)
        {
            _vehicleRepository = vehicleRepository;
            _vehicleTypeRepository = vehicleTypeRepository;
            _fleetRepository = fleetRepository;
            _employeeRepository = employeeRepository;
            _FinancialSettlementRepository = financialSettlementRepository;
            _cityRepository = cityRepository;
            _stateRepository = stateRepository;
            _mapper = mapper;
        }


        public async Task<IActionResult> Index()
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);

            var vehicle = await _vehicleRepository.GetByCompanyId(companyId);

            return View(_mapper.Map<IEnumerable<VehicleViewModel>>(vehicle));
        }

        public async Task<IActionResult> Details(Guid id)
        {


            var VehicleViewModel = _mapper.Map<VehicleViewModel>(await _vehicleRepository.GetById(id));
            if (VehicleViewModel == null)
            {
                return NotFound();
            }

            return View(VehicleViewModel);
        }

        public async Task<IActionResult> Create()
        {
            var vehicleTypes = await _vehicleTypeRepository.GetAll();
            ViewData["VehicleTypes"] = _mapper.Map<IEnumerable<VehicleTypeViewModel>>(vehicleTypes);
            var fleets = await _fleetRepository.GetAll();
            ViewData["Fleets"] = _mapper.Map<IEnumerable<FleetViewModel>>(fleets);
            var employees = await _employeeRepository.GetAll();
            ViewData["Employees"] = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
            var states = await _stateRepository.GetAll();
            ViewData["States"] = _mapper.Map<IEnumerable<StateViewModel>>(states);

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleViewModel VehicleViewModel)
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            VehicleViewModel.Company_Id = companyId;

            if (!ModelState.IsValid) return View(VehicleViewModel);

            var vehicle = _mapper.Map<Vehicle>(VehicleViewModel);

            await _vehicleRepository.Add(vehicle);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var vehicleTypes = await _vehicleTypeRepository.GetAll();
            ViewData["VehicleTypes"] = _mapper.Map<IEnumerable<VehicleTypeViewModel>>(vehicleTypes);
            var fleets = await _fleetRepository.GetAll();
            ViewData["Fleets"] = _mapper.Map<IEnumerable<FleetViewModel>>(fleets);
            var employees = await _employeeRepository.GetAll();
            ViewData["Employees"] = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
            var states = await _stateRepository.GetAll();
            ViewData["States"] = _mapper.Map<IEnumerable<StateViewModel>>(states);
            var VehicleViewModel = _mapper.Map<VehicleViewModel>(await _vehicleRepository.GetById(id));

            var cities = await _cityRepository.GetAll();
            ViewData["Cities"] = _mapper.Map<IEnumerable<CityViewModel>>(cities);

            if (VehicleViewModel == null)
            {
                return NotFound();
            }
            return View(VehicleViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, VehicleViewModel VehicleViewModel)
        {

            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            VehicleViewModel.Company_Id = companyId;

            if (id != VehicleViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(VehicleViewModel);

            var vehicle = _mapper.Map<Vehicle>(VehicleViewModel);
            await _vehicleRepository.Update(vehicle);

            var vehicleTypes = await _vehicleTypeRepository.GetAll();
            ViewData["VehicleTypes"] = _mapper.Map<IEnumerable<VehicleTypeViewModel>>(vehicleTypes);
            var fleets = await _fleetRepository.GetAll();
            ViewData["Fleets"] = _mapper.Map<IEnumerable<FleetViewModel>>(fleets);
            var employees = await _employeeRepository.GetAll();
            ViewData["Employees"] = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
            var states = await _stateRepository.GetAll();
            ViewData["States"] = _mapper.Map<IEnumerable<StateViewModel>>(states);

            TempData["cls"] = "success";
            TempData["message"] = "Editado com sucesso !!";


            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var VehicleViewModel = _mapper.Map<VehicleViewModel>(await _vehicleRepository.GetById(id));

            if (VehicleViewModel == null)
            {
                return NotFound();
            }

            //verifico se tem despesa para esse veiculo.
            string cAcertos = string.Empty;
            string proximoAcerto = string.Empty;
            int nAcertos = 0;

            var expenser = await _FinancialSettlementRepository.GetAll();
            ViewData["FinancialSettlement"] = _mapper.Map<IEnumerable<FinancialSettlement>>(expenser).ToList();

            foreach (var item in _mapper.Map<IEnumerable<FinancialSettlement>>(expenser).ToList().Where(w => w.Vehicle_Id == id))
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
                TempData["message"] = "Não é possível excluir esse registro. Existe (m) " + nAcertos + " acerto (s) vinculado (s) a esse veículo.!! " + cAcertos;

                return RedirectToAction(nameof(Index));
            }
            else
            {
                await _vehicleRepository.Remove(await _vehicleRepository.GetById(id));

                TempData["cls"] = "success";
                TempData["message"] = "Deletado com sucesso !!";

                return RedirectToAction(nameof(Index));
            }

        }
        [HttpGet]
        public async Task<IActionResult> GetCityByStateId(int StateId)
        {
            var cities = await _cityRepository.Search(c => c.Uf == StateId);
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
