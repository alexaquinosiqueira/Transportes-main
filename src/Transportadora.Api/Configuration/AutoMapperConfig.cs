using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transportadora.Api.ViewModels;
using Transportadora.Business.Models;

namespace Transportadora.Api.Configuration
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

        }
    }
}
