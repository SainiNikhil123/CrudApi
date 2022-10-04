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
    [Route("api/Role")]
    [ApiController]
    [Authorize(Roles = SD.Role_Admin)]
    public class RoleController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public RoleController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Roles()
        {
            var roleList = _context.Roles.ToList();
            var adminRole = roleList.FirstOrDefault(r => r.Name == SD.Role_Admin);
            roleList.Remove(adminRole);
            return Ok(roleList);
        }
        [HttpPost]
        public IActionResult AddRole([FromBody] Roles roles)
        {
            if (roles != null && ModelState.IsValid)
            {
                _context.Roles.Add(roles);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
        [HttpPut]
        public IActionResult UpdateRole([FromBody] Roles roles)
        {
            if (roles != null && ModelState.IsValid)
            {
                _context.Roles.Update(roles);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
    }
}
