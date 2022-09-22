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
        ApplicationUser Authenticate(string UserName, string Password);
        ApplicationUser Register(ApplicationUser user);
        ICollection<UserDto> GetUser();
    }
}
