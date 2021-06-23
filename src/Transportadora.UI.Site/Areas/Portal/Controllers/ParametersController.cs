using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Transportadora.Business.Interfaces;
using Transportadora.Business.Models;
using Transportadora.UI.Site.AppServer;
using Transportadora.UI.Site.ViewModels;

namespace Transportadora.UI.Site.Areas.Portal.Controllers
{
    [Area("Portal")]
    public class ParametersController : Controller
    {
        private readonly ICostcenterRepository _CostcenterRepository;
        private readonly IBankAccountRepository _BankAccountRepository;
        private readonly IDocumentTypeRepository _DocumentTypeRepository;
        private readonly IPaymentFormRepository _PaymentFormRepository;
        private readonly IParameterRepository _parameterRepository;
        private readonly IStateRepository _stateRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;

        public ParametersController(IExpenseRepository ExpenseRepository,
            ICostcenterRepository CostcenterRepository,
            IBankAccountRepository BankAccountRepository,
            IDocumentTypeRepository DocumentTypeRepository,
            IPaymentFormRepository PaymentFormRepository, 
            IParameterRepository parameterRepository,
                                            ICityRepository cityRepository,
                                            IStateRepository stateRepository,
                                   IMapper mapper)
        {
            _CostcenterRepository = CostcenterRepository;
            _BankAccountRepository = BankAccountRepository;
            _DocumentTypeRepository = DocumentTypeRepository;
            _PaymentFormRepository = PaymentFormRepository;
            _parameterRepository = parameterRepository;
            _stateRepository = stateRepository;
            _cityRepository = cityRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            var parameter = await _parameterRepository.GetByCompanyId(companyId);
            var parameterViewModel = _mapper.Map<IEnumerable<ParameterViewModel>>(parameter).FirstOrDefault();
            var costcenters = await _CostcenterRepository.GetAll();
            ViewData["CostCenters"] = _mapper.Map<IEnumerable<CostCenterViewModel>>(costcenters);
            var bankAccounts = await _BankAccountRepository.GetAll();
            ViewData["BankAccounts"] = _mapper.Map<IEnumerable<BankAccountViewModel>>(bankAccounts);
            var documentTypes = await _DocumentTypeRepository.GetAll();
            ViewData["DocumentTypes"] = _mapper.Map<IEnumerable<DocumentTypeViewModel>>(documentTypes);
            var paymentForms = await _PaymentFormRepository.GetAll();
            ViewData["PaymentForms"] = _mapper.Map<IEnumerable<PaymentFormViewModel>>(paymentForms);
            var states = await _stateRepository.GetAll();
            ViewData["States"] = _mapper.Map<IEnumerable<StateViewModel>>(states);
            if (parameterViewModel != null)
            {
                var cities = await _cityRepository.Search(c => c.State.Id == parameterViewModel.StateOrigin_Id );
                ViewData["Cities"] = _mapper.Map<IEnumerable<CityViewModel>>(cities);
            }
            else
                ViewData["Cities"] = new List<CityViewModel>();
            return View(parameterViewModel != null ? parameterViewModel : new ParameterViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ParameterViewModel parameterViewModel)
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            var parameters = await _parameterRepository.GetByCompanyId(companyId);
            var parameter = parameters.FirstOrDefault();

            if (parameter != null)
                await _parameterRepository.Remove(parameter);

            if (!ModelState.IsValid) return View(parameterViewModel);

            parameterViewModel.Id_Company = companyId;

            var fleet = _mapper.Map<Parameter>(parameterViewModel);

            await _parameterRepository.Add(fleet);

            return RedirectToAction(nameof(Create));

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
