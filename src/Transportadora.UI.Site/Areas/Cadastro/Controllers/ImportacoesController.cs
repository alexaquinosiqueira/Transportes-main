using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
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
    public class ImportacoesController : Controller
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IFleetRepository _fleetRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IResponsibilityRepository _responsibilityRepository;
        private readonly IVehicleTypeRepository _vehicleTypeRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IStateRepository _stateRepository;
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly IBankRepository _bankRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IExpenseTypeRepository _expensetypeRepository;
        private readonly IExpenseRepository _ExpenseRepository;
        private readonly IPaymentFormRepository _paymentFormRepository;
        private readonly ICostcenterRepository _costcenterRepository;
        private readonly IDocumentTypeRepository _documentTypeRepository;
        private readonly IExpenseFinancialSettlementRepository _expenseFinancialSettlementRepository;
        private readonly IFinancialSettlementRepository _financialSettlementRepository;
        private readonly IMapper _mapper;
        public ImportacoesController(IResponsibilityRepository responsibilityRepository,
                                ICompanyRepository companyRepository,
                                IFleetRepository fleetRepository,
                                ICustomerRepository customerRepository,
                                IVehicleRepository vehicleRepository,
                                IVehicleTypeRepository vehicleTypeRepository,
                                ISupplierRepository supplierRepository,
                                IEmployeeRepository employeeRepository,
                                ICityRepository cityRepository,
                                IBankAccountRepository bankAccountRepository,
                                IBankRepository bankRepository,
                                IStateRepository stateRepository,
                                IAddressRepository addressRepository,
                                IExpenseTypeRepository expensetypeRepository,
                                IExpenseRepository ExpenseRepository,
                                IPaymentFormRepository paymentFormRepository,
                                ICostcenterRepository costcenterRepository,
                                IDocumentTypeRepository documentTypeRepository,
                                IExpenseFinancialSettlementRepository expenseFinancialSettlementRepository,
                                IFinancialSettlementRepository financialSettlementRepository,

                                IMapper mapper)
        {
            _companyRepository = companyRepository;
            _responsibilityRepository = responsibilityRepository;
            _supplierRepository = supplierRepository;
            _customerRepository = customerRepository;
            _vehicleRepository = vehicleRepository;
            _employeeRepository = employeeRepository;
            _vehicleTypeRepository = vehicleTypeRepository;
            _bankRepository = bankRepository;
            _fleetRepository = fleetRepository;
            _bankAccountRepository = bankAccountRepository;
            _cityRepository = cityRepository;
            _addressRepository = addressRepository;
            _expensetypeRepository = expensetypeRepository;
            _stateRepository = stateRepository;
            _ExpenseRepository = ExpenseRepository;
            _costcenterRepository = costcenterRepository;
            _paymentFormRepository = paymentFormRepository;
            _documentTypeRepository = documentTypeRepository;
            _expenseFinancialSettlementRepository = expenseFinancialSettlementRepository;
            _financialSettlementRepository = financialSettlementRepository;
            _mapper = mapper;
        }

        // GET: Cadastro/Responsibilities
        public async Task<IActionResult> Index()
        {
            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);

            var users = await _responsibilityRepository.GetByCompanyId(companyId);

            return View(_mapper.Map<IEnumerable<ResponsibilityViewModel>>(users));
        }

        public async Task<IActionResult> Importacao_cargos(List<string> data)
        {

            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            int importados = 0;
            int duplicados = 0;
            ResponsibilityViewModel responsa = new ResponsibilityViewModel();


            foreach (var item in data)
            {
                var responsibility = _mapper.Map<Responsibility>(responsa);
                var cargos = await _responsibilityRepository.GetAll();
                var dados = item.Split(',');

                string desccargo = dados[0];

                cargos.ToList();

                string encontrei = "N";

                foreach (var itemc in cargos)
                {
                    if (itemc.Description == desccargo)
                        encontrei = "S";
                }

                if (encontrei == "S")
                    duplicados = duplicados + 1;

                if (encontrei == "N")
                {
                    responsibility.Description = dados[0];

                    if (dados[2] == "1")
                        responsibility.Active = true;
                    else
                        responsibility.Active = false;

                    responsibility.Company_Id = companyId;

                    await _responsibilityRepository.Add(responsibility);
                    importados = importados + 1;

                }
            }

            TempData["cls"] = "success";
            TempData["message"] = "PLANILHA IMPORTADA !!";


            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Importacao_frotas(List<string> data)
        {

            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            int importados = 0;
            int duplicados = 0;
            FleetViewModel responsa = new FleetViewModel();


            foreach (var item in data)
            {
                var frotas = _mapper.Map<Fleet>(responsa);
                var frotasrepo = await _fleetRepository.GetAll();
                var dados = item.Split(',');

                string desccargo = dados[0];

                frotasrepo.ToList();

                string encontrei = "N";

                foreach (var itemf in frotasrepo)
                {
                    if (itemf.Description == desccargo)
                        encontrei = "S";
                }

                if (encontrei == "S")
                    duplicados = duplicados + 1;

                if (encontrei == "N")
                {
                    frotas.Description = dados[0];

                    if (dados[1] == "1")
                        frotas.Active = true;
                    else
                        frotas.Active = false;

                    frotas.Company_Id = companyId;

                    await _fleetRepository.Add(frotas);
                    importados = importados + 1;
                }
            }

            TempData["cls"] = "success";
            TempData["message"] = "PLANILHA IMPORTADA !!";


            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Importacao_tipo_vei(List<string> data)
        {

            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            int importados = 0;
            int duplicados = 0;
            VehicleTypeViewModel tp_vei = new VehicleTypeViewModel();


            foreach (var item in data)
            {
                var tipo_veiculo = _mapper.Map<VehicleType>(tp_vei);
                var ltpvei = await _vehicleTypeRepository.GetAll();
                var dados = item.Split(',');

                string desctpve = dados[0];

                ltpvei.ToList();

                string encontrei = "N";

                foreach (var itemtpv in ltpvei)
                {
                    if (itemtpv.Description == desctpve)
                        encontrei = "S";
                }

                if (encontrei == "S")
                    duplicados = duplicados + 1;

                if (encontrei == "N")
                {
                    tipo_veiculo.Description = dados[0];

                    tipo_veiculo.Company_Id = companyId;

                    await _vehicleTypeRepository.Add(tipo_veiculo);
                    importados = importados + 1;
                }
            }

            TempData["cls"] = "success";
            TempData["message"] = "PLANILHA TIPO DE VEICULO IMPORTADA !!";


            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Importacao_veiculos(List<string> sVeiculoList)
        {
            string ocorrencia = string.Empty;
            string retorno_oco = string.Empty;

            try
            {


                IdentityManager identityManager = new IdentityManager(User);
                var companyId = Guid.Parse(identityManager.CompanyID);
                int importados = 0;
                int duplicados = 0;
                VehicleViewModel vei = new VehicleViewModel();

                //*** Validação do tipo de veiculo já cadastrado no sistema.
                var ltpvei = await _vehicleTypeRepository.GetAll();

                //*** Validação da frota já cadastrado no sistema.
                var frota = await _fleetRepository.GetAll();

                //*** Validação da frota já cadastrado no sistema.
                var moto = await _employeeRepository.GetAll();

                //*** Validação da estado cadastrado no sistema.
                var uf = await _stateRepository.GetAll();

                //*** Validação da cidades cadastrado no sistema.
                var cidades = await _cityRepository.GetAll();

                Guid tipo_vei_id = default(Guid);
                Guid frota_id = default(Guid);
                Guid moto_id = default(Guid);
                int state_id = 0;
                int cidade_id = 0;

                ltpvei.ToList();
                frota.ToList();
                moto.ToList();
                uf.ToList();
                cidades.ToList();

                foreach (var item in sVeiculoList)
                {
                    var veiculo = _mapper.Map<Vehicle>(vei);
                    var lvei = await _vehicleRepository.GetAll();

                    var dados = item.Split(',');

                    string descve = dados[5];

                    string uuf = dados[3];
                    string placa = dados[6];
                    placa = placa.ToUpper();

                    //uf
                    foreach (var item_uf in uf)
                    {
                        if (item_uf.Uf == dados[3])
                            state_id = item_uf.Id;
                    }

                    var nncidade = dados[4];

                    //cidades
                    foreach (var itemc in cidades)
                    {
                        if (itemc.Name.ToUpper() == nncidade.ToUpper())
                        {
                            veiculo.City_Id = itemc.Id;
                            cidade_id = itemc.Id;
                            break;
                        }
                    }

                    //frota
                    foreach (var item_frota in frota)
                    {
                        if (item_frota.Description.ToUpper() == dados[1].ToUpper())
                        {
                            frota_id = item_frota.Id;
                            veiculo.Fleet_Id = frota_id;
                        }
                    }

                    //veiculo
                    foreach (var item_tipo in ltpvei)
                    {
                        if (item_tipo.Description.ToUpper() == dados[0].ToUpper())
                        {
                            tipo_vei_id = item_tipo.Id;
                            veiculo.VehicleType_Id = tipo_vei_id;
                        }
                    }

                    //funcionario

                    string lok = string.Empty;

                    foreach (var item_moto in moto)
                    {
                        if (item_moto.Name.ToUpper().Contains(dados[2].ToUpper()))
                        {
                            moto_id = item_moto.Id;
                            veiculo.Employee_Id = item_moto.Id;
                            lok = "S";
                            break;
                        }
                    }

                    if (lok == "")
                        ocorrencia = ocorrencia + " | MOTORISTA " + dados[2].ToUpper() + " não cadastrado para " + descve + " PLACA " + dados[6] + Environment.NewLine;

                    if (moto_id == default(Guid))
                        ocorrencia = ocorrencia + "MOTORISTA " + dados[2].ToUpper() + " não cadastrado para " + descve + Environment.NewLine;

                    if (state_id == 0)
                        ocorrencia = ocorrencia + "UF do Veículo não cadastrado para " + descve + Environment.NewLine;

                    if (cidade_id == 0)
                        ocorrencia = ocorrencia + "CIDADE do Veículo não cadastrado para " + descve + Environment.NewLine;

                    if (tipo_vei_id == default(Guid))
                        ocorrencia = ocorrencia + "TIPO DO VEICULO não cadastrado para " + descve + Environment.NewLine;

                    if (frota_id == default(Guid))
                        ocorrencia = ocorrencia + "FROTA não cadastrada para " + descve + Environment.NewLine;

                    string encontrei = "N";


                    //veiculo
                    foreach (var itemveic in lvei)
                    {
                        if (itemveic.VehicleLicensePlate.ToUpper() == placa)
                        {
                            encontrei = "S";
                            //ocorrencia = ocorrencia + "Veiculo " + descve + ", já existe no cadastro." + Environment.NewLine;
                            duplicados = duplicados + 1;
                        }
                    }

                    if (encontrei == "N")
                    {
                        veiculo.Description = dados[5];
                        veiculo.VehicleLicensePlate = dados[6];

                        string nAno = dados[7];

                        veiculo.Year = Convert.ToInt32(nAno);

                        veiculo.Color = dados[8];
                        veiculo.Brand = dados[9];
                        veiculo.Model = dados[10];

                        string nchassi = dados[11];
                        veiculo.chassi = nchassi;

                        decimal renavam = Convert.ToDecimal(dados[12]);
                        veiculo.renavam = renavam;

                        decimal tank = Convert.ToDecimal(dados[13]);
                        veiculo.tank = tank;

                        decimal average = Convert.ToDecimal(dados[14]);
                        veiculo.average = average;

                        decimal litragem = Convert.ToDecimal(dados[15]);
                        veiculo.litragem = litragem;

                        veiculo.Company_Id = companyId;


                        if (ocorrencia == string.Empty)
                        {
                            await _vehicleRepository.Add(veiculo);
                            importados = importados + 1;
                        }
                    }
                }

                if (ocorrencia == string.Empty)
                {
                    TempData["cls"] = "success";
                    TempData["message"] = "PLANILHA DE VEÍCULO IMPORTADA !!";
                    ocorrencia = "PLANILHA DE VEÍCULO IMPORTADA !!" + Environment.NewLine + " ** Nenhuma ocorrência encontrada! *** ";
                    retorno_oco = ocorrencia;
                }
                else
                {
                    TempData["cls"] = "warning";
                    TempData["message"] = "VERIFIQUE AS INCONSISTENCIAS ENCONTRADAS!!" + Environment.NewLine + ocorrencia;
                    retorno_oco = "VERIFIQUE AS INCONSISTENCIAS ENCONTRADAS!!" + Environment.NewLine + ocorrencia;

                }

            }
            catch (Exception e)
            {
                retorno_oco = e.InnerException.Message;
                return Content(e.InnerException.Message);
                throw;
            }
            return Content(retorno_oco);
        }

        public async Task<IActionResult> Importacao_funcionarios(List<string> sFuncList)
        {

            string ocorrencia = string.Empty;
            string retorno_oco = string.Empty;

            try
            {


                IdentityManager identityManager = new IdentityManager(User);
                var companyId = Guid.Parse(identityManager.CompanyID);
                int importados = 0;
                int duplicados = 0;
                EmployeeViewModel func = new EmployeeViewModel();

                //*** Validação do tipo de veiculo já cadastrado no sistema.
                var ltpvei = await _vehicleTypeRepository.GetAll();

                //*** Validação da frota já cadastrado no sistema.
                var frota = await _fleetRepository.GetAll();

                //*** Validação da frota já cadastrado no sistema.
                var responsa = await _responsibilityRepository.GetAll();

                //*** Validação da estado cadastrado no sistema.
                var uf = await _stateRepository.GetAll();

                //*** Validação da cidades cadastrado no sistema.
                var cidades = await _cityRepository.GetAll();

                //*** Endereços
                var aadress = await _addressRepository.GetAll();


                Guid tipo_vei_id = default(Guid);
                Guid frota_id = default(Guid);
                Guid cargo_id = default(Guid);
                Guid adress_id = default(Guid);


                ltpvei.ToList();
                frota.ToList();
                responsa.ToList();
                uf.ToList();
                cidades.ToList();

                var lfunc = await _employeeRepository.GetAll();
                string lendereco = string.Empty;

                foreach (var item in sFuncList)
                {

                    EmployeeViewModel employeeViewModel = new EmployeeViewModel();

                    var employee = _mapper.Map<Employee>(employeeViewModel);

                    //var funcionario = _mapper.Map<Employee>(func);                    

                    var dados = item.Split(',');

                    string encontrei = "N";

                    foreach (var itemfunc in lfunc)
                    {
                        if (itemfunc.Name == dados[0])
                        {
                            encontrei = "S";
                            //ocorrencia = ocorrencia + "Funcionário " + dados[0] + ", já existe no cadastro." + Environment.NewLine;
                            duplicados = duplicados + 1;
                        }
                    }

                    if (encontrei == "N")
                    {
                        employee.Name = dados[0];

                        employee.NickName = dados[1];

                        employee.CelPhone = dados[2];

                        string ccargo = dados[6];

                        foreach (var item_cargo in responsa)
                        {
                            if (item_cargo.Description.ToUpper() == ccargo.ToUpper())
                            {
                                cargo_id = item_cargo.Id;
                                employee.Responsibility_Id = cargo_id;
                                break;
                            }
                        }

                        if (cargo_id == default(Guid))
                            ocorrencia = ocorrencia + "Cargo não cadastrado para " + dados[0] + Environment.NewLine;

                        if (dados[3] == "M")
                            employee.Genre = Genre.Male;
                        else
                            employee.Genre = Genre.Female;


                        string ddata = dados[4];

                        DateTime dtAniversario = Convert.ToDateTime(ddata);
                        employee.BirthDate = dtAniversario;

                        if (dados[5] == "CASADO")
                            employee.MaritalStatus = MaritalStatus.Married;

                        if (dados[5] == "SOLTEIRO")
                            employee.MaritalStatus = MaritalStatus.Single;
                        if (dados[5] == "VIÚVO")
                            employee.MaritalStatus = MaritalStatus.Widowed;
                        if (dados[5] == "DIVORCIADO")
                            employee.MaritalStatus = MaritalStatus.Divorced;
                        if (dados[5] == "SEPARADO")
                            employee.MaritalStatus = MaritalStatus.Separated;

                        DateTime dtAdm = Convert.ToDateTime(dados[7]);
                        employee.AdmissionDate = dtAdm;

                        decimal salario = Convert.ToDecimal(dados[8]) / 100;
                        employee.Salary = salario;

                        employee.Active = true;
                        employee.Company_Id = companyId;

                        employee.OrgaoExpedidor = " ";

                        employee.NomeMae = ".";

                        if (dados[23] == "")
                            employee.NumeroINSS = "";
                        else
                            employee.NumeroINSS = dados[23];

                        if (dados[22] == "")
                            employee.CPF = " ";
                        else
                            employee.CPF = dados[22];

                        if (dados[21] == "")
                            employee.DataEmissao = DateTime.Now;
                        else
                            employee.DataEmissao = Convert.ToDateTime(dados[21]);

                        if (dados[19] == "")
                            employee.RG = " ";
                        else
                            employee.RG = dados[19];

                        if (dados[18] == "")
                            employee.DataCNH = DateTime.Now;
                        else
                            employee.DataCNH = Convert.ToDateTime(dados[18]);

                        int tipocnh = 0;

                        if (dados[17] == "A")
                            tipocnh = 1;
                        if (dados[17] == "AE")
                            tipocnh = 2;
                        if (dados[17] == "C")
                            tipocnh = 3;
                        if (dados[17] == "D")
                            tipocnh = 4;
                        if (dados[17] == "E")
                            tipocnh = 5;
                        employee.TipoCNH = tipocnh;

                        var dadoscep = dados[15];

                        if (dados[16] == "")
                            employee.CNH = " ";
                        else
                            employee.CNH = dados[16];

                        employee.UFRG = ".";

                        lendereco = "";

                        if (dados[15] != "")
                        {
                            foreach (var iadres in aadress)
                            {
                                string cadres = "";
                                cadres = iadres.PostalCode.Replace(".", "").Replace("-", "");

                                if (cadres == dadoscep)
                                {
                                    employee.Address_Id = iadres.Id;
                                    lendereco = "ok";
                                    break;
                                }
                            }
                        }

                        if (lendereco == "") // se não encontrou o endereço, pego o cep da empresa e busco seu endereço.
                        {

                            foreach (var iadres in aadress)
                            {
                                string cadres = "";
                                cadres = iadres.PostalCode.Replace(".", "").Replace("-", "");

                                if (cadres == "32649270")
                                {
                                    employee.Address_Id = iadres.Id;
                                    lendereco = "ok";
                                    //fornecedor.Address_Id = Guid.Parse("FACBE011-D0BA-42CE-61C4-08D9320B5F87");
                                    break;
                                }
                            }
                        }

                        if (ocorrencia == string.Empty)
                        {


                            await _employeeRepository.Add(employee);

                            //await _employeeRepository.Add(funcionario);
                            importados = importados + 1;
                        }
                    }
                }

                if (ocorrencia == string.Empty)
                {
                    TempData["cls"] = "success";
                    TempData["message"] = "PLANILHA DE FUNCIONÁRIOS IMPORTADA !!";
                    ocorrencia = "PLANILHA DE FUNCIONÁRIOS IMPORTADA !!" + Environment.NewLine + " ** Nenhuma ocorrência encontrada! *** ";
                    retorno_oco = ocorrencia;
                }
                else
                {
                    TempData["cls"] = "warning";
                    TempData["message"] = "VERIFIQUE AS INCONSISTENCIAS ENCONTRADAS!!" + Environment.NewLine + ocorrencia;
                    retorno_oco = "VERIFIQUE AS INCONSISTENCIAS ENCONTRADAS!!" + Environment.NewLine + ocorrencia;

                }
            }
            catch (Exception e)
            {
                //retorno_oco = e.InnerException.Message;
                //Console.WriteLine(e.InnerException.Message);
                retorno_oco = e.InnerException.Message;

                return Content(e.InnerException.Message);

                throw;

            }
            //return RedirectToAction(nameof(Index));
            return Content(retorno_oco);
        }

        public async Task<IActionResult> Process(List<string> sForneceList)
        {
            string ocorrencia = string.Empty;
            string retorno_oco = string.Empty;

            try
            {


                IdentityManager identityManager = new IdentityManager(User);
                var companyId = Guid.Parse(identityManager.CompanyID);
                int importados = 0;
                int duplicados = 0;
                SupplierViewModel fornec = new SupplierViewModel();

                //*** Endereços
                var aadress = await _addressRepository.GetAll();

                //*** Validação da estado cadastrado no sistema.
                var uf = await _stateRepository.GetAll();

                //*** Validação da cidades cadastrado no sistema.
                var cidades = await _cityRepository.GetAll();


                int state_id = 0;

                uf.ToList();
                cidades.ToList();

                var lforn = await _supplierRepository.GetAll();

                var expense_type = await _expensetypeRepository.GetAll();

                foreach (var item in sForneceList)
                {
                    var dados = item.Split(',');

                    var fornecedor = _mapper.Map<Supplier>(fornec);

                    string nomefor = dados[0];

                    string uuf = dados[11];

                    fornecedor.Company_Id = companyId;


                    foreach (var item_uf in uf)
                    {
                        if (item_uf.Uf == dados[11])
                            state_id = item_uf.Id;
                    }

                    string encontrei = "N";

                    foreach (var itemveic in lforn)
                    {
                        if (itemveic.Name.ToUpper() == nomefor.ToUpper())
                        {
                            encontrei = "S";
                            //ocorrencia = ocorrencia + "Fornecedor " + nomefor + ", já existe no cadastro." + Environment.NewLine;
                            duplicados = duplicados + 1;
                        }
                    }


                    fornecedor.Name = nomefor;

                    if (dados[1] == "JURIDICO")
                        fornecedor.KindPerson = KindPerson.company;
                    else
                        fornecedor.KindPerson = KindPerson.person;

                    var cnpj = dados[2];
                    if (cnpj == "")
                        fornecedor.Document = " ";
                    else
                        fornecedor.Document = cnpj;

                    var lokdesp = "N";

                    foreach (var item_exp in expense_type)
                    {
                        var tpdesp = dados[3].ToUpper();

                        if (tpdesp == "PECAS")
                            tpdesp = "PEÇAS";

                        if (item_exp.Description.ToUpper() == tpdesp)
                        {
                            fornecedor.ExpenseType_Id = item_exp.Id;
                            lokdesp = "S";
                            break;
                        }
                    }

                    if (lokdesp == "N")
                        ocorrencia = ocorrencia + " Tipo de despesa para o fornecedor " + nomefor + " Não existe.";

                    var datacontato = dados[4];

                    if (datacontato == "")
                        fornecedor.DateSince = DateTime.Now;
                    else
                        fornecedor.DateSince = Convert.ToDateTime(datacontato);

                    var obs = dados[5];
                    if (obs == "")
                        fornecedor.Observation = obs;
                    else
                        fornecedor.Observation = " ";

                    var limite = dados[6];
                    limite = limite.Replace(".", "").Replace(",", ".");
                    fornecedor.CreditLimit = Convert.ToDecimal(limite);

                    var celf = dados[7];
                    if (celf == "")
                        fornecedor.CelPhone = " ";
                    else
                        fornecedor.CelPhone = celf;

                    var cep = dados[13];

                    var exped = dados[15];


                    var lendereco = "";
                    if (dados[13] != "")
                    {
                        foreach (var iadres in aadress)
                        {
                            string cadres = "";
                            cadres = iadres.PostalCode.Replace(".", "").Replace("-", "");

                            if (cadres == cep)
                            {
                                fornecedor.Address_Id = iadres.Id;
                                lendereco = "ok";
                                break;
                            }
                        }
                    }

                    if (lendereco == "") // se não encontrou o endereço, pego o cep da empresa e busco seu endereço.
                    {

                        foreach (var iadres in aadress)
                        {
                            string cadres = "";
                            cadres = iadres.PostalCode.Replace(".", "").Replace("-", "");

                            if (cadres == "32649270")
                            {
                                fornecedor.Address_Id = iadres.Id;
                                lendereco = "ok";
                                //fornecedor.Address_Id = Guid.Parse("FACBE011-D0BA-42CE-61C4-08D9320B5F87");
                                break;
                            }
                        }
                    }



                    if (encontrei == "N")
                    {
                        if (ocorrencia == string.Empty)
                        {
                            await _supplierRepository.Add(fornecedor);
                            importados = importados + 1;
                        }
                    }
                }

                if (ocorrencia == string.Empty)
                {
                    TempData["cls"] = "success";
                    TempData["message"] = "PLANILHA DE FORNECEDORES IMPORTADA !!";
                    ocorrencia = "PLANILHA DE FORNECEDORES IMPORTADA !!" + Environment.NewLine + " ** Nenhuma ocorrência encontrada! *** ";
                    retorno_oco = ocorrencia;
                }
                else
                {
                    TempData["cls"] = "warning";
                    TempData["message"] = "VERIFIQUE AS INCONSISTENCIAS ENCONTRADAS!!" + Environment.NewLine + ocorrencia;
                    retorno_oco = "VERIFIQUE AS INCONSISTENCIAS ENCONTRADAS!!" + Environment.NewLine + ocorrencia;

                }

            }
            catch (Exception e)
            {
                //retorno_oco = e.InnerException.Message;
                //Console.WriteLine(e.InnerException.Message);
                retorno_oco = e.InnerException.Message;

                return Content(retorno_oco);

                throw;

            }
            //return RedirectToAction(nameof(Index));
            return Content(retorno_oco);
        }

        [HttpPost]
        public async Task<IActionResult> Importacao_codigo_banco(List<string> data)
        {

            IdentityManager identityManager = new IdentityManager(User);
            var companyId = Guid.Parse(identityManager.CompanyID);
            int importados = 0;
            int duplicados = 0;
            BankViewModel responsa = new BankViewModel();
            string ocorrencia = string.Empty;


            foreach (var item in data)
            {
                var bancopesq = await _bankRepository.GetAll();
                var banco = _mapper.Map<Bank>(responsa);
                var dados = item.Split(',');
                bancopesq.ToList();

                string encontrei = "N";

                foreach (var itemb in bancopesq)
                {
                    if (itemb.Description.ToUpper() == dados[1].ToUpper())
                    {
                        encontrei = "S";
                        ocorrencia = ocorrencia + " Código Banco " + dados[0] + " já cadastrado.";
                    }
                }

                if (encontrei == "S")
                    duplicados = duplicados + 1;

                if (encontrei == "N")
                {
                    banco.Code = dados[0];
                    banco.Description = dados[1];
                    banco.Company_Id = companyId;

                    await _bankRepository.Add(banco);
                    importados = importados + 1;
                }
            }

            TempData["cls"] = "success";
            TempData["message"] = "PLANILHA IMPORTADA !!";


            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Importacao_contas_pagar(List<string> sContasPagarList)
        {
            string ocorrencia = string.Empty;
            string retorno_oco = string.Empty;

            try
            {
                IdentityManager identityManager = new IdentityManager(User);
                var companyId = Guid.Parse(identityManager.CompanyID);
                int importados = 0;
                int duplicados = 0;
                string encontrei = "N";

                var lvei = await _vehicleRepository.GetAll();

                var lempresa = await _companyRepository.GetAll();

                var fornecedor = await _supplierRepository.GetAll();

                var forma_pagto = await _paymentFormRepository.GetAll();

                var banco = await _bankRepository.GetAll();

                //*** centro de custo
                var centro_custo = await _costcenterRepository.GetAll();

                //*** Tipo de documento
                var tipo_doc = await _documentTypeRepository.GetAll();

                lempresa.ToList();

                Guid vei_id = default(Guid);
                Guid forne_id = default(Guid);

                foreach (var item in sContasPagarList)
                {
                    //var expense = _mapper.Map<Expense>(despesa);
                    var ldesp = await _ExpenseRepository.GetAll();
                    var dados = item.Split(',');

                    var vvalor_pag = dados[9];


                    //ICollection<ExpensePayment> exp = new List<ExpensePayment>()
                    //{
                    //    new ExpensePayment {AmountExpensePayment = Convert.ToDecimal( vvalor_pag), StatusExpensePayment = Status.opened}
                    //    //new ExpensePayment {DueDateExpensePayment = Convert.ToDateTime(ddata_pag)}
                    //};



                    var dd = new List<ExpensePayment>()
                    {
                        new ExpensePayment() {AmountExpensePayment = Convert.ToDecimal( vvalor_pag), StatusExpensePayment = Status.opened }
                    };

                    ExpenseViewModel eexpense = new ExpenseViewModel();


                    foreach (var itemc in eexpense.ExpensePayment)
                    {
                        itemc.AmountExpensePayment = Convert.ToDecimal(vvalor_pag);
                        itemc.StatusExpensePayment = StatusViewModel.opened;
                    }

                    eexpense.Company_Id = companyId;
                    eexpense.Date = DateTime.Now;
                    eexpense.status = StatusViewModel.opened;

                    //var expense = _mapper.Map<Expense>(eexpense);


                    // Verifico se a despesa já foi inserida.
                    foreach (var item_despesa in ldesp)
                    {
                        if (item_despesa.DocumentNumber == Convert.ToInt32(dados[3]))
                        {
                            encontrei = "S";
                            ocorrencia = ocorrencia + "Documento já importado " + Environment.NewLine;
                        }
                    }

                    ///empresa da planilha
                    foreach (var item_empresa in lempresa)
                    {
                        if (dados[0].ToUpper().Contains(item_empresa.RazaoSocial))
                        {
                            eexpense.Company_Id = item_empresa.Id;
                        }
                    }

                    //veiculo da planilha
                    foreach (var item_vei in lvei)
                    {
                        if (item_vei.VehicleLicensePlate.ToUpper() == dados[2].ToUpper())
                        {
                            vei_id = item_vei.Id;
                            eexpense.Vehicle_Id = vei_id;
                        }
                    }

                    //fornecedor da planilha
                    foreach (var item_forn in fornecedor)
                    {
                        if (item_forn.Name.ToUpper().Contains(dados[1].ToUpper()))
                        {
                            forne_id = item_forn.Id;
                            eexpense.Supplier_Id = forne_id;
                        }
                    }

                    eexpense.DocumentNumber = Convert.ToInt32(dados[3]);

                    var ddata = dados[4];
                    var ddata_pag = dados[8];
                    vvalor_pag = dados[9];

                    var vjuros = Convert.ToDecimal(dados[10]);

                    eexpense.Date = Convert.ToDateTime(ddata);
                    eexpense.Amount = Convert.ToDecimal(dados[5]);

                    var pago = dados[6];

                    if (pago == "PG")
                    {
                        eexpense.DueDate = Convert.ToDateTime(ddata_pag);
                        eexpense.PaidAmount = Convert.ToDecimal(vvalor_pag);

                    }


                    //forma de pagamento será boleto
                    string forma_pag = "BOLETO";
                    string ltipopag = string.Empty;

                    //eexpense.status = (StatusViewModel)Status.opened;

                    //eexpense.Status = (StatusViewModel)Status.opened;

                    //forma de pagamento boleto
                    foreach (var item_forma in forma_pagto)
                    {
                        if (item_forma.Description.ToUpper().Contains(forma_pag.ToUpper()))
                        {
                            var idpagto = item_forma.Id;
                            eexpense.PaymentForm_Id = idpagto;
                            ltipopag = "achei";
                            break;
                        }
                    }


                    string ccusto = string.Empty;
                    ccusto = "FINANCEIRO";
                    string ltipocentro = string.Empty;

                    //centro de custo
                    foreach (var item_ccusto in centro_custo)
                    {
                        if (item_ccusto.Description.ToUpper().Contains(ccusto.ToUpper()))
                        {
                            eexpense.CostCenter_Id = item_ccusto.Id;
                            ltipocentro = "achei";
                            break;
                        }
                    }

                    //tipo de documento
                    string ctipodoc = string.Empty;
                    ctipodoc = "BOLETO";
                    string ltipodoc = string.Empty;

                    foreach (var item_tpdoc in tipo_doc)
                    {
                        if (item_tpdoc.Description.ToUpper().Contains(ctipodoc.ToUpper()))
                        {
                            eexpense.DocumentType_Id = item_tpdoc.Id;
                            ltipodoc = "achei";
                            break;
                        }
                    }


                    if (ltipopag != "")
                    {
                        ltipopag = forma_pag + " Não encontrado no cadstro";
                        ocorrencia = ocorrencia + " " + ltipopag;
                    }

                    if (ltipocentro != "")
                    {
                        ltipocentro = " Centro de custo FINANCEIRO Não encontrado no cadastro";
                        ocorrencia = ocorrencia + " " + ltipocentro;
                    }

                    if (ltipodoc != "")
                    {
                        ltipodoc = " Tipo de documento BOLETO Não encontrado no cadastro";
                        ocorrencia = ocorrencia + " " + ltipodoc;
                    }

                    eexpense.Description = "Importado atraves planilha";
                    eexpense.Observation = "Informação importada planilha dia " + DateTime.Now; ;

                    var cbanco = string.Empty;
                    //banco da planilha
                    foreach (var item_banco in banco)
                    {
                        if (item_banco.Description.ToUpper().Contains(dados[8].ToUpper()))
                        {
                            var idbanco = item_banco.Id;
                            eexpense.BankAccount_Id = idbanco;
                        }
                    }

                    //exp.Add(eexpense.Amount);

                    //eexpense.ExpensePayment.Add()

                    if (encontrei == "N")
                    {
                        if (ocorrencia == string.Empty)
                        {
                            var Expense = _mapper.Map<Expense>(eexpense);
                            eexpense.status = StatusViewModel.closed;

                            await _ExpenseRepository.Add(Expense);
                            importados = importados + 1;
                        }
                    }
                }

                if (ocorrencia == string.Empty)
                {
                    TempData["cls"] = "success";
                    TempData["message"] = "PLANILHA DE CONTAS A PAGAR IMPORTADA !!";
                    ocorrencia = "PLANILHA DE CONTAS A PAGAR IMPORTADA !!" + Environment.NewLine + " ** Nenhuma ocorrência encontrada! *** ";
                    retorno_oco = ocorrencia;
                }
                else
                {
                    TempData["cls"] = "warning";
                    TempData["message"] = "VERIFIQUE AS INCONSISTENCIAS ENCONTRADAS!!" + Environment.NewLine + ocorrencia;
                    retorno_oco = "VERIFIQUE AS INCONSISTENCIAS ENCONTRADAS!!" + Environment.NewLine + ocorrencia;

                }

            }
            catch (Exception e)
            {
                retorno_oco = e.InnerException.Message;
                return Content(e.InnerException.Message);
                throw;
            }
            return Content(retorno_oco);
        }

        public async Task<IActionResult> Importacao_acertos(List<string> sAcertoList)
        {
            string ocorrencia = string.Empty;
            string retorno_oco = string.Empty;

            try
            {


                IdentityManager identityManager = new IdentityManager(User);
                var companyId = Guid.Parse(identityManager.CompanyID);
                int importados = 0;
                int duplicados = 0;
                FinancialSettlementViewModel ace = new FinancialSettlementViewModel();

                //*** Validação do tipo de veiculo já cadastrado no sistema.
                var lvei = await _vehicleRepository.GetAll();

                //*** Validação da frota já cadastrado no sistema.
                var moto = await _employeeRepository.GetAll();

                //*** Validação da estado cadastrado no sistema.
                var uf = await _stateRepository.GetAll();

                //*** Validação da cidades cadastrado no sistema.
                var cidades = await _cityRepository.GetAll();

                Guid tipo_vei_id = default(Guid);
                Guid frota_id = default(Guid);
                Guid moto_id = default(Guid);
                int state_id = 0;
                int cidade_id = 0;

                lvei.ToList();
                moto.ToList();
                uf.ToList();
                cidades.ToList();

                foreach (var item in sAcertoList)
                {
                    var acerto = _mapper.Map<FinancialSettlement>(ace);
                    var lace = await _financialSettlementRepository.GetAll();

                    var dados = item.Split(',');


                    ////uf de origem
                    //string uuf = dados[6];
                    //int nuf = 0;

                    //foreach (var item_uf in uf)
                    //{
                    //    nuf = nuf + 1;
                    //    if (item_uf.Uf == uuf)
                    //    {
                    //        acerto.CityOrigin.State.Id = item_uf.Id;
                    //        nuf = 0;
                    //        break;
                    //    }                            
                    //}
                    //if (nuf > 0)
                    //    ocorrencia = ocorrencia + " Estado de Orgigem não cadastrado para o acerto da viagem numero " + dados[3];


                    //cidades de origem
                    var nncidade = dados[5];
                    int ncidade = 0;

                    foreach (var itemc in cidades)
                    {
                        ncidade = ncidade + 1;
                        if (itemc.Name.ToUpper().Contains(nncidade.ToUpper()))
                        {
                            acerto.CityOrigin_Id = itemc.Id;
                            ncidade = 0;
                            break;
                        }
                    }
                    if (ncidade > 0)
                        ocorrencia =  "<br>" + ocorrencia  + " Cidade Origem não cadastrado para o acerto da viagem numero " + dados[3] + "<br>" + Environment.NewLine;

                    //uf de destino
                    //string uufd = dados[8];
                    //int nufd = 0;

                    //foreach (var item_uf in uf)
                    //{
                    //    nufd = nufd + 1;
                    //    if (item_uf.Uf == uufd)
                    //    {
                    //        acerto.DestinationCity.State.Id = item_uf.Id;
                    //        nufd = 0;
                    //        break;
                    //    }
                    //}
                    //if (nufd > 0)
                    //    ocorrencia = ocorrencia + " Estado de Destino não cadastrado para o acerto da viagem numero " + dados[3];


                    //cidades de destino
                    var nncidaded = dados[7];
                    int ncidaded = 0;

                    foreach (var itemcd in cidades)
                    {
                        ncidade = ncidade + 1;
                        if (itemcd.Name.ToUpper().Contains(nncidaded.ToUpper()))
                        {
                            acerto.DestinationCity_Id = itemcd.Id;
                            ncidaded = 0;
                            break;
                        }
                    }
                    if (ncidaded > 0)
                        ocorrencia = ocorrencia + " Cidade Destino não cadastrado para o acerto da viagem numero " + dados[3] + "<br>" + Environment.NewLine;


                    string placa = dados[1];
                    placa = placa.ToUpper();

                    //veiculo
                    int nvei = 0;
                    foreach (var item_vei in lvei)
                    {
                        nvei = nvei + 1;
                        if (item_vei.VehicleLicensePlate.ToUpper() == placa)
                        {
                            acerto.Vehicle_Id = item_vei.Id;
                            acerto.Fleet_Id = item_vei.Fleet_Id;
                            nvei = 0;
                            break;
                        }
                    }
                    if (nvei > 0)
                        ocorrencia = ocorrencia + "Veículo não cadastrado para o acerto da viagem numero " + dados[3] + "<br>" + Environment.NewLine;


                    //motorista
                    int nmoto = 0;

                    foreach (var item_moto in moto)
                    {
                        nmoto = nmoto + 1;
                        if (item_moto.Name.ToUpper().Contains(dados[0].ToUpper()))
                        {
                            moto_id = item_moto.Id;
                            acerto.Employee_Id = item_moto.Id;
                            nmoto = 0;
                            break;
                        }
                    }
                    if (nmoto > 0)
                        ocorrencia = ocorrencia + "Motorista não cadastrado para o acerto da viagem numero " + dados[3] + "<br>" + Environment.NewLine;

                    string encontrei = "N";

                    //acertos já incluidos
                    foreach (var item_acertos_ja_incluidos in lace)
                    {
                        if (item_acertos_ja_incluidos.Code == dados[3])
                        {
                            encontrei = "S";
                            duplicados = duplicados + 1;
                            ocorrencia = ocorrencia + "Acerto viagem N.º " + dados[3] + " já foi incluido. ";
                        }
                    }

                    if (encontrei == "N")
                    {
                        var data_viagem = Convert.ToDateTime(dados[4]);
                        acerto.TravelDate = data_viagem;

                        decimal frete = Convert.ToDecimal(dados[9].Replace('.', ','));

                        acerto.TotalShipping = frete;

                        decimal adianta = Convert.ToDecimal(dados[10].Replace('.', ','));
                        acerto.Advance = adianta;

                        decimal perfor = Convert.ToDecimal(dados[11].Replace('.', ','));

                        decimal percent = Math.Round((adianta / frete) * 100, 2);
                        acerto.TravelPercAdiantamento = percent;

                        decimal percviagem = Math.Round((perfor / frete) * 100, 2);
                        acerto.TravelPercentage = percviagem;

                        acerto.KmInitial = 0;
                        acerto.FinalKm = 0;


                        acerto.Date = DateTime.Now;

                        if (ocorrencia == string.Empty)
                        {
                            await _financialSettlementRepository.Add(acerto);
                            importados = importados + 1;
                        }
                    }
                }

                if (ocorrencia == string.Empty)
                {
                    TempData["cls"] = "success";
                    TempData["message"] = "PLANILHA DE ACERTO IMPORTADA !!";
                    ocorrencia = "PLANILHA DE ACERTO IMPORTADA !!" + Environment.NewLine + " ** Nenhuma ocorrência encontrada! *** ";
                    retorno_oco = ocorrencia;
                }
                else
                {
                    TempData["cls"] = "warning";
                    TempData["message"] = "VERIFIQUE AS INCONSISTENCIAS ENCONTRADAS!!" + Environment.NewLine + ocorrencia;
                    retorno_oco = "VERIFIQUE AS INCONSISTENCIAS ENCONTRADAS!!" + Environment.NewLine + ocorrencia;

                }

            }
            catch (Exception e)
            {
                retorno_oco = e.InnerException.Message;
                return Content(e.InnerException.Message);
                throw;
            }
            return Content(retorno_oco);
        }

        public async Task<IActionResult> Importacao_despesas_acertos(List<string> sADesp_certoList)
        {
            string ocorrencia = string.Empty;
            string retorno_oco = string.Empty;

            try
            {


                IdentityManager identityManager = new IdentityManager(User);
                var companyId = Guid.Parse(identityManager.CompanyID);
                int importados = 0;

                ExpenseFinancialSettlement despeacerto = new ExpenseFinancialSettlement();

                foreach (var item in sADesp_certoList)
                {
                    var dados = item.Split(',');

                    var despesa = _mapper.Map<ExpenseFinancialSettlement>(despeacerto);
                    var desp = await _expenseFinancialSettlementRepository.GetAll();
                    var fornecedor = await _supplierRepository.GetAll();
                    var tipo_desp = await _expensetypeRepository.GetAll();
                    var acerto = await _financialSettlementRepository.GetAll();

                    Guid id_despesa = default(Guid);
                    Guid id_forne = default(Guid);
                    Guid id_acerto = default(Guid);


                    /// acerto
                    int nacerto = 0;
                    foreach (var itema in acerto)
                    {
                        nacerto = nacerto + 1;
                        if (itema.Code == dados[0])
                        {
                            id_acerto = itema.Id;
                            despesa.FinancialSettlement_Id = id_acerto;
                            nacerto = 0;
                            break;
                        }
                    }
                    if (nacerto > 0)
                        ocorrencia = ocorrencia + " Acerto não encontrado para essa viagem " + dados[0];


                    /// fornecedor
                    int nfor = 0;
                    foreach (var itemf in fornecedor)
                    {
                        nfor = nfor + 1;
                        if (itemf.Name.ToUpper().Contains(dados[3].ToUpper()))
                        {
                            id_forne = itemf.Id;
                            despesa.Supplier_Id = id_forne;
                            nfor = 0;
                            break;
                        }
                    }
                    if (nfor > 0)
                        ocorrencia = ocorrencia + " Fornecedor não encontrado  " + dados[3];

                    /// despesa
                    int ndesp = 0;
                    foreach (var itemd in tipo_desp)
                    {
                        ndesp = ndesp + 1;
                        if (itemd.Description.ToUpper() == dados[2].ToUpper())
                        {
                            id_despesa = itemd.Id;
                            despesa.ExpenseType_Id = id_despesa;
                            ndesp = 0;
                            break;
                        }
                    }
                    if (ndesp > 0)
                        ocorrencia = ocorrencia + " Despesa não encontrada  " + dados[2];

                    string encontrei = "N";

                    foreach (var item_d in desp)
                    {
                        if (item_d.Amount == Convert.ToDecimal(dados[4]) && item_d.ExpenseType_Id == id_despesa && item_d.ExpenseDate == Convert.ToDateTime(dados[1]))
                        {
                            encontrei = "S";
                        }
                    }

                    string descve = dados[5];

                    string uuf = dados[3];
                    string placa = dados[6];
                    placa = placa.ToUpper();

                    if (encontrei == "N")
                    {
                        string ddata = dados[1];
                        decimal nvalor = Convert.ToDecimal(dados[4]) / 100;
                        decimal nvalor_total = Convert.ToDecimal(dados[6]) / 100;
                        decimal ndesconto = Convert.ToDecimal(dados[7]);
                        decimal nlitros = Convert.ToDecimal(dados[5]);

                        despesa.ExpenseDate = Convert.ToDateTime(ddata);
                        despesa.Amount = nvalor;
                        despesa.Valor_Total = nvalor_total;
                        despesa.Desconto = ndesconto;
                        despesa.Litros = nlitros;

                        if (ocorrencia == string.Empty)
                        {
                            await _expenseFinancialSettlementRepository.Add(despesa);
                            importados = importados + 1;
                        }
                    }
                }

                if (ocorrencia == string.Empty)
                {
                    TempData["cls"] = "success";
                    TempData["message"] = "PLANILHA DESPESAS DOS ACERTOS IMPORTADA !!";
                    ocorrencia = "PLANILHA DESPESAS DOS ACERTOS IMPORTADA !!" + Environment.NewLine + " ** Nenhuma ocorrência encontrada! *** ";
                    retorno_oco = ocorrencia;
                }
                else
                {
                    TempData["cls"] = "warning";
                    TempData["message"] = "VERIFIQUE AS INCONSISTENCIAS ENCONTRADAS!!" + Environment.NewLine + ocorrencia;
                    retorno_oco = "VERIFIQUE AS INCONSISTENCIAS ENCONTRADAS!!" + Environment.NewLine + ocorrencia;

                }

            }
            catch (Exception e)
            {
                retorno_oco = e.InnerException.Message;
                return Content(e.InnerException.Message);
                throw;
            }
            return Content(retorno_oco);
        }


    }
}
