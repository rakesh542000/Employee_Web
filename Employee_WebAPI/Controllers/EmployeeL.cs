using EmpAPI.Models; // Importing the necessary namespaces
using EmpAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System; // Importing the System namespace for DateTime

namespace EmpAPI.Controllers // Defining the namespace
{
    [Route("[controller]")] // Attribute routing to define the route for the controller
    [ApiController] // Indicates that this controller is an API controller
    public class EmployeeController : ControllerBase // Defining the controller class
    {
        private readonly IEmployeeBusiness _employeeBusiness; // Declaring a private field to hold the business logic

        public EmployeeController(IEmployeeBusiness employeeBusiness) // Constructor injection
        {
            _employeeBusiness = employeeBusiness; // Assigning the injected business logic instance to the private field
        }

        [HttpGet] // Attribute specifying that this method handles HTTP GET requests
        public ActionResult GetAllEmployees() // Method to get all employees
        {
            return _employeeBusiness.GetAll(); // Returning the result from the business logic
        }

        [HttpGet("GetSecondHighest")] // Attribute specifying a route for getting the employee with the second-highest salary
        public ActionResult GetSecondHighestSalaryEmployee() // Method to get the employee with the second-highest salary
        {
            return _employeeBusiness.GetSecondHighest(); // Returning the result from the business logic
        }

        [HttpGet("{id}")] // Attribute specifying a route with a parameter for getting an employee by ID
        public ActionResult GetEmployeeById(int id) // Method to get an employee by ID
        {
            var employee = _employeeBusiness.GetById(id); // Getting the employee by ID using the business logic
            if (employee == null) // Checking if the employee is not found
            {
                return NotFound(); // Returning a 404 Not Found response
            }
            return employee; // Returning the employee
        }

        [HttpGet("GetByDate/{dt}")] // Attribute specifying a route with a parameter for getting an employee by joining date
        public ActionResult GetEmployeeByJoiningDate(DateTime dt) // Method to get an employee by joining date
        {
            var employee = _employeeBusiness.GetByDate(dt); // Getting the employee by joining date using the business logic
            if (employee == null) // Checking if the employee is not found
            {
                return NotFound(); // Returning a 404 Not Found response
            }
            return employee; // Returning the employee
        }

        [HttpPost] // Attribute specifying that this method handles HTTP POST requests
        public IActionResult AddEmployee(Employee employee) // Method to add a new employee
        {
            _employeeBusiness.Add(employee); // Adding the employee using the business logic
            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee); // Returning a 201 Created response with the newly created employee
        }

        [HttpPut("{id}")] // Attribute specifying a route with a parameter for updating an employee
        public IActionResult UpdateEmployee(int id, Employee employee) // Method to update an existing employee
        {
            if (id != employee.Id) // Checking if the provided ID matches the employee's ID
            {
                return BadRequest(); // Returning a 400 Bad Request response
            }

            var existingEmployee = _employeeBusiness.GetById(id); // Getting the existing employee by ID using the business logic
            if (existingEmployee == null) // Checking if the existing employee is not found
            {
                return NotFound(); // Returning a 404 Not Found response
            }

            _employeeBusiness.Update(employee); // Updating the employee using the business logic
            return NoContent(); // Returning a 204 No Content response
        }

        [HttpDelete("{id}")] // Attribute specifying a route with a parameter for deleting an employee
        public IActionResult DeleteEmployee(int id) // Method to delete an employee
        {
            var employee = _employeeBusiness.GetById(id); // Getting the employee by ID using the business logic
            if (employee == null) // Checking if the employee is not found
            {
                return NotFound(); // Returning a 404 Not Found response
            }

            _employeeBusiness.Delete(id); // Deleting the employee using the business logic
            return NoContent(); // Returning a 204 No Content response
        }
    }
}
