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

namespace Transportadora.UI.Site.Areas.Relatorio.Controllers
{
    [Area("Relatorio")]
    public class LiquidoFrotaController : Controller
    {
        private readonly IFinancialSettlementRepository _financialSettlementRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IExpenseFinancialSettlementRepository _expenseFinancialSettlementRepository;
        private readonly IFleetRepository _fleetRepository;


        private readonly IExpenseRepository _ExpenseRepository;


        private readonly IMapper _mapper;

        public LiquidoFrotaController(IFinancialSettlementRepository financialSettlementRepository,
                                            IExpenseFinancialSettlementRepository expenseFinancialSettlementRepository,
                                            IEmployeeRepository employeeRepository,
                                            IFleetRepository fleetRepository,
                                            IExpenseRepository expenseRepository,
                                IMapper mapper)
        {
            _financialSettlementRepository = financialSettlementRepository;
            _expenseFinancialSettlementRepository = expenseFinancialSettlementRepository;
            _employeeRepository = employeeRepository;
            _ExpenseRepository = expenseRepository;
            _fleetRepository = fleetRepository;
            _mapper = mapper;
        }

        [Authorize(Roles = RolesConfig.GestaoAcertosListagem)]
        public async Task<IActionResult> LiquidoFrota()
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID); 
            var financialSettlements = await _financialSettlementRepository.GetByCompanyId(companyId);
            var valores = await _expenseFinancialSettlementRepository.GetAll();

            return View(_mapper.Map<IEnumerable< ExpenseFinancialSettlementViewModel>>(valores));
        }
        public async Task<IActionResult> GetLiqFrotaAll()
        {
            var fret = await _fleetRepository.GetAll();

            var valuesReturn = (from t in fret
                                group t by new { t.Description }
                         into grp
                                select new
                                {
                                    grp.Key.Description,
                                    nome = grp.Key.Description
                                }).OrderBy(o => o.nome);
            return Json(valuesReturn);
        }

        public async Task<IActionResult> GetLiqFrotaId(DateTime data_ini, DateTime data_fim, Guid frota)
        {
            var valores = await _expenseFinancialSettlementRepository.GetAll();

            var valuesReturn = (from t in valores
                                group t by new { t.ExpenseDate.Month, t.FinancialSettlement.Fleet.Description, t.Arquivo }
                         into grp
                                select new
                                {
                                    grp.Key.Description,
                                    grp.Key.Arquivo,
                                    valor = grp.Sum(t => t.Valor_Total),
                                    mes = grp.Key.Month,
                                    nome = grp.Key.Description
                                }).Where(w => w.Arquivo == "Performance Motorista");

            return Json(valuesReturn);
        }
    }
}
