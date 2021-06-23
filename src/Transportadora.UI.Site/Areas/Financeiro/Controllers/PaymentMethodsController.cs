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
    public class PaymentMethodsController : Controller
    {
        private readonly IPaymentMethodRepository _PaymentMethodRepository;
        private readonly IMapper _mapper;
        public PaymentMethodsController(IPaymentMethodRepository PaymentMethodRepository,
                                IMapper mapper)
        {
            _PaymentMethodRepository = PaymentMethodRepository;
            _mapper = mapper;
        }

        // GET: Cadastro/Responsibilities
        public async Task<IActionResult> Index()
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            var users = await _PaymentMethodRepository.GetByCompanyId(companyId);

            return View(_mapper.Map<IEnumerable<PaymentMethodViewModel>>(users));
        }

        // GET: Cadastro/Responsibilities/Details/5
        public async Task<IActionResult> Details(Guid id)
        {


            var userViewModel = _mapper.Map<PaymentMethodViewModel>(await _PaymentMethodRepository.GetById(id));
            if (userViewModel == null)
            {
                return NotFound();
            }

            return View(userViewModel);
        }

        // GET: Cadastro/Responsibilities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cadastro/Responsibilities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PaymentMethodViewModel PaymentMethodViewModel)
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            PaymentMethodViewModel.Company_Id = companyId;

            if (!ModelState.IsValid) return View(PaymentMethodViewModel);

            var PaymentMethod = _mapper.Map<PaymentMethod>(PaymentMethodViewModel);

            await _PaymentMethodRepository.Add(PaymentMethod);

            TempData["cls"] = "success";
            TempData["message"] = "Criado com sucesso !!";


            return RedirectToAction(nameof(Index));
        }

        // GET: Cadastro/Responsibilities/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var PaymentMethodViewModel = _mapper.Map<PaymentMethodViewModel>(await _PaymentMethodRepository.GetById(id));

            if (PaymentMethodViewModel == null)
            {
                return NotFound();
            }
            return View(PaymentMethodViewModel);
        }

        // POST: Cadastro/Responsibilities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PaymentMethodViewModel PaymentMethodViewModel)
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            PaymentMethodViewModel.Company_Id = companyId;

            if (id != PaymentMethodViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(PaymentMethodViewModel);

            var PaymentMethod = _mapper.Map<PaymentMethod>(PaymentMethodViewModel);
            await _PaymentMethodRepository.Update(PaymentMethod);

            TempData["cls"] = "success";
            TempData["message"] = "Editado com sucesso !!";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var PaymentMethodViewModel = _mapper.Map<PaymentMethodViewModel>(await _PaymentMethodRepository.GetById(id));

            if (PaymentMethodViewModel == null)
            {
                return NotFound();
            }
            await _PaymentMethodRepository.Remove(await _PaymentMethodRepository.GetById(id));

            TempData["cls"] = "success";
            TempData["message"] = "Deletado com sucesso !!";

            return RedirectToAction(nameof(Index));
        }
    }
}
