using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeCrud.Models.DTOs
{
    public class EmployeeListDto
    {
        public int Id { get; set; }
        public string EmpName { get; set; }
        public string Address { get; set; }
        public String Number { get; set; }
        public double Salary { get; set; }
        public int DesignationId { get; set; }
        public string Designation { get; set; }
        public int DepartmentId { get; set; }
        public string Department { get; set; }
        public List<int> DepartmentIds { get; set; }
        public List<int> Departmenteditid { get; set; }
    }
}
