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
        //private readonly IMapper _mapper;
        public EmployeeController(ApplicationDbContext context /*,IMapper mapper*/)
        {
            _context = context;
            //_mapper = mapper;
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
                            });

            return Ok(employee);
        }

        #endregion

        #region post/put
        [HttpPost]
        public IActionResult SaveEmployee([FromBody] EmployeeListDto employee)
        {
            //string depId = employee.DepartmentIds;
            //char[] deppId = depId.ToCharArray();

            //var dep = employee.DepartmentIds.Count();

            if (employee != null && ModelState.IsValid)
            {
                Employee emp = new Employee()
                {
                    EmpId = employee.Id,
                    EmpName = employee.EmpName,
                    Address = employee.Address,
                    Number = employee.Number,
                    Salary = employee.Salary,
                    DesignationId = employee.DesignationId
                };
                _context.employees.Add(emp);
                _context.SaveChanges();

                foreach (var d in employee.DepartmentIds)
                {
                    EmpDepTbl edt = new EmpDepTbl()
                    {
                        EmployeeId = emp.EmpId,
                        DepartmentId = d
                    };
                    _context.empDepTbls.Add(edt);
                    _context.SaveChanges();
                }

                return Ok();
            }
            return BadRequest();
        }
        [HttpPut]
        public IActionResult UpdateEmployee([FromBody] EmployeeListDto employee)
        {
            var empl = _context.employees.Find(employee.Id);
            if (empl == null) return BadRequest(error: "Data Not Found");

            if (employee != null && ModelState.IsValid)
            {
                empl.EmpId = employee.Id;
                empl.EmpName = employee.EmpName;
                empl.Address = employee.Address;
                empl.Number = employee.Number;
                empl.Salary = employee.Salary;
                empl.DesignationId = employee.DesignationId;

                _context.employees.Update(empl);
                _context.SaveChanges();
                if (employee.DepartmentId != employee.Departmenteditid)
                {
                    var edt = _context.empDepTbls.FirstOrDefault(x => x.EmployeeId == employee.Id && x.DepartmentId == employee.Departmenteditid);
                    _context.empDepTbls.Remove(edt);
                    _context.SaveChanges();

                    EmpDepTbl edt1 = new EmpDepTbl()
                    {
                        EmployeeId = employee.Id,
                        DepartmentId = employee.DepartmentId
                    };
                    _context.empDepTbls.Add(edt1);
                    _context.SaveChanges();
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

