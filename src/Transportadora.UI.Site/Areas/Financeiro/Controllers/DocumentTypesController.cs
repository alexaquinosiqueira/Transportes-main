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
    public class DocumentTypesController : Controller
    {
        private readonly IDocumentTypeRepository _DocumentTypeRepository;
        private readonly IMapper _mapper;
        public DocumentTypesController(IDocumentTypeRepository DocumentTypeRepository,
                                IMapper mapper)
        {
            _DocumentTypeRepository = DocumentTypeRepository;
            _mapper = mapper;
        }

        // GET: Cadastro/Responsibilities
        public async Task<IActionResult> Index()
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);

            var users = await _DocumentTypeRepository.GetByCompanyId(companyId);

            return View(_mapper.Map<IEnumerable<DocumentTypeViewModel>>(users));
        }

        // GET: Cadastro/Responsibilities/Details/5
        public async Task<IActionResult> Details(Guid id)
        {


            var userViewModel = _mapper.Map<DocumentTypeViewModel>(await _DocumentTypeRepository.GetById(id));
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
        public async Task<IActionResult> Create(DocumentTypeViewModel DocumentTypeViewModel)
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            DocumentTypeViewModel.Company_Id = companyId;

            if (!ModelState.IsValid) return View(DocumentTypeViewModel);

            var DocumentType = _mapper.Map<DocumentType>(DocumentTypeViewModel);

            await _DocumentTypeRepository.Add(DocumentType);

            TempData["cls"] = "success";
            TempData["message"] = "Criado com sucesso !!";


            return RedirectToAction(nameof(Index));
        }

        // GET: Cadastro/Responsibilities/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var DocumentTypeViewModel = _mapper.Map<DocumentTypeViewModel>(await _DocumentTypeRepository.GetById(id));

            if (DocumentTypeViewModel == null)
            {
                return NotFound();
            }
            return View(DocumentTypeViewModel);
        }

        // POST: Cadastro/Responsibilities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, DocumentTypeViewModel DocumentTypeViewModel)
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            DocumentTypeViewModel.Company_Id = companyId;

            if (id != DocumentTypeViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(DocumentTypeViewModel);

            var DocumentType = _mapper.Map<DocumentType>(DocumentTypeViewModel);
            await _DocumentTypeRepository.Update(DocumentType);

            TempData["cls"] = "success";
            TempData["message"] = "Editado com sucesso !!";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var DocumentTypeViewModel = _mapper.Map<DocumentTypeViewModel>(await _DocumentTypeRepository.GetById(id));

            if (DocumentTypeViewModel == null)
            {
                return NotFound();
            }
            await _DocumentTypeRepository.Remove(await _DocumentTypeRepository.GetById(id));

            TempData["cls"] = "success";
            TempData["message"] = "Deletado com sucesso !!";

            return RedirectToAction(nameof(Index));
        }
    }
}
