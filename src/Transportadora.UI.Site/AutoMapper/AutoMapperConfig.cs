using AutoMapper;
using Transportadora.Business.Models;
using Transportadora.UI.Site.ViewModels;

namespace Transportadora.UI.Site.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<User, LoginViewModel>().ReverseMap();
            CreateMap<ProfileT, ProfileViewModel>().ReverseMap();
            CreateMap<Module, ModuleViewModel>().ReverseMap();
            CreateMap<Funcionality, FuncionalityViewModel>().ReverseMap();
            CreateMap<ActionFuncionality, ActionFuncionalityViewModel>().ReverseMap();
            CreateMap<ProfileActionFuncionality, ProfileActionFuncionalityViewModel>().ReverseMap();

            CreateMap<Responsibility, ResponsibilityViewModel>().ReverseMap();
            CreateMap<Address, AddressViewModel>().ReverseMap();
            CreateMap<Genre, GenreViewModel>().ReverseMap();
            CreateMap<MaritalStatus, MaritalStatusViewModel>().ReverseMap();
            CreateMap<Employee, EmployeeViewModel>().ReverseMap();
            CreateMap<VehicleType, VehicleTypeViewModel>().ReverseMap();
            CreateMap<VehicleClass, VehicleClassViewModel>().ReverseMap();
            CreateMap<Fleet, FleetViewModel>().ReverseMap();
            CreateMap<Vehicle, VehicleViewModel>().ReverseMap();
            CreateMap<InsuranceSituation, InsuranceSituationViewModel>().ReverseMap();
            CreateMap<InsuranceCompany, InsuranceCompanyViewModel>().ReverseMap();
            CreateMap<Insurance, InsuranceViewModel>().ReverseMap();

            CreateMap<Bank, BankViewModel>().ReverseMap();
            CreateMap<BankAccount, BankAccountViewModel>().ReverseMap();
            CreateMap<PaymentMethod, PaymentMethodViewModel>().ReverseMap();

            CreateMap<Customer, CustomerViewModel>().ReverseMap();
            CreateMap<Supplier, SupplierViewModel>().ReverseMap();
            CreateMap<CostCenter, CostCenterViewModel>().ReverseMap();
            CreateMap<DocumentType, DocumentTypeViewModel>().ReverseMap();
            CreateMap<PaymentForm, PaymentFormViewModel>().ReverseMap();
            CreateMap<Invoice, InvoiceViewModel>().ReverseMap();
            CreateMap<InvoicePayment, InvoicePaymentViewModel>().ReverseMap();
            CreateMap<Expense, ExpenseViewModel>().ReverseMap();
            CreateMap<ExpensePayment, ExpensePaymentViewModel>().ReverseMap();
            CreateMap<ExpenseType, ExpenseTypeViewModel>().ReverseMap();

            CreateMap<City, CityViewModel>().ReverseMap();
            CreateMap<State, StateViewModel>().ReverseMap();
            CreateMap<FinancialSettlement, FinancialSettlementViewModel>().ReverseMap();
            CreateMap<ExpenseFinancialSettlement, ExpenseFinancialSettlementViewModel>().ReverseMap();
            CreateMap<ExpenseSupplier, ExpenseSupplierViewModel>().ReverseMap();
            CreateMap<Company, CompanyViewModel>().ReverseMap();
            CreateMap<AuditLog, AuditLogViewModel>().ReverseMap();
            CreateMap<UserCompany, UserCompanyViewModel>().ReverseMap();
            CreateMap<Parameter, ParameterViewModel>().ReverseMap();
            CreateMap<FluxoCaixa, FluxoCaixaViewModel>().ReverseMap();
        }
    }
}