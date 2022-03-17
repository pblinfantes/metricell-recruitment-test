using InterviewTest.Commons.Requests;
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
                    queryCmd.CommandText = @"SELECT Name, Value FROM Employees ORDER BY Name asc";
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
            var employeeList = new List<Employee>();
            try
            {
                using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
                {
                    connection.Open();

                    var queryCmd = connection.CreateCommand();
                    queryCmd.CommandText = $"INSERT INTO Employees (Name, Value) VALUES (@Name, @Value)";
                    queryCmd.Parameters.Add(new SqliteParameter("@Name", employee.Name));
                    queryCmd.Parameters.Add(new SqliteParameter("@Value", employee.Value));
                    queryCmd.ExecuteNonQuery();

                }

                return new ProcessResult<Employee>(true, "Employees inserted correctly", employee);
            }
            catch (Exception ex)
            {
                return new ProcessResult<Employee>(false, "Exception throwed trying to insert employee", result: null, exception: ex);
            }
        }

        public ProcessResult<Employee> UpdateEmployee(UpdateEmployeeRequest request)
        {
            try
            {
                using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
                {
                    connection.Open();

                    var queryCmd = connection.CreateCommand();
                    queryCmd.CommandText = $"UPDATE Employees SET Name=@NewName, Value=@NewValue Where Name = @OriginalName AND Value = @OriginalValue";
                    queryCmd.Parameters.Add(new SqliteParameter("@NewName", request.UpdatedEmployee.Name));
                    queryCmd.Parameters.Add(new SqliteParameter("@NewValue", request.UpdatedEmployee.Value));
                    queryCmd.Parameters.Add(new SqliteParameter("@OriginalName", request.OriginalEmployee.Name));
                    queryCmd.Parameters.Add(new SqliteParameter("@OriginalValue", request.OriginalEmployee.Value));
                    queryCmd.ExecuteNonQuery();
                }

                return new ProcessResult<Employee>(true, "Employee removed correctly", result: request.UpdatedEmployee);
            }
            catch (Exception ex)
            {
                return new ProcessResult<Employee>(false, "Exception throwed trying to update employee", result: null, exception: ex);
            }
        }


        public ProcessResult<Employee> RemoveEmployee(Employee employee)
        {
            try
            {
                using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
                {
                    connection.Open();

                    var queryCmd = connection.CreateCommand();
                    queryCmd.CommandText = $"DELETE FROM Employees Where Name = @Name AND Value = @Value";
                    queryCmd.Parameters.Add(new SqliteParameter("@Name", employee.Name));
                    queryCmd.Parameters.Add(new SqliteParameter("@Value", employee.Value));
                    queryCmd.ExecuteNonQuery();
                }

                return new ProcessResult<Employee>(true, "Employee removed correctly", result: employee);
            }
            catch (Exception ex)
            {
                return new ProcessResult<Employee>(false, "Exception throwed trying to remove employee", result: null, exception: ex);
            }
        }

        
    }
}
