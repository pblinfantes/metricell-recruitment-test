using InterviewTest.Commons.Results;
using InterviewTest.Model;
using System.Collections.Generic;

namespace InterviewTest.Services.Contracts
{
    public interface IEmployeeService
    {
        public ProcessResult<List<Employee>> GetAll();

        public ProcessResult<Employee> AddEmployee(Employee employee);

        public ProcessResult<Employee> UpdateEmployee(Employee employee);

        public ProcessResult RemoveEmployee(Employee employee);

    }
}
