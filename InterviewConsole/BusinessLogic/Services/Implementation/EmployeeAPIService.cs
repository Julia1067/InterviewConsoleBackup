using InterviewConsole.BusinessLogic.Models;
using InterviewConsole.BusinessLogic.Services.Abstraction;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.ServiceModel;
using System.Threading.Tasks;

namespace InterviewConsole.BusinessLogic.Services.Implementation
{
    public class EmployeeAPIService : IEmployeeAPIService
    {
        private readonly string _baseUrl;
        private static readonly HttpClient _client = new HttpClient();
        public EmployeeAPIService()
        {
            _baseUrl = ConfigurationManager.AppSettings["ServiceBaseUrl"];
        }
        public async Task EnableEmployeeAsync(int employeeID, int enable)
        {
            try
            {
                string url = $"{_baseUrl}/EnableEmployee?id={employeeID}&enable={enable}";
                var response = await _client.PutAsync(url, null);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new FaultException("Error enabling/disabling employee: " + ex.Message);
            }
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
                throw new FaultException("Error getting employees: " + ex.Message);
            }
            return employees;
        }

        public async Task<Employee> GetEmployeeByIDAsync(int employeeID)
        {
            var employee = new Employee();
            try
            {
                string url = $"{_baseUrl}/GetEmployeeById?id={employeeID}";
                string json = await _client.GetStringAsync(url);
                employee = JsonConvert.DeserializeObject<Employee>(json);
            }
            catch (Exception ex)
            {
                throw new FaultException("Error getting employee by ID: " + ex.Message);
            }
            return employee;
        }
    }
}
