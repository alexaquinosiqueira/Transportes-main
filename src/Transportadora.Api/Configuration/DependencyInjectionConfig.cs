using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transportadora.Business.Interfaces;
using Transportadora.Data.Context;
using Transportadora.Data.Repository;

namespace Transportadora.Api.Configuration
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


            return services;
        }
    }
}
