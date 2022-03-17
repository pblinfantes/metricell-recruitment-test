using System;
using System.Collections.Generic;
using InterviewTest.Commons.Results;
using InterviewTest.Model;
using InterviewTest.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace InterviewTest.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ListController : ControllerBase
    {
        private IListService _listService;

        public ListController(IListService listService)
        {
            _listService = listService;
        }

        [HttpGet]
        public List<Employee> GetEmployeesWithIncrementedValue()
        {
            var incrementedEmployeesResult = _listService.GetEmployeesWithIncrementedValue();

            return incrementedEmployeesResult.Success
                   ? incrementedEmployeesResult.Result
                   : new List<Employee>();
        }

        [HttpGet]
        public long? GetEmployeesValueSum()
        {
            var employeesValueSumResult = _listService.GetEmployeesValueSum();

            return employeesValueSumResult.Success
                   ? employeesValueSumResult.Result
                   : null;
        }

    }
}
