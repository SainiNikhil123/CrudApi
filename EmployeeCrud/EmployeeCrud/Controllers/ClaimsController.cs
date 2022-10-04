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
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ClaimsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetClaims()
        {
            return Ok(_context.ClaimTable.ToList());
        }

        [HttpPost]
        public IActionResult AddClaim([FromBody]ClaimsTable claims)
        {
            if(claims != null && ModelState.IsValid)
            {
                _context.ClaimTable.Add(claims);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
        [HttpPut]
        public IActionResult UpdateDepartment([FromBody] ClaimsTable claims)
        {
            if (claims != null && ModelState.IsValid)
            {
                _context.ClaimTable.Update(claims);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
    }
}
