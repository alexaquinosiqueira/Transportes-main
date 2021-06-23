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
using Transportadora.UI.Site.ViewModels;

namespace Transportadora.UI.Site.Areas.Portal.Controllers
{
    [Area("Portal")]
    public class CompaniesController : Controller
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;

        public CompaniesController(ICompanyRepository companyRepository,
                                IAddressRepository addressRepository,
                                   IMapper mapper)
        {
            _companyRepository = companyRepository;
            _addressRepository = addressRepository;
            _mapper = mapper;
        }

        // GET: Portal/Companies
        public async Task<IActionResult> Index()
        {
            var companies = await _companyRepository.GetAll();

            return View(_mapper.Map<IEnumerable<CompanyViewModel>>(companies));
        }

        // GET: Portal/Companies/Details/5
        public async Task<IActionResult> Details(Guid id)
        {

            var companyViewModel = _mapper.Map<CompanyViewModel>(await _companyRepository.GetById(id));
            if (companyViewModel == null)
            {
                return NotFound();
            }

            return View(companyViewModel);

        }

        // GET: Portal/Companies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Portal/Companies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompanyViewModel companyViewModel)
        {
            if (!ModelState.IsValid) return View(companyViewModel);

            companyViewModel.Address_Id = companyViewModel.Address.Id;

            var company = _mapper.Map<Company>(companyViewModel);

            await _companyRepository.Add(company);

            TempData["cls"] = "success";
            TempData["message"] = "Criado com Sucesso";

            return RedirectToAction(nameof(Index));
        }

        // GET: Portal/Companies/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var companyViewModel = _mapper.Map<CompanyViewModel>(await _companyRepository.GetById(id));

            if (companyViewModel == null)
            {
                return NotFound();
            }
            return View(companyViewModel);
        }

        // POST: Portal/Companies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CompanyViewModel companyViewModel)
        {
            if (id != companyViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(companyViewModel);

            var company = _mapper.Map<Company>(companyViewModel);
            await _companyRepository.Update(company);

            TempData["cls"] = "success";
            TempData["message"] = "Editado com Sucesso";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var companyViewModel = _mapper.Map<CompanyViewModel>(await _companyRepository.GetById(id));

            if (companyViewModel == null)
            {
                return NotFound();
            }
            await _companyRepository.Remove(await _companyRepository.GetById(id));

            TempData["cls"] = "success";
            TempData["message"] = "Deletado com sucesso !!";

            return RedirectToAction(nameof(Index));
        }
    }
}
