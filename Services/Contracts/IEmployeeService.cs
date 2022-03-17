using InterviewTest.Model;
using InterviewTest.Commons.Results;
using System.Collections.Generic;

namespace InterviewTest.Services.Contracts
{
    public interface IEmployeeService
    {
        public ProcessResult<List<Employee>> GetAll();

        public ProcessResult<Employee> AddEmployee(Employee employee);

        public ProcessResult<Employee> UpdateEmployee(UpdateEmployeeRequest employee);

        public ProcessResult<Employee> RemoveEmployee(Employee employee);

    }
}
