using System;
using System.Threading.Tasks;
using Transportadora.Business.Interfaces;
using Transportadora.Business.Models;
using Transportadora.Business.Models.Validations;

namespace Transportadora.Business.Services
{
    public class EmployeeService : BaseService, IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository,
                                 INotificador notificador) : base(notificador)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task Add(Employee employee)
        {
            if (!ExecutarValidacao(new EmployeeValidation(), employee)) return;


            await _employeeRepository.Add(employee);

        }

        public Task Remove(Employee employee)
        {
            throw new NotImplementedException();
        }

        public async Task Update(Employee employee)
        {
            if (!ExecutarValidacao(new EmployeeValidation(), employee)) return;


            await _employeeRepository.Update(employee);
        }
    }
}
