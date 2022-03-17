using InterviewTest.Commons.Results;
using InterviewTest.Model;
using InterviewTest.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using System;
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
        public ProcessResult<Employee> Post(Employee employee)
        {
            var addEmployeeResult = _employeeService.AddEmployee(employee);
            return addEmployeeResult;
        }

        [HttpPut]
        public ProcessResult<Employee> Put(UpdateEmployeeRequest request)
        {
            var updateEmployeeResult = _employeeService.UpdateEmployee(request);
            return updateEmployeeResult;
        }

        [HttpDelete]
        public ProcessResult<Employee> Delete(Employee employee)
        {
            var removeEmployeeResult = _employeeService.RemoveEmployee(employee);
            return removeEmployeeResult;
        }
    }
}
