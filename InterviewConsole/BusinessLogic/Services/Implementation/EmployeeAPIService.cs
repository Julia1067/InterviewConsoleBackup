using InterviewConsole.BusinessLogic.Models;
using InterviewConsole.BusinessLogic.Services.Abstraction;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InterviewConsole.BusinessLogic.Services.Implementation
{
    internal class EmployeeAPIService : IEmployeeAPIService
    {
        private readonly string _baseUrl;
        private readonly HttpClient _client;
        internal EmployeeAPIService()
        {
            _client = new HttpClient();
            _baseUrl = ConfigurationManager.AppSettings["ServiceBaseUrl"];
        }
        public async Task EnableEmployeeAsync(int employeeID, int enable)
        {
            string url = $"{_baseUrl}/EnableEmployee?id={employeeID}&enable={enable}";
            await _client.PutAsync(url, null);
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                string url = $"{_baseUrl}/GetEmployees";
                string json = await _client.GetStringAsync(url);
                employees = JsonConvert.DeserializeObject<List<Employee>>(json);
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            return employees;
        }

        public async Task<Employee> GetEmployeeByIDAsync(int employeeID)
        {
            string url = $"{_baseUrl}/GetEmployeeById?id={employeeID}";
            string json = await _client.GetStringAsync(url);
            var employees = JsonConvert.DeserializeObject<Employee>(json);
            return employees;
        }
    }
}
