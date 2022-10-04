using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeCrud.Models
{
    public class UserRoleTable
    {
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }
        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public Roles Roles { get; set; }
        

       
    }
}
