using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeCrud.Models
{
    public class UserClaimTable
    {
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }
        public int ClaimId { get; set; }
        [ForeignKey("ClaimId")]
        public ClaimsTable claims { get; set; }
    }
}
