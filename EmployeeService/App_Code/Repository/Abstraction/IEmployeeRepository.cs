using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeService
{
    internal interface IEmployeeRepository
    {
        Task<Employee> GetEmployeeByIDAsync(int employeeID);
        Task UpdateEmployeeAsync(Employee employee);
        Task<List<Employee>> GetAllAsync();
    }
}