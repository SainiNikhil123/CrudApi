using EmployeeCrud.Models;
using EmployeeCrud.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeCrud.Repository.iRepository
{
   public interface iUserRepository
    {
        bool IsUniqueUser(string UserName);
        Token Authenticate(string UserName, string Password);
        ApplicationUser Register(UserDto user);
        ICollection<UserDto> GetUser();
        ClaimsDto GetClaimsById(int id);
        //ICollection<UserClaimTable> AddClaim(ClaimsDto claims);
        bool EditClaim(ClaimsDto claim);
    }
}
