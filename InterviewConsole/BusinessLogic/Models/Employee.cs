using System.Collections.Generic;

namespace InterviewConsole.BusinessLogic.Models
{
    internal class Employee
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int? ManagerID { get; set; }

        public bool IsEnabled { get; set; }

        public List<Employee> Employees { get; set; }
    }
}
