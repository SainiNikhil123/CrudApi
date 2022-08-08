using EmployeeCrud.Data;
using EmployeeCrud.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeCrud.Controllers
{
    [Route("api/Employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            return Ok(_context.employees.ToList());
        }
        [HttpGet("api/Employee/{id}")]
        public IActionResult GetEmployee(int id)
        {
            var employeeInDb = _context.employees.Find(id);
            if (employeeInDb == null) return BadRequest();
            return Ok(employeeInDb);
        }
        [HttpPost]
        public IActionResult SaveEmployee([FromBody] Employee employee)
        {
            if (employee != null && ModelState.IsValid)
            {
                _context.employees.Add(employee);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
        [HttpPut]
        public IActionResult UpdateStudent([FromBody] Employee employee)
        {
            if (employee != null && ModelState.IsValid)
            {
                _context.employees.Update(employee);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
        [HttpDelete("{id:int}")]
        public IActionResult DeleteStudent(int id)
        {
            var employeeInDb = _context.employees.Find(id);
            if (employeeInDb == null) return NotFound();
            _context.employees.Remove(employeeInDb);
            _context.SaveChanges();
            return Ok();
        }
    }
}
