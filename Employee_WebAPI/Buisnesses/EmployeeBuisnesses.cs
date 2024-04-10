using EmpAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmpAPI.Services
{
    public static class EmployeeBusiness
    {
        private static List<Employee> _employees;
        private static int _nextId = 7;

        static EmployeeBusiness()
        {
            _employees = new List<Employee>
            {
                new Employee {Id=1, Name="Rakesh", Position="SDE-2", Salary=35000, JoinDate = new DateTime(2024, 01, 26)},
                new Employee {Id=2, Name="Rajesh", Position="SDE", Salary=45000, JoinDate = new DateTime(2024, 07, 25)},
                new Employee {Id=3, Name="Ashutosh", Position="SSD", Salary=60000, JoinDate = new DateTime(2023, 03, 25)},
                new Employee {Id=4, Name="Anish", Position="SSD", Salary=70000, JoinDate = new DateTime(2022, 04, 25)},
                new Employee {Id=5, Name="Subham", Position="HR", Salary=55000, JoinDate = new DateTime(2021, 05, 29)},
                new Employee {Id=6, Name="Rajat", Position="Manager", Salary=65000, JoinDate = new DateTime(2024, 08, 15)}
            };
        }

        // GET REQUEST METHODS

        public static List<Employee> GetAll() => _employees;

        public static Employee? GetById(int id) => _employees.FirstOrDefault(e => e.Id == id);

        public static Employee? GetByDate(DateTime dt) => _employees.FirstOrDefault(emp => emp.JoinDate == dt);

        public static Employee GetSecondHighest()
        {
            var sortedEmployees = _employees.OrderByDescending(e => e.Salary).ToList();
            return sortedEmployees.Skip(1).FirstOrDefault();
        }

        // POST REQUEST METHOD

        public static void Add(Employee e)
        {
            e.Id = _nextId++;
            _employees.Add(e);
        }

        // PUT (UPDATE) REQUEST METHOD

        public static void Update(Employee e)
        {
            var existingEmployee = _employees.FirstOrDefault(emp => emp.Id == e.Id);
            if (existingEmployee != null)
            {
                existingEmployee.Name = e.Name;
                existingEmployee.Position = e.Position;
                existingEmployee.Salary = e.Salary;
                existingEmployee.JoinDate = e.JoinDate;
            }
        }

        // DELETE REQUEST METHOD

        public static void Delete(int id)
        {
            var employeeToRemove = _employees.FirstOrDefault(e => e.Id == id);
            if (employeeToRemove != null)
            {
                _employees.Remove(employeeToRemove);
            }
        }
    }
}
