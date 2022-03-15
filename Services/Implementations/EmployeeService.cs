using InterviewTest.Commons.Results;
using InterviewTest.Model;
using InterviewTest.Services.Contracts;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;

namespace InterviewTest.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {

        private SqliteConnectionStringBuilder connectionStringBuilder => new SqliteConnectionStringBuilder { DataSource = "./SqliteDB.db" };

        public ProcessResult<List<Employee>> GetAll()
        {
            var employeeList = new List<Employee>();
            try
            {
                using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
                {
                    connection.Open();

                    var queryCmd = connection.CreateCommand();
                    queryCmd.CommandText = @"SELECT Name, Value FROM Employees";
                    using (var reader = queryCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employeeList.Add(new Employee
                            {
                                Name = reader.GetString(0),
                                Value = reader.GetInt32(1)
                            });
                        }
                    }
                }

                return new ProcessResult<List<Employee>>(true, "Employees obtained correctly", employeeList);
            }
            catch(Exception ex)
            {
                return new ProcessResult<List<Employee>>(false, "Exception throwed trying to obtain all employees", result: null, exception: ex);
            }
            
        }

        public ProcessResult<Employee> AddEmployee(Employee employee)
        {
            throw new System.NotImplementedException();
        }

        public ProcessResult<Employee> UpdateEmployee(Employee employee)
        {
            throw new System.NotImplementedException();
        }


        public ProcessResult RemoveEmployee(Employee employee)
        {
            throw new System.NotImplementedException();
        }

        
    }
}
