using System;
using System.Collections.Generic;
using InterviewTest.Commons.Results;
using InterviewTest.Model;
using InterviewTest.Services.Contracts;
using Microsoft.Data.Sqlite;

namespace InterviewTest.Services.Implementations
{
    public class ListService : IListService
    {

        private SqliteConnectionStringBuilder connectionStringBuilder => new SqliteConnectionStringBuilder { DataSource = "./SqliteDB.db" };

        /// <summary>
        /// Get all employees, incrementing the Value field depending upon the first letter of the Name field.
        /// E = Value + 1 / G = Value + 10 / Rest of the letters = Value + 100
        /// </summary>
        public ProcessResult<List<Employee>> GetEmployeesWithIncrementedValue()
        {
            var employeeList = new List<Employee>();
            try
            {
                using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
                {
                    connection.Open();

                    var queryCmd = connection.CreateCommand();
                    queryCmd.CommandText = @"SELECT 
                                                Name, 
                                                CASE
                                                    WHEN Name LIKE 'E%' THEN Value + 1
                                                    WHEN Name LIKE 'G%' THEN Value + 10
                                                    ELSE Value+100
												END AS IncrementedValue
                                            FROM 
                                                Employees 
                                            ORDER BY 
                                                Name asc";
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

                return new ProcessResult<List<Employee>>(true, "Employees with incremented values obtained correctly", employeeList);
            }
            catch (Exception ex)
            {
                return new ProcessResult<List<Employee>>(false, "Exception throwed trying to obtain all employees with incremented values", result: null, exception: ex);
            }
        }

        /// <summary>
        /// Get the sum of the employees Value field whose names start with "A", "B" or "C"
        /// </summary>
        public ProcessResult<long?> GetEmployeesValueSum()
        {
            long? valueSum = null;
            try
            {
                using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
                {
                    connection.Open();

                    var queryCmd = connection.CreateCommand();
                    queryCmd.CommandText = @"SELECT 
                                                SUM(Value)
                                            FROM 
                                                Employees
                                            WHERE 
                                                Name LIKE 'A%' 
                                                OR Name LIKE 'B%' 
                                                OR Name LIKE 'C%'";
                    using (var reader = queryCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            valueSum = reader.GetInt64(0);
                        }
                    }
                }

                return new ProcessResult<long?>(true, "Employees with incremented values obtained correctly", valueSum);
            }
            catch (Exception ex)
            {
                return new ProcessResult<long?>(false, "Exception throwed trying to obtain the sum of all employees value", result: null, exception: ex);
            }
        }

    }
}
