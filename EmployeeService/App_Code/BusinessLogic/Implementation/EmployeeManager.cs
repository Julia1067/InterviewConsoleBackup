using EmployeeService.App_Code.BusinessLogic.Abstraction;
using Newtonsoft.Json;
using System.Configuration;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.App_Code.BusinessLogic.Implementation
{
    public class EmployeeManager : IEmployeeManager
    {
        private readonly IEmployeeRepository _repo;

        public EmployeeManager(string connString)
        {
            _repo = new EmployeeRepository(connString);
        }

        public async Task EnableEmployeeAsync(int employeeID, int enable)
        {
            var employee = await _repo.GetEmployeeByIDAsync(employeeID);
            employee.IsEnabled = enable == 1;

            await _repo.UpdateEmployeeAsync(employee);
        }

        public async Task<Stream> GetAllAsync()
        {
            var result = await _repo.GetAllAsync();
            string json = JsonConvert.SerializeObject(result);

            return new MemoryStream(Encoding.UTF8.GetBytes(json));
        }

        public async Task<Stream> GetEmployeeByIDAsync(int employeeID)
        {
            var result = await _repo.GetEmployeeByIDAsync(employeeID);

            string json = JsonConvert.SerializeObject(result);
            return new MemoryStream(Encoding.UTF8.GetBytes(json));
        }
    }
}