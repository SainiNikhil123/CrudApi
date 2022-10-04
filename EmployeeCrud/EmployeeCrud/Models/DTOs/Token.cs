using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeCrud.Models.DTOs
{
    public class Token
    {
        public String Tokens { get; set; }
        public List<string> Claims { get; set; }
    }
}
