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
    public class PaymentFormsController : Controller
    {
        private readonly IPaymentFormRepository _PaymentFormRepository;
        private readonly IMapper _mapper;
        public PaymentFormsController(IPaymentFormRepository PaymentFormRepository,
                                IMapper mapper)
        {
            _PaymentFormRepository = PaymentFormRepository;
            _mapper = mapper;
        }

        // GET: Cadastro/Responsibilities
        public async Task<IActionResult> Index()
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            var users = await _PaymentFormRepository.GetByCompanyId(companyId);

            return View(_mapper.Map<IEnumerable<PaymentFormViewModel>>(users));
        }

        // GET: Cadastro/Responsibilities/Details/5
        public async Task<IActionResult> Details(Guid id)
        {


            var userViewModel = _mapper.Map<PaymentFormViewModel>(await _PaymentFormRepository.GetById(id));
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
        public async Task<IActionResult> Create(PaymentFormViewModel PaymentFormViewModel)
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            PaymentFormViewModel.Company_Id = companyId;

            if (!ModelState.IsValid) return View(PaymentFormViewModel);

            var PaymentForm = _mapper.Map<PaymentForm>(PaymentFormViewModel);

            await _PaymentFormRepository.Add(PaymentForm);

            TempData["cls"] = "success";
            TempData["message"] = "Criado com sucesso !!";

            return RedirectToAction(nameof(Index));
        }

        // GET: Cadastro/Responsibilities/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var PaymentFormViewModel = _mapper.Map<PaymentFormViewModel>(await _PaymentFormRepository.GetById(id));

            if (PaymentFormViewModel == null)
            {
                return NotFound();
            }
            return View(PaymentFormViewModel);
        }

        // POST: Cadastro/Responsibilities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PaymentFormViewModel PaymentFormViewModel)
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            PaymentFormViewModel.Company_Id = companyId;

            if (id != PaymentFormViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(PaymentFormViewModel);

            var PaymentForm = _mapper.Map<PaymentForm>(PaymentFormViewModel);
            await _PaymentFormRepository.Update(PaymentForm);

            TempData["cls"] = "success";
            TempData["message"] = "Editado com sucesso !!";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var PaymentFormViewModel = _mapper.Map<PaymentFormViewModel>(await _PaymentFormRepository.GetById(id));

            if (PaymentFormViewModel == null)
            {
                return NotFound();
            }
            await _PaymentFormRepository.Remove(await _PaymentFormRepository.GetById(id));

            TempData["cls"] = "success";
            TempData["message"] = "Deletado com sucesso !!";

            return RedirectToAction(nameof(Index));
        }
    }
}
