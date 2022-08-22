import { Empdeptbl } from './../empdeptbl';
import { Employee } from './../employee';
import { DepartmentService } from './../department.service';
import { Department } from './../department';
import { Component, Injectable, OnInit } from '@angular/core';
import { Designation } from '../designation';
import { DesignationService } from '../designation.service';
import { FormBuilder, FormGroup, FormArray, FormControl, Validators } from '@angular/forms';
import { EmployeeService } from '../employee.service';


@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.scss']
})

export class EmployeeComponent implements OnInit {
  
  DesignationList: Designation[]=[];
  DepartmentList: Department[]= [];
  EmployeeList: Employee[] = []; 
  newEmployee:Employee = new Employee();
  editEmployee:Employee =new Employee();
  empdes:Empdeptbl= new Empdeptbl();
  selectedDes:number=0;
  editDes:number=0;
  

  constructor(private employeeService:EmployeeService, private designationService:DesignationService) { }

  ngOnInit(): void {
    this.getAll();
    this.getAllDes();
    this.getAllDep();
  }
  getAllDep()
  { 
    this.DepartmentList=[
      {id: 1, depName:"IT", isselected:false},
      {id: 2, depName:".NET", isselected:false},
      {id: 3, depName:"PHP", isselected:false},
      {id: 4, depName:"HR", isselected:false},
    ]
  }

  getAllDes()
  { 
    this.designationService.getDesignation().subscribe(
      (response)=>{
        this.DesignationList =response;
        console.log(this.DesignationList);
      },
      (error)=>{
        console.log(error);
      }
    )
  }

  getAll()
    {
      
      this.employeeService.getAllEmployee().subscribe(
        (response)=>{
          this.EmployeeList=response;
          //console.log(this.EmployeeList);
        },
        (error)=>{
          console.log(error);
        }
      )
    }
    clrRec()
    {
      this.newEmployee.empName="";
      this.newEmployee.address="";
      this.newEmployee.number="";
      this.newEmployee.salary="";
      this.newEmployee.designationId="";
      this.newEmployee.departmentId="";
    }

  checkboxvalue()
    {
    //console.log(this.DepartmentList)
    this.newEmployee.departmentId = this.DepartmentList.filter(x=>x.isselected==true).map(x=>x.id);
    
    //this.editEmployee.departmentId = this.DepartmentList.filter(x=>x.isselected==true).map(x=>x.id);
    }

  Dropdown(e:any)
  {
   
   this.newEmployee.designationId = e.target.value;
   this.selectedDes = this.newEmployee.designationId

   //console.log(this.employee.designationId);
  }

  saveClick()
  {
    this.newEmployee.id=0; 
    this.newEmployee.departmentId = this.DepartmentList.filter(x=>x.isselected==true).map(x=>x.id).join(",");
    
    this.EmployeeList.push(this.newEmployee.departmentId);
    this.empdes.employeeId = this.newEmployee.id;
    this.empdes.departmentId = this.newEmployee.departmentId;
    this.newEmployee.employees = this.empdes;
    //alert(this.employee.designationId);
    this.employeeService.saveEmployee(this.newEmployee).subscribe(
      (response)=>{
        this.clrRec();
        this.getAll();        
      },
      (error)=>{
        console.log(error);
      }
    );
  }
  editClick(emp:Employee)
  {
    
    //debugger;
    this.getAllDep();
    this.editEmployee=emp;
    this.editDes = this.editEmployee.designationId;
    
    //alert(this.editDes);
    let selectedDepartmentId = emp.departmentId;
    this.DepartmentList.filter(x=>x.id == Number(selectedDepartmentId)).map(x=>x.isselected = true);
    
    
  }

  updateClick()
  {

  }

  deleteClick(emp:any)
  {
   
    this.newEmployee=emp;
    
    let ans=confirm("Want To Delete Data ?")
    if(!ans) return;
    //alert(this.employee.id);
    this.employeeService.deleteEmployee(this.newEmployee.id,this.newEmployee.departmentId).subscribe(
      (response)=>{
        this.getAll();
      },
      (error)=>{
        console.log(error);
      }
    )
  }
}
