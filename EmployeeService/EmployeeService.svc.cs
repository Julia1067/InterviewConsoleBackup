using EmployeeService.App_Code.BusinessLogic.Abstraction;
using EmployeeService.App_Code.BusinessLogic.Implementation;
using System;
using System.Configuration;
using System.IO;
using System.ServiceModel;
using System.Threading.Tasks;


namespace EmployeeService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class EmployeeService : IEmployeeService
    {

        private readonly IEmployeeManager _manager;

        public EmployeeService()
        {
            string connString = ConfigurationManager
                .ConnectionStrings["Employee"]
                .ConnectionString;
            _manager = new EmployeeManager(connString);
        }

        public async Task<Stream> GetEmployeeById(int id)
        {
            try
            {
                var result = await _manager.GetEmployeeByIDAsync(id);
                return result;
            }
            catch (Exception ex)
            {
                throw new FaultException("Error getting employee by ID: " + ex.Message);
            }
        }

        public async Task EnableEmployee(int id, int enable)
        {
            try
            {
                await _manager.EnableEmployeeAsync(id, enable);
            }
            catch (Exception ex)
            {
                throw new FaultException("Error enabling employee: " + ex.Message);
            }
        }

        public async Task<Stream> GetEmployees()
        {
            try
            {
                var result = await _manager.GetAllAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new FaultException("Error getting employees: " + ex.Message);
            }
        }
    }


}