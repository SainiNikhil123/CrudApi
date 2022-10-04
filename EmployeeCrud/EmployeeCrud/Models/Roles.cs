﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeCrud.Models
{
    public class Roles
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
       
    }
}
