using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models;
using EmployeeAdminPortal.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAdminPortal.Controllers
{
    //localhost:7017/api/employees/
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext applicationDbContext;
        public EmployeesController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var allEmployees = applicationDbContext.Employees.ToList();
            return Ok(allEmployees);
        }

        [HttpPost]
        public IActionResult AddEmployee(AddEmployeeDTO employee)
        {
            var emp = new Employee()
            {
                Name = employee.Name,
                Email = employee.Email,
                Phone = employee.Phone,
                Salary = employee.Salary,
            };
            applicationDbContext.Employees.Add(emp);
            applicationDbContext.SaveChanges();
            return Ok(emp);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var emp = applicationDbContext.Employees.Find(id);
            return Ok(emp);
        }

        [HttpDelete]
        public IActionResult DeleteById(Guid id)
        {
            var emp = applicationDbContext.Employees.Find(id);
            if (emp == null)
            {
                throw new Exception("Employee not found");
            }
            applicationDbContext.Employees.Remove(emp);
            applicationDbContext.SaveChanges();
            return Ok("Employee successfully deleted from database.");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(Guid id, UpdateEmployeeDTO employee)
        {
            var emp = applicationDbContext.Employees.Find(id);

            if (emp == null)
            {
                throw new Exception("Employee not found");
            }

            emp.Name = employee.Name;
            emp.Email = employee.Email;
            emp.Phone = employee.Phone;
            emp.Salary = employee.Salary;

            applicationDbContext.SaveChanges();
            return Ok(employee);
        }
    }
}
