using EmployeeCrud.Models;
using EmployeeCrud.Models.DTOs;
using EmployeeCrud.Repository.iRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeCrud.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly iUserRepository _UserRepository;
        public UserController(iUserRepository UserRepository)
        {
            _UserRepository = UserRepository;
        }
        [HttpGet]
        [Authorize]
        public IActionResult GetUser()
        {
            return Ok( _UserRepository.GetUser());
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] ApplicationUser User)
        {
            if (User == null && !ModelState.IsValid) return BadRequest();

            var isUnique = _UserRepository.IsUniqueUser(User.UserName);
            if (!isUnique) return BadRequest();
            
            _UserRepository.Register(User);
            return Ok();
        }
        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] Login login)
        {
            var user = _UserRepository.Authenticate(login.UserName, login.Password);
            if (user == null) return BadRequest("Wromg User / PWD");
            return Ok(user);
        }
    }
}
