using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeCrud.Models.DTOs
{
    public class UserDto
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public string Role { get; set; }

    }
}
