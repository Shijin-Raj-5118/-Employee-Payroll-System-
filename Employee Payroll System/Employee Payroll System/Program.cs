using System;
using System.Collections.Generic;
using System.Linq;
namespace PayrollSystem
{
    class Program
    {
        public class BaseEmployee
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Role { get; set; }
            public double BasicPay { get; set; }
            public double Allowances { get; set; }
            public virtual double CalculateSalary()
            {
                return BasicPay + Allowances;
            }
            public virtual void DisplayEmployeeDetails()
            {
                Console.WriteLine($"ID: EM_{ID}, Name: {Name}, Role: {Role}, Basic Pay: { BasicPay: C}, Allowances: { Allowances: C}");
            }
        }
        public class Manager : BaseEmployee
        {
            public override double CalculateSalary()
            {
                return BasicPay + Allowances - 500; // Manager deduction
            }
        }
        public class Developer : BaseEmployee
        {
            public double ProjectBonus { get; set; }
            public override double CalculateSalary()
            {
                return BasicPay + Allowances + ProjectBonus;
            }
            public override void DisplayEmployeeDetails()
            {
                base.DisplayEmployeeDetails();
                Console.WriteLine($"Project Bonus: {ProjectBonus:C}");
            }
        }
        public class Intern : BaseEmployee
        {
            public override double CalculateSalary()
            {
                return BasicPay + Allowances - 100; // Intern deduction
            }
        }
        static void Main(string[] args)
        {
            List<BaseEmployee> employees = new List<BaseEmployee>();
            while (true)
            {
                Console.WriteLine("\nPayroll System Menu:");
                Console.WriteLine("1. Add Employee");
                Console.WriteLine("2. Display All Employees");
                Console.WriteLine("3. Calculate Individual Salary");
                Console.WriteLine("4. Delete Employee");
                Console.WriteLine("5. Edit Employee");
                Console.WriteLine("6. Exit");
                Console.Write("Enter your choice: ");
                int choice = GetValidIntInput();
                switch (choice)
                {
                    case 1:
                        AddEmployee(employees);
                        break;
                    case 2:
                        DisplayEmployees(employees);
                        break;
                    case 3:
                        CalculateSalary(employees);
                        break;
                    case 4:
                        DeleteEmployee(employees);
                        break;
                    case 5:
                        EditEmployee(employees);
                        break;
                    case 6:
                        return;
                    default:
                        Console.WriteLine("Invalid choice! Please enter a valid option.");
                        break;
                }
            }
        }
        static void AddEmployee(List<BaseEmployee> employees)
        {
            Console.Write("Enter ID:EM_");
            int id = GetValidIntInput();
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Role (Manager/Developer/Intern): ");
            string role = Console.ReadLine();
            Console.Write("Enter Basic Pay: ");
            double basicPay = GetValidDoubleInput();
            Console.Write("Enter Allowances: ");
            double allowances = GetValidDoubleInput();
            BaseEmployee employee;
            if (role.Equals("Manager", StringComparison.OrdinalIgnoreCase))
            {
                employee = new Manager();
            }
            else if (role.Equals("Intern", StringComparison.OrdinalIgnoreCase))
            {
                employee = new Intern();
            }
            else if (role.Equals("Developer", StringComparison.OrdinalIgnoreCase))
            {
                Console.Write("Enter Project Bonus: ");
                double projectBonus = GetValidDoubleInput();
                employee = new Developer { ProjectBonus = projectBonus };
            }
            else
            {
                Console.WriteLine("Invalid Role. Employee not added.");
                return;
            }
            employee.ID = id;
            employee.Name = name;
            employee.Role = role;
            employee.BasicPay = basicPay;
            employee.Allowances = allowances;
            employees.Add(employee);
            Console.WriteLine("Employee added successfully.");
        }
        static void DisplayEmployees(List<BaseEmployee> employees)
        {
            if (employees.Count == 0)
            {
                Console.WriteLine("No data to display.");
            }
            else
            {
                foreach (var emp in employees)
                {
                    emp.DisplayEmployeeDetails();
                    Console.WriteLine($"Salary: {emp.CalculateSalary():C}");
                }
            }
        }
        static void DeleteEmployee(List<BaseEmployee> employees)
        {
            if (employees.Count == 0)
            {
                Console.WriteLine("No data to delete.");
            }
            else
            {
                Console.Write("Enter Employee ID to delete:EM_");
                int id = GetValidIntInput();
                var employee = employees.FirstOrDefault(e => e.ID == id);
                if (employee != null)
                {
                    employees.Remove(employee);
                    Console.WriteLine($"Employee with ID EM_{id} has been deleted.");
                }
                else
                {
                    Console.WriteLine($"Employee with ID EM_{id} not found.");
                }
            }
        }
        static void EditEmployee(List<BaseEmployee> employees)
        {
            if (employees.Count == 0)
            {
                Console.WriteLine("No data to edit.");
            }
            else
            {
                Console.Write("Enter Employee ID to edit:EM_ ");
                int id = GetValidIntInput();
                var employee = employees.FirstOrDefault(e => e.ID == id);
                if (employee != null)
                {
                    Console.WriteLine($"Editing Employee: {employee.Name} (ID: EM_{id})");
                    Console.Write("Enter new Name (leave blank to keep current): ");
                    string name = Console.ReadLine();
                    if (!string.IsNullOrEmpty(name)) employee.Name = name;
                    Console.Write("Enter new Role (leave blank to keep current): ");
                    string role = Console.ReadLine();
                    if (!string.IsNullOrEmpty(role)) employee.Role = role;
                    Console.Write("Enter new Basic Pay (leave blank to keep current): ");
                    string basicPayInput = Console.ReadLine();
                    if (!string.IsNullOrEmpty(basicPayInput)) employee.BasicPay =
                   double.Parse(basicPayInput);
                    Console.Write("Enter new Allowances (leave blank to keep current): ");
                    string allowancesInput = Console.ReadLine();
                    if (!string.IsNullOrEmpty(allowancesInput)) employee.Allowances =
                   double.Parse(allowancesInput);
                    if (employee is Developer dev)
                    {
                        Console.Write("Enter new Project Bonus (leave blank to keep current): ");
                        string projectBonusInput = Console.ReadLine();
                        if (!string.IsNullOrEmpty(projectBonusInput)) dev.ProjectBonus =
                       double.Parse(projectBonusInput);
                    }
                    Console.WriteLine($"Employee with ID EM_{id} has been updated.");
                }
                else
                {
                    Console.WriteLine($"Employee with ID EM_{id} not found.");
                }
            }
        }
        static void CalculateSalary(List<BaseEmployee> employees)
        {
            if (employees.Count == 0)
            {
                Console.WriteLine("No data to calculate salary.");
            }
            else
            {
                Console.Write("Enter Employee ID to calculate salary (e.g., EM_123): ");
                int id = GetValidIntInput();
                var emp = employees.Find(e => e.ID == id);
                if (emp != null)
                {
                    Console.WriteLine($"Salary for {emp.Name}: {emp.CalculateSalary():C}");
                }
                else
                {
                    Console.WriteLine("Employee not found.");
                }
            }
        }
        // Helper method to get valid integer input
        static int GetValidIntInput()
        {
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int result))
                    return result;
                else
                    Console.WriteLine("Invalid input. Please enter a valid integer.");
            }
        }
        // Helper method to get valid double input
        static double GetValidDoubleInput()
        {
            while (true)
            {
                if (double.TryParse(Console.ReadLine(), out double result))
                    return result;
                else
                    Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }
    }
}