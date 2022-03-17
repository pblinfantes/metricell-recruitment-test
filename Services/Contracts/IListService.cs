using InterviewTest.Commons.Results;
using InterviewTest.Model;
using System.Collections.Generic;

namespace InterviewTest.Services.Contracts
{
    public interface IListService
    {
        ProcessResult<List<Employee>> GetEmployeesWithIncrementedValue();

        ProcessResult<long?> GetEmployeesValueSum();
    }
}
