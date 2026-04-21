using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeService
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetEmployeeByIDAsync(int employeeID);
        Task UpdateEmployeeAsync(Employee employee);
        Task<List<Employee>> GetAllAsync();
    }
}