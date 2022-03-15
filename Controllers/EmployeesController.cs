using InterviewTest.Model;
using InterviewTest.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace InterviewTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {

        private IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public List<Employee> Get()
        {
            var employeesResult = _employeeService.GetAll();

            return employeesResult.Success
                   ? employeesResult.Result
                   : new List<Employee>();
        }

        [HttpPost]
        public IActionResult Post(Employee employee)
        {
            return Ok();
        }

        public IActionResult Put(Employee employee)
        {
            return Ok();
        }
    }
}
