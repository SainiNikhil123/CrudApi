using EmployeeCrud.Data;
using EmployeeCrud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeCrud.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetEmployees()
        {
            var employeeList = from e in _context.employees
                               join edp in _context.empDepTbls
                               on e.Id equals edp.EmployeeId
                               select new
                               {
                                   Name = e.EmpName,
                                   Address = e.Address,
                                   Number = e.Number,
                                   Salary = e.Salary,
                                   Designation = e.Designation.DesName,
                                   Department = edp.Department.DepName
                               };
            return Ok(employeeList);
        }
        [HttpPost]
        public IActionResult SaveEmployee([FromBody]Employee employee)
        {
            if (employee == null && !ModelState.IsValid)
            {
                return BadRequest();
            }
            //using(Employee emp = Employee())
            //{

            //}
            _context.employees.Add(employee);            
             _context.SaveChanges();
            return Ok();
        }
    }
}
