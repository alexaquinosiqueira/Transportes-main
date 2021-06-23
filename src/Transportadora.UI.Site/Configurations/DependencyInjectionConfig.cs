
using Transportadora.Business.Interfaces;
using Transportadora.Data.Context;
using Transportadora.Data.Repository;
using Microsoft.Extensions.DependencyInjection;
using Transportadora.Business.Notifications;
using Transportadora.Business.Services;
using Transportadora.Business.Models;

namespace Transportadora.UI.Site.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<TruckDbContext>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProfileRepository, ProfileRepository>();
            services.AddScoped<IModuleRepository, ModuleRepository>();
            services.AddScoped<IProfileActionFuncionalityRepository, ProfileActionFuncionalityRepository>();

            services.AddScoped<IResponsibilityRepository, ResponsibilityRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            services.AddScoped<IVehicleTypeRepository, VehicleTypeRepository>();
            services.AddScoped<IFleetRepository, FleetRepository>();
            services.AddScoped<IVehicleRepository, VehicleRepository>();

            services.AddScoped<IInsuranceCompanyRepository, InsuranceCompanyRepository>();
            services.AddScoped<IInsuranceSituationRepository, InsuranceSituationRepository>();
            services.AddScoped<IInsuranceRepository, InsuranceRepository>();

            services.AddScoped<IBankRepository, BankRepository>();
            services.AddScoped<IBankAccountRepository, BankAccountRepository>();
            services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();

            
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<ICostcenterRepository, CostCenterRepository>();
            services.AddScoped<IDocumentTypeRepository, DocumentTypeRepository>();
            services.AddScoped<IPaymentFormRepository, PaymentFormRepository>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            services.AddScoped<IInvoicePaymentRepository, InvoicePaymentRepository>();
            services.AddScoped<IExpenseRepository, ExpenseRepository>();
            services.AddScoped<IExpensePaymentRepository, ExpensePaymentRepository>();

            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IStateRepository, StateRepository>();
            services.AddScoped<IFinancialSettlementRepository, FinancialSettlementRepository>();
            services.AddScoped<IExpenseFinancialSettlementRepository, ExpenseFinancialSettlementRepository>();
            services.AddScoped<IExpenseTypeRepository, ExpenseTypeRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();

            services.AddScoped<IAuditLogRepository, AuditLogRepository>();

            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IParameterRepository, ParameterRepository>();

            services.AddScoped<IFluxoCaixaRepository, FluxoCaixaRepository>();

            return services;
        }
    }
}