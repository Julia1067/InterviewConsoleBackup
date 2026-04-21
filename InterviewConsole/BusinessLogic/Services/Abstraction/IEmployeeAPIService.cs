using InterviewConsole.BusinessLogic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InterviewConsole.BusinessLogic.Services.Abstraction
{
    public interface IEmployeeAPIService
    {
        Task<Employee> GetEmployeeByIDAsync(int employeeID);
        Task EnableEmployeeAsync(int employeeID, int enable);
        Task<List<Employee>> GetEmployeesAsync();
    }
}
