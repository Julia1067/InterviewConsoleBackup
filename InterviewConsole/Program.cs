using InterviewConsole.BusinessLogic.Services.Abstraction;
using InterviewConsole.BusinessLogic.Services.Implementation;
using System;
using System.Threading.Tasks;

namespace InterviewConsole
{
    class Program
    {
        private readonly static IEmployeeAPIService _apiService = new EmployeeAPIService();

        static async Task Main(string[] args)
        {
            Console.Title = "Employee Management System";

            while (true)
            {
                ShowMenu();
                string choice = Console.ReadLine();

                switch(choice)
                {
                    case "1": await GetEmployees(); break;
                    case "2": await GetEmployeeById(); break;
                    case "3": await EnableEmployee(); break;
                    case "4": await DisableEmployee(); break;
                    case "5": return;
                    default: Console.WriteLine("Invalid option.Please try again"); break;
                }

                Console.WriteLine("\nPress any key to return to the menu");
                Console.ReadKey();
                Console.Clear();
            }
        }

        static void ShowMenu()
        {
            Console.WriteLine("     [1] View All Employees");
            Console.WriteLine("     [2] Find Employee By ID");
            Console.WriteLine("     [3] Enable Employee");
            Console.WriteLine("     [4] Disable Employee");
            Console.WriteLine("     [5] Exit\n");

            Console.Write("Select an option:  ");
        }

        static async Task GetEmployees()
        {
            Console.WriteLine("Employees List");
            var employees = await _apiService.GetEmployeesAsync();
            Console.WriteLine($"\n  {"ID",-5} {"Name",-20} {"ManagerID",-12} {"Status",-10}");
            foreach (var employee in employees)
            {
                string status = employee.IsEnabled ? "Active" : "Inactive";
                Console.WriteLine($"  {employee.ID,-5} {employee.Name,-20} {employee.ManagerID,-12} {status,-10}");
            }
        }

        static async Task GetEmployeeById()
        {
            Console.Write("Select employee by ID:  ");
            var choice = Console.ReadLine();
            if(!int.TryParse(choice, out int id))
            {
                Console.WriteLine("Invalid ID");
                return;
            }
            var employee = await _apiService.GetEmployeeByIDAsync(id);

            if(employee == null)
            {
                Console.WriteLine("No user with current ID was found");
                return;
            }

            Console.WriteLine("ID: " + employee.ID);
            Console.WriteLine("Name: " + employee.Name);
            Console.WriteLine("ManagerID: " + employee.ManagerID ?? string.Empty);
            Console.WriteLine("Enabled: " + employee.IsEnabled);

            if(employee.Employees.Count != 0)
            {
                Console.WriteLine("\nSubordinates");
                Console.WriteLine($"\n  {"ID",-5} {"Name",-20} {"ManagerID",-12} {"Status",-10}");
                foreach (var emp in employee.Employees)
                {
                    string status = emp.IsEnabled ? "Active" : "Inactive";
                    Console.WriteLine($"  {emp.ID,-5} {emp.Name,-20} {emp.ManagerID,-12} {status,-10}");
                }
            }
        }

        static async Task EnableEmployee()
        {
            Console.Write("Select employee by ID:  ");
            var choice = Console.ReadLine();
            if (!int.TryParse(choice, out int id))
            {
                Console.WriteLine("Invalid ID");
            }
            await _apiService.EnableEmployeeAsync(id, 1);
        }
        
        static async Task DisableEmployee()
        {
            Console.Write("Select employee by ID:  ");
            var choice = Console.ReadLine();
            if (!int.TryParse(choice, out int id))
            {
                Console.WriteLine("Invalid ID");
            }
            await _apiService.EnableEmployeeAsync(id, 0);
        }
    }
}
