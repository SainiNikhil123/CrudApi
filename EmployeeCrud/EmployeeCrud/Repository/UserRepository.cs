using EmployeeCrud.Data;
using EmployeeCrud.Models;
using EmployeeCrud.Models.DTOs;
using EmployeeCrud.Repository.iRepository;
using Microsoft.EntityFrameworkCore;
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

        public Token Authenticate(string UserName, string Password)
        {
            var UserInDb = (from u in _context.applicationUsers
                            join ur in _context.userRoleTables
                            on u.UserId equals ur.UserId
                            join r in _context.Roles
                            on ur.RoleId equals r.Id
                            where u.UserName == UserName && u.Password == Password
                            select new 
                            {
                                id = u.UserId,
                                UserName = u.UserName,
                                Role = r.Name
                            }).FirstOrDefault();

            var userClaims = _context.UserClaimTable.Where(x => x.UserId == UserInDb.id).Select(x => x.claims.name).Select(x=>new Claim(x,"true")).ToList(); 


            if (UserInDb == null)
                return null;

            //JWT

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_iconfiguration["JWT:Key"]);
            var tokenDescritor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, UserInDb.UserName),
                    new Claim(ClaimTypes.Role, UserInDb.Role),
                }.Union(userClaims)),

                Issuer = _iconfiguration["jwt:Issuer"],
                Audience = _iconfiguration["jwt:Audience"],
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescritor);
            Token ValidToken = new Token()
            {
                Tokens = tokenHandler.WriteToken(token),
                //Claims = userClaims
            };

            return ValidToken;
        }

        public ICollection<UserDto> GetUser()
        {
            var UserList = (from u in _context.applicationUsers
                            join r in _context.userRoleTables
                            on u.UserId equals r.UserId
                            select new UserDto()
                            {
                                id = u.UserId,
                                Name = u.Name,
                                UserName = u.UserName,
                                Email = u.Email,
                                RoleId = r.RoleId,
                                Role = r.Roles.Name
                            }).ToList();

            return (UserList);
        }

        public bool IsUniqueUser(string UserName)
        {
            var User = _context.applicationUsers.FirstOrDefault(u => u.UserName == UserName);
            if (User == null) return true; else return false;
        }

        public bool IsUserExist(int id)
        {
            var User = _context.applicationUsers.FirstOrDefault(u => u.UserId == id);
            if (User != null) return true; else return false;
        }

        public ApplicationUser Register(UserDto user)
        {
           // if (user == null) return null;
            ApplicationUser userdata = new ApplicationUser()
            {
                Name = user.Name,
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password
            };
            _context.applicationUsers.Add(userdata);
            _context.SaveChanges();

            UserRoleTable role = new UserRoleTable()
            {
                UserId = userdata.UserId,
                RoleId = user.RoleId
            };
            _context.userRoleTables.Add(role);
            _context.SaveChanges();

            return userdata;
        }

        //public ICollection<UserClaimTable> AddClaim(ClaimsDto claims)
        //{
        //    if (claims == null) return null;

        //    List<UserClaimTable> userclaim = new List<UserClaimTable>();
        //    foreach (var claim in claims.ClaimIds)
        //    {
        //        UserClaimTable clm = new UserClaimTable()
        //        {
        //            UserId = claims.UserId,
        //            ClaimId = claim
        //        };
        //        userclaim.Add(clm);
        //    }
        //    _context.UserClaimTable.AddRange(userclaim);
        //    _context.SaveChanges();
        //    return userclaim;


        //}

        public ClaimsDto GetClaimsById(int id)
        {
            bool isUserExist = IsUserExist(id);
            if (!isUserExist) return null;

            List<int> Claims = _context.UserClaimTable.Where(x => x.UserId == id).Select(x => x.ClaimId).ToList();

            ClaimsDto userClaim = new ClaimsDto()
            {
                UserId = id,
                ClaimIds = Claims
            };

            return userClaim;

        }

        public bool EditClaim(ClaimsDto claim)
        {
            bool isUserExist = IsUserExist(claim.UserId);
            if (!isUserExist) return false;

            List<int> addClaim = claim.ClaimIds.Except(claim.oldClaimId).ToList();

            List<int> removeClaim = claim.oldClaimId.Except(claim.ClaimIds).ToList();

            if (removeClaim.Count != 0)
            {
                List<UserClaimTable> usrclmTbls = new List<UserClaimTable>();
                foreach (var item in removeClaim)
                {
                    var empdep = _context.UserClaimTable.FirstOrDefault(x => x.UserId == claim.UserId && x.ClaimId == item);
                    usrclmTbls.Add(empdep);
                }
                _context.UserClaimTable.RemoveRange(usrclmTbls);
                _context.SaveChanges();
            }
            if (addClaim.Count != 0)
            {
                List<UserClaimTable> usrclmTbls = new List<UserClaimTable>();
                foreach (var d in addClaim)
                {
                    UserClaimTable edt = new UserClaimTable()
                    {
                        UserId = claim.UserId,
                        ClaimId = d
                    };
                    usrclmTbls.Add(edt);

                }
                _context.UserClaimTable.AddRange(usrclmTbls);
                _context.SaveChanges();
            }
            return true;
        }
    }
}
