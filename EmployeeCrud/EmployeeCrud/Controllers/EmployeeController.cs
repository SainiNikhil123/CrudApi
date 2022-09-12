using AutoMapper;
using EmployeeCrud.Data;
using EmployeeCrud.Models;
using EmployeeCrud.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;
using System.IO;
using System.Text;

namespace EmployeeCrud.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public EmployeeController(ApplicationDbContext context )
        {
            _context = context;
        }
        #region get/find
        [HttpGet]
        public IActionResult GetEmployees()
        {
            var employeeList = (from e in _context.employees
                                join edp in _context.empDepTbls
                                on e.EmpId equals edp.EmployeeId
                                select new EmployeeListDto()
                                {
                                    Id = e.EmpId,
                                    EmpName = e.EmpName,
                                    Address = e.Address,
                                    Number = e.Number,
                                    Salary = e.Salary,
                                    DesignationId = e.DesignationId,
                                    Designation = e.Designation.DesName,
                                    DepartmentId = edp.Department.Id,
                                    Department = edp.Department.DepName,
                                });

            return Ok(employeeList);
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployee(int id)
        {
            var employeeFromDb = _context.employees.Find(id);
            if (employeeFromDb == null) return BadRequest(error: "Employee Id Is Invalid");

            var employee = (from e in _context.employees
                            join edp in _context.empDepTbls
                            on e.EmpId equals edp.EmployeeId
                            where e.EmpId == id
                            select new EmployeeListDto()
                            {
                                Id = e.EmpId,
                                EmpName = e.EmpName,
                                Address = e.Address,
                                Number = e.Number,
                                Salary = e.Salary,
                                DesignationId = e.DesignationId,
                                Designation = e.Designation.DesName,
                                DepartmentId = edp.Department.Id,
                                Department = edp.Department.DepName
                            }).FirstOrDefault();

            return Ok(employee);
        }

        #endregion

        #region post/put
        [HttpPost]
        public IActionResult SaveEmployee([FromBody] EmployeeListDto newEmployee)
        {
            if (newEmployee != null && ModelState.IsValid)
            {
                Employee employee = new Employee()
                {
                    EmpId = newEmployee.Id,
                    EmpName = newEmployee.EmpName,
                    Address = newEmployee.Address,
                    Number = newEmployee.Number,
                    Salary = newEmployee.Salary,
                    DesignationId = newEmployee.DesignationId
                };
                _context.employees.Add(employee);
                _context.SaveChanges();

                foreach (var dep in newEmployee.DepartmentIds)
                {
                    EmpDepTbl edt = new EmpDepTbl()
                    {
                        EmployeeId = employee.EmpId,
                        DepartmentId = dep
                    };
                    _context.empDepTbls.Add(edt);
                    _context.SaveChanges();
                }

                return Ok();
            }
            return BadRequest();
        }
        [HttpPut]
        public IActionResult UpdateEmployee([FromBody] EmployeeListDto editEmployee)
        {
            var employee = _context.employees.Find(editEmployee.Id);
            if (employee == null) return BadRequest(error: "Data Not Found");

            if (editEmployee != null && ModelState.IsValid)
            {
                employee.EmpId = editEmployee.Id;
                employee.EmpName = editEmployee.EmpName;
                employee.Address = editEmployee.Address;
                employee.Number = editEmployee.Number;
                employee.Salary = editEmployee.Salary;
                employee.DesignationId = editEmployee.DesignationId;

                _context.employees.Update(employee);
                _context.SaveChanges();

                if (editEmployee.DepartmentIds.Count > 1)
                {
                    var empdep = _context.empDepTbls.FirstOrDefault(x => x.EmployeeId == editEmployee.Id && x.DepartmentId == editEmployee.Departmenteditid);
                    _context.empDepTbls.Remove(empdep);
                    _context.SaveChanges();

                    foreach (var d in editEmployee.DepartmentIds)
                    {
                        EmpDepTbl edt = new EmpDepTbl()
                        {
                            EmployeeId = employee.EmpId,
                            DepartmentId = d
                        };
                        _context.empDepTbls.Add(edt);
                        _context.SaveChanges();
                    }
                }
                else
                {
                    if (editEmployee.Departmenteditid != editEmployee.DepartmentId)
                    {
                        var empdep = _context.empDepTbls.FirstOrDefault(x => x.EmployeeId == editEmployee.Id && x.DepartmentId == editEmployee.Departmenteditid);
                        _context.empDepTbls.Remove(empdep);
                        _context.SaveChanges();
                        EmpDepTbl edt = new EmpDepTbl()
                        {
                            EmployeeId = employee.EmpId,
                            DepartmentId = editEmployee.DepartmentId
                        };
                        _context.empDepTbls.Add(edt);
                        _context.SaveChanges();
                    }
                }
                return Ok();
            }
            return BadRequest(error: "Error While Updating");
        }
            

    #endregion

    #region del
    [HttpDelete("{id:int}")]
    public IActionResult DeleteEmployee(int id, int depid)
    {
           
        var employee = from e in _context.employees
                       join edp in _context.empDepTbls
                       on e.EmpId equals edp.EmployeeId
                       where e.EmpId == id
                       select new
                       {
                           id = e.EmpId,
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
}
    #endregion




}

