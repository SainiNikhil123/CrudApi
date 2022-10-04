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
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        public IActionResult GetUser()
        {
            var UserList = _UserRepository.GetUser();
            var AdminUser = UserList.FirstOrDefault(u => u.Role == SD.Role_Admin);
            UserList.Remove(AdminUser);
            return Ok(UserList);
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] UserDto User)
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

        [HttpGet("Claims")]
        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult GetClaimById(int id)
        {
            if (id != 0)
            {
                var claims = _UserRepository.GetClaimsById(id);
                if (claims == null) return BadRequest();
                return Ok(claims);
            }
            return BadRequest();
        }

        //[HttpPost("AddClaim")]
        //[Authorize(Roles = SD.Role_Admin)]
        //public IActionResult AddClaims([FromBody] ClaimsDto claims)
        //{
        //    var claim = _UserRepository.AddClaim(claims);
        //    if (claim == null) return BadRequest();

        //    return Ok(claim);
        //}

        [HttpPut("Claim")]
        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult UpdateClaim([FromBody]ClaimsDto claims)
        {
            if(claims != null && ModelState.IsValid)
            {
                bool clm = _UserRepository.EditClaim(claims);
                if (!clm) return BadRequest();
                return Ok();
            }
            return BadRequest();
        }
    }
}
