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
    public class VehicleTypesController : Controller
    {
        private readonly IVehicleTypeRepository _vehicleTypeRepository;

        private readonly IMapper _mapper;
        public VehicleTypesController(IVehicleTypeRepository vehicleTypeRepository,
                                IMapper mapper)
        {
            _vehicleTypeRepository = vehicleTypeRepository;
            _mapper = mapper;
        }

        // GET: Cadastro/VehicleTypes
        public async Task<IActionResult> Index()
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);

            var vehicleType = await _vehicleTypeRepository.GetByCompanyId(companyId);

            return View(_mapper.Map<IEnumerable<VehicleTypeViewModel>>(vehicleType));
        }

        // GET: Cadastro/VehicleTypes/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var vehicleTypeViewModel = _mapper.Map<VehicleTypeViewModel>(await _vehicleTypeRepository.GetById(id));
            if (vehicleTypeViewModel == null)
            {
                return NotFound();
            }

            return View(vehicleTypeViewModel);
        }

        // GET: Cadastro/VehicleTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cadastro/VehicleTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleTypeViewModel vehicleTypeViewModel)
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            vehicleTypeViewModel.Company_Id = companyId;

            if (!ModelState.IsValid) return View(vehicleTypeViewModel);

            var vehicleType = _mapper.Map<VehicleType>(vehicleTypeViewModel);

            await _vehicleTypeRepository.Add(vehicleType);

            return RedirectToAction(nameof(Index));
        }

        // GET: Cadastro/VehicleTypes/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {

            var vehicleType = await _vehicleTypeRepository.GetAll();
            var vehicleTypeViewModel = _mapper.Map<VehicleTypeViewModel>(await _vehicleTypeRepository.GetById(id));

            if (vehicleTypeViewModel == null)
            {
                return NotFound();
            }
            return View(vehicleTypeViewModel);
        }

        // POST: Cadastro/VehicleTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, VehicleTypeViewModel vehicleTypeViewModel)
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            vehicleTypeViewModel.Company_Id = companyId;

            if (id != vehicleTypeViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(vehicleTypeViewModel);

            var employee = _mapper.Map<VehicleType>(vehicleTypeViewModel);
            await _vehicleTypeRepository.Update(employee);


            return RedirectToAction("Index");
        }

        // GET: Cadastro/VehicleTypes/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var vehicleTypeViewModel = _mapper.Map<VehicleTypeViewModel>(await _vehicleTypeRepository.GetById(id));

            if (vehicleTypeViewModel == null)
            {
                return NotFound();
            }
            await _vehicleTypeRepository.Remove(await _vehicleTypeRepository.GetById(id));

            TempData["cls"] = "success";
            TempData["message"] = "Deletado com sucesso !!";

            return RedirectToAction(nameof(Index));
        }
    }
}
