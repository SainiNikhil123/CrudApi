using EmployeeCrud.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeCrud.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetEmployees()
        { //EmpDepTbl edp = new EmpDepTbl();
            var employeeList = from e in _context.employees
                               join des in _context.designations
                               on e.DesignationId equals des.Id
                               join edp in _context.empDepTbls
                               on e.Id equals edp.EmployeeId
                               select new
                               {
                                   Name = e.EmpName,
                                   Address = e.Address,
                                   Number = e.Number,
                                   Salary = e.Salary,
                                   Designation = e.Designation.DesName,
                                   Department = edp.Department.DepName
                               };
          return Ok(employeeList);
        }
        //[HttpPost]
        //public IActionResult SaveEmployee()
        //{
        //    var employee=  
        //}
    }
}
