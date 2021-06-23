using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Transportadora.Business.Interfaces;
using Transportadora.Api.ViewModels;
using AutoMapper;
using Transportadora.Business.Models;
using Transportadora.Api.Security;

namespace Transportadora.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [BasicAuth]
    public abstract class MainController : ControllerBase
    {
        
    }
    [Route("Employees")]
    public class EmployeesController : MainController
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        public EmployeesController(IEmployeeRepository employeeRepository,
                                   IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeViewModel>>> GetAll()
        {
            var employee = await _employeeRepository.GetAll(); 
            return Ok(_mapper.Map<IEnumerable<EmployeeViewModel>>(employee));
        }
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<EmployeeViewModel>> GetById(Guid id)
        {
            var employee = _mapper.Map<EmployeeViewModel>(await _employeeRepository.GetById(id));

            if (employee == null) return NotFound();

            return employee;
        }
        [HttpPost]
        public async Task<ActionResult<EmployeeViewModel>> Add(EmployeeViewModel employeeViewModel)
        {
            if (!ModelState.IsValid) return BadRequest();

            var employee = _mapper.Map<Employee>(employeeViewModel);

            await _employeeRepository.Add(employee);

            return Ok(employee);
        }
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<EmployeeViewModel>> Update(Guid id, EmployeeViewModel employeeViewModel)
        {
            if (id != employeeViewModel.Id) return BadRequest();

            if (!ModelState.IsValid) return BadRequest();

            var employee = _mapper.Map<Employee>(employeeViewModel);

            await _employeeRepository.Update(employee);

            return Ok(employee);
        }
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<EmployeeViewModel>> Delete(Guid id)
        {
            var employee = _mapper.Map<EmployeeViewModel>(await _employeeRepository.GetById(id));

            if (employee == null) return NotFound();

            await _employeeRepository.Remove(_mapper.Map<Employee>(employee));

            return Ok(employee);
        }
    }
}
