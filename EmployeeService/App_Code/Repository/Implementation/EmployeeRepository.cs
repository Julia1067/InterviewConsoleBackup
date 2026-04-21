using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeService
{
    internal class EmployeeRepository : IEmployeeRepository
    {
        private readonly string connString;

        public EmployeeRepository(string connString)
        {
            this.connString = connString;
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            using (var sqlConnection = new SqlConnection(connString))
            {
                await sqlConnection.OpenAsync();
                using (var cmd = new SqlCommand(@"
                        UPDATE Employee
                        SET Name = @name
                          , ManagerId = @managerId
                          , IsEnabled = @isEnabled
                        WHERE Id = @employeeId
                    ", sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@employeeId", employee.ID);
                    cmd.Parameters.AddWithValue("@name", employee.Name);
                    cmd.Parameters.AddWithValue("@managerId", employee.ManagerID);
                    cmd.Parameters.AddWithValue("@isEnabled", employee.IsEnabled);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task<List<Employee>> GetAllAsync()
        {
            var employees = new List<Employee>();
            using (var sqlConnection = new SqlConnection(connString))
            {
                await sqlConnection.OpenAsync();
                using (var cmd = new SqlCommand(@"
                             SELECT ID
                                , Name
                                , ManagerId
                                , IsEnabled
                            FROM Employee
                        ", sqlConnection))
                {
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            employees.Add(new Employee
                            {
                                ID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                ManagerID = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2),
                                IsEnabled = reader.GetBoolean(3),
                            });
                        }
                    }
                }
            }
            return employees;
        }

        public async Task<Employee> GetEmployeeByIDAsync(int employeeID)
        {
            var employees = new List<Employee>();
            var root = new Employee();
            using (var sqlConnection = new SqlConnection(connString))
            {
                await sqlConnection.OpenAsync();
                using (var cmd = new SqlCommand(@"
                    SELECT Id
                       , Name
                       , ManagerId
                       , IsEnabled
                    FROM Employee
                ", sqlConnection))
                {
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            employees.Add(new Employee
                            {
                                ID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                ManagerID = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2),
                                IsEnabled = reader.GetBoolean(3),
                                Employees = new List<Employee>()
                            });
                        }
                    }
                }
            }
            var lookup = employees.ToDictionary(e => e.ID);

            foreach (var employee in employees)
            {
                if (employee.ManagerID.HasValue
                    && lookup.ContainsKey(employee.ManagerID.Value))
                {
                    lookup[employee.ManagerID.Value].Employees.Add(employee);
                }
                if (employee.ID == employeeID)
                {
                    root = employee;
                }
            }
            return root;
        }
    }
}