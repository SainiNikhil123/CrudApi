using EmployeeCrud.Data;
using EmployeeCrud.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeCrud.Controllers
{
    [Route("api/department")]
    [ApiController]
    [Authorize]
    public class DepartmentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public DepartmentController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Departments()
        {
            return Ok(_context.departments.ToList());
        }
        [HttpPost]
        public IActionResult AddDepartment([FromBody] Department department)
        {
            if (department != null && ModelState.IsValid)
            {
                _context.departments.Add(department);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
        [HttpPut]
        public IActionResult UpdateDepartment([FromBody] Department department)
        {
            if (department != null && ModelState.IsValid)
            {
                _context.departments.Update(department);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
    }
}
