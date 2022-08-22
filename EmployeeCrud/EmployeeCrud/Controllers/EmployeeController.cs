using EmployeeCrud.Data;
using EmployeeCrud.Models;
using EmployeeCrud.Models.DTOs;
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
                               select new EmployeeListDto()
                               {
                                   Id = e.Id,
                                   EmpName = e.EmpName,
                                   Address = e.Address,
                                   Number = e.Number,
                                   Salary = e.Salary,
                                   DesignationId = e.DesignationId,
                                   Designation = e.Designation.DesName,
                                   DepartmentId = edp.DepartmentId,
                                   Department = edp.Department.DepName
                               };
            return Ok(employeeList);
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployee(int id)
        {
            var employeeFromDb = _context.employees.Find(id);
            if (employeeFromDb == null) return BadRequest(error: "Employee Id Is Invalid");

            var employee = from e in _context.employees
                           join edp in _context.empDepTbls
                           on e.Id equals edp.EmployeeId
                           where e.Id == id
                           select new EmployeeListDto()
                           {
                               Id = e.Id,
                               EmpName = e.EmpName,
                               Address = e.Address,
                               Number = e.Number,
                               Salary = e.Salary,
                               DesignationId = e.DesignationId,
                               Designation = e.Designation.DesName,
                               DepartmentId = edp.DepartmentId,
                               Department = edp.Department.DepName
                           };

            return Ok(employee);
        }

        [HttpPost]
        public IActionResult SaveEmployee([FromBody] EmployeeListDto employee)
        {
            //var depId = employee.Employees.ToString();
            //var dep = depId.Count();
            if (employee != null && ModelState.IsValid)
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
                //if (dep > 1)
                //{
                //    foreach (var depp in )
                //    {
                //        EmpDepTbl edt = new EmpDepTbl()
                //        {
                //            EmployeeId = emp.Id,
                //            DepartmentId = 
                //        };
                //        _context.empDepTbls.Add(edt);
                //        _context.SaveChanges();
                //    }
                //}
                //else
                //{
                    EmpDepTbl edt = new EmpDepTbl()
                    {
                        EmployeeId = emp.Id,
                        DepartmentId = employee.DepartmentId
                    };
                    _context.empDepTbls.Add(edt);
                    _context.SaveChanges();
                //}
                return Ok();
            }
            return BadRequest();
        }
        [HttpDelete("{id:int}")]
        public IActionResult DeleteEmployee(int id, int depid)
        {
            var employee = from e in _context.employees
                           join edp in _context.empDepTbls
                           on e.Id equals edp.EmployeeId
                           where e.Id == id
                           select new EmployeeListDto()
                           {
                               Id = e.Id,
                               EmpName = e.EmpName,
                               Address = e.Address,
                               Number = e.Number,
                               Salary = e.Salary,
                               Designation = e.Designation.DesName,
                               DepartmentId = edp.Department.Id
                           };
           
            if (employee.Count() > 1)
            {
                EmpDepTbl edt = new EmpDepTbl()
                {
                    EmployeeId = id,
                    DepartmentId = depid
                };
                _context.empDepTbls.Remove(edt);
            }
            else
            {
                var employees = _context.employees.Find(id);
                if (employees == null) return BadRequest(error: "Error While Deleting");
                _context.employees.Remove(employees);
            }
            _context.SaveChanges();
            return Ok();
        }
        [HttpPut]
        public IActionResult UpdateEmployee([FromBody] EmployeeListDto employee)
        {
            if (employee == null) return NotFound();
            if (ModelState.IsValid)
            {
                Employee emp = new Employee()
                {
                    Id = employee.Id,
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
                    DepartmentId = employee.DepartmentId
                };
                _context.empDepTbls.Update(edt);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest(error: "Error While Updating");
        }
    }
}

