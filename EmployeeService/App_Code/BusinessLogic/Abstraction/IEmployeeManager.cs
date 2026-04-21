using System.IO;
using System.Threading.Tasks;

namespace EmployeeService.App_Code.BusinessLogic.Abstraction
{
    public interface IEmployeeManager
    {
        Task<Stream> GetEmployeeByIDAsync(int employeeID);
        Task EnableEmployeeAsync(int employeeID, int enable);
        Task<Stream> GetAllAsync();
    }
}