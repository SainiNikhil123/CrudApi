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
    [Route("api/designation")]
    [ApiController]
    [Authorize]
    public class DesignationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public DesignationController(ApplicationDbContext context)
        {
            _context = context;
        }
     
        [HttpGet]
        public IActionResult Designation()
        {
            return Ok(_context.designations.ToList());
        }
        [HttpPost]
        public IActionResult AddDesignation([FromBody] Designation designation)
        {
            if (designation != null && ModelState.IsValid)
            {
                _context.designations.Add(designation);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
        [HttpPut]
        public IActionResult UpdateDesignation([FromBody] Designation designation)
        {
            if (designation != null && ModelState.IsValid)
            {
                _context.designations.Update(designation);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
    }
}
