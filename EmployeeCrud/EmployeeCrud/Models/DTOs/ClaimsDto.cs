using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeCrud.Models.DTOs
{
    public class ClaimsDto
    {
        public int UserId { get; set; }
        public List<int> ClaimIds { get; set; }
        public List<int> oldClaimId { get; set; }
        public List<string> Claims { get; set; }
    }
}
