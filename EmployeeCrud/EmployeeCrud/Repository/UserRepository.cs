using EmployeeCrud.Data;
using EmployeeCrud.Models;
using EmployeeCrud.Models.DTOs;
using EmployeeCrud.Repository.iRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCrud.Repository
{
    public class UserRepository : iUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _iconfiguration;
        public UserRepository(ApplicationDbContext context, IConfiguration iconfiguration)
        {
            _context = context;
            _iconfiguration = iconfiguration;
        }

        public ApplicationUser Authenticate(string UserName, string Password)
        {
            var UserInDb = _context.applicationUsers.FirstOrDefault(u => u.UserName == UserName && u.Password == Password);
            if (UserInDb == null)
                return null;
            //JWT
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_iconfiguration["JWT:Key"]);
            var tokenDescritor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, UserInDb.UserName.ToString())
                }),
                Issuer = _iconfiguration["jwt:Issuer"],
                Audience = _iconfiguration["jwt:Audience"],
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescritor);
            UserInDb.Token = tokenHandler.WriteToken(token);
            UserInDb.Password = "";
            
            return UserInDb;
        }

        public ICollection<UserDto> GetUser()
        {
            var UserList =(from u in _context.applicationUsers
                           join r in _context.Roles
                           on u.RoleId equals r.Id
                           select new UserDto()
                           {
                               id = u.UserId,
                               Name = u.Name,
                               UserName = u.UserName,
                               Email = u.Email,
                               RoleId = u.RoleId,
                               Role = r.Name
                           }).ToList();

            return (UserList);
        }

        public bool IsUniqueUser(string UserName)
        {
            var User = _context.applicationUsers.FirstOrDefault(u => u.UserName == UserName);
            if (User == null) return true; else return false;
        }

        public ApplicationUser Register(ApplicationUser user)
        {
            if (user == null) return null;            
            _context.applicationUsers.Add(user);
            _context.SaveChanges();
            return user;
        }
    }
}
