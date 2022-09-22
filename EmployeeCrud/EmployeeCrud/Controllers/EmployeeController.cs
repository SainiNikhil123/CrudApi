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
using Microsoft.AspNetCore.Authorization;

namespace EmployeeCrud.Controllers
{
    [Route("api/employee")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public EmployeeController(ApplicationDbContext context)
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
                                    Department = edp.Department.DepName
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

                List<EmpDepTbl> empDepTbls = new List<EmpDepTbl>();
                foreach (var dep in newEmployee.DepartmentIds)
                {
                    EmpDepTbl edt = new EmpDepTbl()
                    {
                        EmployeeId = employee.EmpId,
                        DepartmentId = dep
                    };
                    empDepTbls.Add(edt);
                }
                _context.empDepTbls.AddRange(empDepTbls);
                _context.SaveChanges();
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

                var empdep = _context.empDepTbls.FirstOrDefault(x => x.EmployeeId == editEmployee.Id && x.DepartmentId == editEmployee.Departmenteditid);
                _context.empDepTbls.Remove(empdep);
                _context.SaveChanges();

                if (editEmployee.DepartmentIds.Count > 1)
                {
                    List<EmpDepTbl> empDepTbls = new List<EmpDepTbl>();
                    foreach (var d in editEmployee.DepartmentIds)
                    {
                        EmpDepTbl edt = new EmpDepTbl()
                        {
                            EmployeeId = employee.EmpId,
                            DepartmentId = d
                        };
                        empDepTbls.Add(edt);

                    }
                    _context.empDepTbls.AddRange(empDepTbls);
                    _context.SaveChanges();
                }
                else
                {
                    if (editEmployee.Departmenteditid != editEmployee.DepartmentId)
                    {
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
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id, int depid)
        {
            if (depid != 0)
            {
                var emplyee = _context.empDepTbls.FirstOrDefault(x => x.EmployeeId == id && x.DepartmentId == depid);
                if (emplyee == null) return BadRequest(error: "EmployeeId Or DepartmentId Incorect");
                _context.empDepTbls.Remove(emplyee);
            }
            else
            {
                var employee = _context.employees.Find(id);
                if (employee == null) return BadRequest(error: "EmployeeId Incorect");
                _context.employees.Remove(employee);
            }
            _context.SaveChanges();
            return Ok();
        }
    }
    #endregion




}

