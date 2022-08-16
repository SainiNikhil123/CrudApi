using EmployeeCrud.Data;
using EmployeeCrud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        {
            var employeeList = from e in _context.employees
                               join edp in _context.empDepTbls
                               on e.Id equals edp.EmployeeId
                               select new
                               {
                                   Id=e.Id,
                                   Name = e.EmpName,
                                   Address = e.Address,
                                   Number = e.Number,
                                   Salary = e.Salary,
                                   Designation = e.Designation.DesName,
                                   Department = edp.Department.DepName
                               };
            return Ok(employeeList);
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployee(int id)
        {
            var employeeFromDb = _context.employees.Find(id);
            if (employeeFromDb == null) return BadRequest(error:"Employee Id Is Invalid");

           var employee = from e in _context.employees
                          join edp in _context.empDepTbls
                          on e.Id equals edp.EmployeeId
                          select new 
                          {
                            id=e.Id,
                            Name = e.EmpName,
                            Address = e.Address,
                            Number = e.Number,
                            Salary = e.Salary,
                            Designation = e.Designation.DesName,
                            Department = edp.Department.DepName
                           };

            return Ok(employee.Where(x=>x.id==id));
        }

        [HttpPost]
        public IActionResult SaveEmployee([FromBody]Employee employee)
        {
            if(employee != null && ModelState.IsValid)
            {
                Employee emp = new Employee()
                {
                    EmpName = employee.EmpName,
                    Address = employee.Address,
                    Number = employee.Number,
                    Salary = employee.Salary,
                    DesignationId = employee.DesignationId
                };
                _context.employees.Add(emp);
                _context.SaveChanges();
                EmpDepTbl edt = new EmpDepTbl()
                {
                    EmployeeId = emp.Id,
                    DepartmentId = employee.Employees.DepartmentId
                };
                _context.empDepTbls.Add(edt);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
        [HttpDelete("{id:int}")]
        public IActionResult DeleteEmployee(int id)
        {
            var employee = _context.employees.Find(id);
            if (employee == null) return BadRequest(error: "Error While Deleting");
            _context.employees.Remove(employee);
            _context.SaveChanges();
            return Ok();
        }
        [HttpPut]
        public IActionResult UpdateEmployee([FromBody]Employee  employee)
        {
            if (ModelState.IsValid)
            {
                Employee emp = new Employee()
                {
                    EmpName = employee.EmpName,
                    Address = employee.Address,
                    Number = employee.Number,
                    Salary = employee.Salary,
                    DesignationId = employee.DesignationId
                };
                _context.employees.Update(emp);
                _context.SaveChanges();
                EmpDepTbl edt = new EmpDepTbl()
                {
                    EmployeeId = emp.Id,
                    DepartmentId = employee.Employees.DepartmentId
                };
                _context.empDepTbls.Update(edt);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest(error:"Error While Updating");


        }
    }
}
