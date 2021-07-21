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
    public class RelatorioController : Controller
    {
        private readonly IFinancialSettlementRepository _financialSettlementRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IExpenseFinancialSettlementRepository _expenseFinancialSettlementRepository;
        private readonly IFleetRepository _fleetRepository;
        private readonly IExpenseTypeRepository _expenseTypeRepository;
        private readonly IExpenseRepository _ExpenseRepository;
        private readonly ISupplierRepository _supplierRepository;


        private readonly IMapper _mapper;

        public RelatorioController(IFinancialSettlementRepository financialSettlementRepository,
                                            IExpenseFinancialSettlementRepository expenseFinancialSettlementRepository,
                                            IEmployeeRepository employeeRepository,
                                            IVehicleRepository vehicleRepository,
                                            IFleetRepository fleetRepository,
                                            IExpenseTypeRepository expenseTypeRepository,
                                            ISupplierRepository supplierRepository,
                                            IExpenseRepository expenseRepository,
                                IMapper mapper)
        {
            _financialSettlementRepository = financialSettlementRepository;
            _expenseFinancialSettlementRepository = expenseFinancialSettlementRepository;
            _employeeRepository = employeeRepository;
            _vehicleRepository = vehicleRepository;
            _ExpenseRepository = expenseRepository;
            _expenseTypeRepository = expenseTypeRepository;
            _supplierRepository = supplierRepository;
            _fleetRepository = fleetRepository;
            _mapper = mapper;
        }

        [Authorize(Roles = RolesConfig.GestaoAcertosListagem)]
        public async Task<IActionResult> LiquidoMotorista()
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            var financialSettlements = await _financialSettlementRepository.GetByCompanyId(companyId);

            var valores = await _expenseFinancialSettlementRepository.GetAll();

            var valuesReturn = (from t in valores
                                group t by new { t.ExpenseDate.Month, t.FinancialSettlement.Employee.Name, t.Arquivo }
                         into grp
                                select new
                                {
                                    grp.Key.Month,
                                    grp.Key.Name,
                                    grp.Key.Arquivo,
                                    valor = grp.Sum(t => t.Valor_Total),
                                    mes = grp.Key.Month,
                                    nome = grp.Key.Name,
                                }).Where(w => w.Arquivo == "Performance Motorista");


            //return View(_mapper.Map<IEnumerable<FinancialSettlementViewModel>>(financialSettlements));
            return View(_mapper.Map<IEnumerable< ExpenseFinancialSettlementViewModel>>(valores));
        }

        public async Task<IActionResult> LiquidoFrota()
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            var financialSettlements = await _financialSettlementRepository.GetByCompanyId(companyId);

            var valores = await _expenseFinancialSettlementRepository.GetAll();

            var valuesReturn = (from t in valores
                                group t by new { t.ExpenseDate.Month, t.FinancialSettlement.Employee.Name, t.Arquivo }
                         into grp
                                select new
                                {
                                    grp.Key.Month,
                                    grp.Key.Name,
                                    grp.Key.Arquivo,
                                    valor = grp.Sum(t => t.Valor_Total),
                                    mes = grp.Key.Month,
                                    nome = grp.Key.Name,
                                }).Where(w => w.Arquivo == "Performance Motorista");


            //return View(_mapper.Map<IEnumerable<FinancialSettlementViewModel>>(financialSettlements));
            return View(_mapper.Map<IEnumerable<ExpenseFinancialSettlementViewModel>>(valores));
        }

        public async Task<IActionResult> ConsumoVeiculo()
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            var financialSettlements = await _financialSettlementRepository.GetByCompanyId(companyId);

            var valores = await _expenseFinancialSettlementRepository.GetAll();

            var valuesReturn = (from t in valores
                                group t by new { t.ExpenseDate.Month, t.FinancialSettlement.Employee.Name, t.Arquivo }
                         into grp
                                select new
                                {
                                    grp.Key.Month,
                                    grp.Key.Name,
                                    grp.Key.Arquivo,
                                    valor = grp.Sum(t => t.Valor_Total),
                                    mes = grp.Key.Month,
                                    nome = grp.Key.Name,
                                }).Where(w => w.Arquivo == "Performance Motorista");


            //return View(_mapper.Map<IEnumerable<FinancialSettlementViewModel>>(financialSettlements));
            return View(_mapper.Map<IEnumerable<ExpenseFinancialSettlementViewModel>>(valores));
        }

        public async Task<IActionResult> DespesasViagem()
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            var financialSettlements = await _financialSettlementRepository.GetByCompanyId(companyId);

            var valores = await _expenseFinancialSettlementRepository.GetAll();

            var valuesReturn = (from t in valores
                                group t by new { t.ExpenseDate.Month, t.FinancialSettlement.Employee.Name, t.Arquivo }
                         into grp
                                select new
                                {
                                    grp.Key.Month,
                                    grp.Key.Name,
                                    grp.Key.Arquivo,
                                    valor = grp.Sum(t => t.Valor_Total),
                                    mes = grp.Key.Month,
                                    nome = grp.Key.Name,
                                }).Where(w => w.Arquivo == "Performance Motorista");


            //return View(_mapper.Map<IEnumerable<FinancialSettlementViewModel>>(financialSettlements));
            return View(_mapper.Map<IEnumerable<ExpenseFinancialSettlementViewModel>>(valores));
        }

        public async Task<IActionResult> CentroCusto()
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            var financialSettlements = await _financialSettlementRepository.GetByCompanyId(companyId);

            var valores = await _expenseFinancialSettlementRepository.GetAll();

            var valuesReturn = (from t in valores
                                group t by new { t.ExpenseDate.Month, t.FinancialSettlement.Employee.Name, t.Arquivo }
                         into grp
                                select new
                                {
                                    grp.Key.Month,
                                    grp.Key.Name,
                                    grp.Key.Arquivo,
                                    valor = grp.Sum(t => t.Valor_Total),
                                    mes = grp.Key.Month,
                                    nome = grp.Key.Name,
                                }).Where(w => w.Arquivo == "Performance Motorista");


            //return View(_mapper.Map<IEnumerable<FinancialSettlementViewModel>>(financialSettlements));
            return View(_mapper.Map<IEnumerable<ExpenseFinancialSettlementViewModel>>(valores));
        }

        public async Task<IActionResult> Fornecedores()
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            var financialSettlements = await _financialSettlementRepository.GetByCompanyId(companyId);

            var valores = await _expenseFinancialSettlementRepository.GetAll();

            var valuesReturn = (from t in valores
                                group t by new { t.ExpenseDate.Month, t.FinancialSettlement.Employee.Name, t.Arquivo }
                         into grp
                                select new
                                {
                                    grp.Key.Month,
                                    grp.Key.Name,
                                    grp.Key.Arquivo,
                                    valor = grp.Sum(t => t.Valor_Total),
                                    mes = grp.Key.Month,
                                    nome = grp.Key.Name,
                                }).Where(w => w.Arquivo == "Performance Motorista");


            //return View(_mapper.Map<IEnumerable<FinancialSettlementViewModel>>(financialSettlements));
            return View(_mapper.Map<IEnumerable<ExpenseFinancialSettlementViewModel>>(valores));
        }

        public async Task<IActionResult> GetLiqConcumoVeiculoId(DateTime data_ini, DateTime data_fim, Guid motorista)
        {
            var consumo = await _financialSettlementRepository.GetAll();

            var valuesReturn = (from t in consumo
                                group t by new { t.TravelDate.Month, t.Vehicle.Description, t.Vehicle.VehicleLicensePlate }
                         into grp
                                select new
                                {
                                    grp.Key.Description,
                                    valor = grp.Sum(t => t.Consumo),
                                    mes = grp.Key.Month,
                                    nome = grp.Key.Description,
                                    placa = grp.Key.VehicleLicensePlate
                                }).OrderByDescending(o => o.mes).ThenBy(o=> o.valor);

            return Json(valuesReturn);
        }

        public async Task<IActionResult> GetLiqVeiculoAll(Guid veic)
        {
            var veiculo = await _vehicleRepository.GetAll();

            var valuesReturn = (from t in veiculo
                                group t by new { t.VehicleLicensePlate, }
                         into grp
                                select new
                                {
                                    grp.Key.VehicleLicensePlate,
                                    placa = grp.Key.VehicleLicensePlate
                                }).OrderBy(o => o.VehicleLicensePlate);
            return Json(valuesReturn);
        }

        public async Task<IActionResult> GetDespesasViagem(DateTime data_ini, DateTime data_fim, Guid veiculo)
        {
            var valores = await _expenseFinancialSettlementRepository.GetAll();

            var valuesReturn = (from t in valores
                                group t by new { t.FinancialSettlement.TravelDate.Month,  
                                    placa_veiculo = t.FinancialSettlement.Vehicle.VehicleLicensePlate, t.Arquivo
                                }
                         into grp
                                select new
                                {
                                    valor = grp.Sum(t => t.Valor_Total),
                                    mes = grp.Key.Month,
                                    placa_veiculo = grp.Key.placa_veiculo,
                                    arquivo = grp.Key.Arquivo
                                }).Where(w=> w.arquivo != "Performance Motorista").OrderBy(o => o.mes);

            return Json(valuesReturn);
        }

        public async Task<IActionResult> GetMotorista(Guid Moto)
        {
            var motorista = await _employeeRepository.GetAll();

            //var motorista = await _employeeRepository.Search(c => c.Id == Moto);
            var valuesReturn = motorista.Select(c =>
                new
                {
                    id = c.Id,
                    text = c.Name
                }
            ).OrderBy(o => o.text);
            return Json(valuesReturn);
        }

        public async Task<IActionResult> GetLiqMotoristaAll(Guid motorista)
        {
            var moto = await _employeeRepository.GetAll();

            var valuesReturn = (from t in moto
                                group t by new { t.Name, t.Responsibility.Description }
                         into grp
                                select new
                                {
                                    grp.Key.Name,
                                    nome = grp.Key.Name,
                                    cargo = grp.Key.Description
                                }).Where(w => w.cargo == "MOTORISTA" || w.cargo == "MOTORISTA CARRETEIRO" || w.cargo == "MOTORISTA CEGONHEIRO").OrderBy(o => o.Name);
            return Json(valuesReturn);
        }

        public async Task<IActionResult> GetLiqMotoristaId(DateTime data_ini, DateTime data_fim, Guid motorista)
        {
            var valores = await _expenseFinancialSettlementRepository.GetAll();

            var valuesReturn = (from t in valores
                        group t by new { t.ExpenseDate.Month, t.FinancialSettlement.Employee.Name, t.Arquivo }
                         into grp
                        select new
                        {
                            grp.Key.Name,
                            grp.Key.Arquivo,
                            valor = grp.Sum(t => t.Valor_Total),
                            mes = grp.Key.Month,
                            nome = grp.Key.Name
                        }).Where(w=> w.Arquivo == "Performance Motorista").OrderBy(o=> o.mes);

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
                                    frota = grp.Key.Description
                                }).Where(w => w.Arquivo == "Performance Motorista");

            return Json(valuesReturn);
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

        public async Task<IActionResult> CentroCustoAll(Guid centrocusto)
        {
            var tpdespesa = await _expenseTypeRepository.GetAll();

            var valuesReturn = (from t in tpdespesa
                                group t by new { t.Id, t.Description }
                         into grp
                                select new
                                {
                                    grp.Key.Description,
                                    id = grp.Key.Id,
                                    descricao = grp.Key.Description
                                }).OrderBy(o => o.Description);
            return Json(valuesReturn);
        }

        public async Task<IActionResult>CentroCustoId(DateTime data_ini, DateTime data_fim, Guid centrocusto)
        {
            var valores = await _expenseFinancialSettlementRepository.GetAll();

            var valuesReturn = (from t in valores
                                group t by new { t.ExpenseDate.Month, t.ExpenseType.Description, t.Arquivo }
                         into grp
                                select new
                                {
                                    grp.Key.Description,
                                    grp.Key.Arquivo,
                                    valor = grp.Sum(t => t.Valor_Total),
                                    mes = grp.Key.Month,
                                    centrocusto = grp.Key.Description
                                }).Where(w => w.Arquivo != "Performance Motorista");

            return Json(valuesReturn);
        }

        public async Task<IActionResult> GetLiqFornecedoresAll()
        {
            var fornec = await _supplierRepository.GetAll();

            var valuesReturn = (from t in fornec
                                group t by new { t.Id, t.Name }
                         into grp
                                select new
                                {
                                    grp.Key.Name,
                                    id = grp.Key.Id,
                                    nome = grp.Key.Name
                                }).OrderBy(o => o.nome);
            return Json(valuesReturn);
        }

        public async Task<IActionResult> GetLiqFornecedoresId(DateTime data_ini, DateTime data_fim, Guid fornecedor)
        {
            var valores = await _expenseFinancialSettlementRepository.GetAll();

            var valuesReturn = (from t in valores
                                group t by new { t.ExpenseDate.Month, t.Supplier.Name, t.Arquivo }
                         into grp
                                select new
                                {
                                    grp.Key.Name,
                                    grp.Key.Arquivo,
                                    valor = grp.Sum(t => t.Valor_Total),
                                    mes = grp.Key.Month,
                                    nome = grp.Key.Name
                                }).Where(w => w.Arquivo != "Performance Motorista");

            return Json(valuesReturn);
        }
    }
}
