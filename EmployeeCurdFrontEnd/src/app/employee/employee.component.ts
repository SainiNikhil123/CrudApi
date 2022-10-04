import { Department } from './../department';
import {EmployeeListDto} from './../employeeList-dto';
import { Component, Injectable, OnInit } from '@angular/core';
import { Designation } from '../designation';
import { DesignationService } from '../designation.service';
import { EmployeeService } from '../employee.service';
import Swal from 'sweetalert2'


@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.scss']
})

export class EmployeeComponent implements OnInit {

  emplyeeById:EmployeeListDto = new EmployeeListDto();
  editDeleteDisplay:boolean=false;
  editButton:boolean=false;
  deleteButton:boolean=false;
  addButton:boolean=false;
  DesignationList: Designation[]=[];
  DepartmentList: Department[]= [];
  EmployeeList: EmployeeListDto[] = []; 
  newEmployee:EmployeeListDto = new EmployeeListDto();
  editEmployee:EmployeeListDto =new EmployeeListDto();
  selectedDes:number=0;
  checkNo:boolean=false;
  count:number=0;
  selectedDepartmentId:[]=[];
  

  constructor(private employeeService:EmployeeService, private designationService:DesignationService) { }

  ngOnInit(): void {
    this.getAll();
    this.getAllDes();
    this.getAllDep();
    this.Display();
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
      
      this.employeeService.getAllEmployees().subscribe(
        (response)=>{
          this.EmployeeList=response;
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

  checkboxvalue(isChecked: any)
    {
    console.log(this.DepartmentList);    
     this.editEmployee.departmentId = this.DepartmentList.filter(x=>x.isselected==true).map(x=>x.id).toString();   

    // In this we have given default value for count 
    // because one department is already selected.
    
    //  if(this.count == 0)
    //  {
    //   this.count = 1;
    //  }
    //  isChecked ? this.count++ : this.count--;
    //  this.checkNo = this.count === 2 ? true : false;
    //  console.log(this.count);

    }

  checkbox(isChecked: any)
    {
      console.log(this.DepartmentList);

     // Two CheckBox Can Select Only (Work For Save Properly) 

     // isChecked ? this.count++ : this.count--;
     // this.checkNo = this.count === 2 ? true : false;
     // console.log(this.count);
    }

  Dropdown(e:any)
  {
   this.newEmployee.designationId = e.target.value;
  }

  saveClick()
  {
    this.newEmployee.id=0; 
    this.newEmployee.departmentId = this.DepartmentList.filter(x=>x.isselected==true).map(x=>x.id);
    this.EmployeeList.push(this.newEmployee.departmentId);
    this.newEmployee.departmentIds = this.newEmployee.departmentId;
    this.newEmployee.departmenteditid = null;
    this.newEmployee.departmentId = 0;    
    this.employeeService.saveEmployee(this.newEmployee).subscribe(
      (response)=>{        
        this.getAll();  
        this.clrRec();
        Swal.fire({
          position: 'top-end',
          icon: 'success',
          title: 'Employee has been saved',
          showConfirmButton: false,
          timer: 1500
        })      
      },
      (error)=>{
        console.log(error);
      }
    );
  }

  editClick(emp:EmployeeListDto)
  {
    //debugger;
    this.getAllDep();
    this.selectedDepartmentId = emp.departmentId;
    this.employeeService.getEmployee(emp.id).subscribe(
      (response)=>{
       
        this.emplyeeById = response;
        this.editEmployee = this.emplyeeById;
        this.editEmployee.departmenteditid = this.emplyeeById.departmentIds;
        this.editEmployee.designationId = this.emplyeeById.designationId;
          
        for(let dep of this.emplyeeById.departmentIds) 
        {
          this.DepartmentList.filter(x=>x.id == Number(dep)).map(x=>x.isselected = true);
        } 
        
        console.log(this.editEmployee.designationId)
      },
      (error)=>{
        console.log(error)
      }
    )  
  }

  updateClick()
  { 
    //debugger;
    this.editEmployee.departmentIds = this.DepartmentList.filter(x=>x.isselected==true).map(x=>x.id);
    if(this.editEmployee.departmentIds.length > 1)
    {
      this.editEmployee.departmentId=0;
    }
    this.employeeService.updateEmployee(this.editEmployee).subscribe(
      (response)=>{
        this.getAll();
        Swal.fire({
          position: 'top-end',
          icon: 'success',
          title: 'Employee has been Updated',
          showConfirmButton: false,
          timer: 1500
        })        
      },
      (error)=>{
        console.log(error);
      }
    );
  }

  deleteClick(emp:any)
  {
    //debugger;
    this.newEmployee=emp;
    this.newEmployee.departmentIds = this.EmployeeList.filter(x=>x.id == this.newEmployee.id).map(x=>x.departmentId)
    if(this.newEmployee.departmentIds.length == 1 )
    {
      this.newEmployee.departmentId = 0;
    }
    
    const swalWithBootstrapButtons = Swal.mixin({
      customClass: {
        confirmButton: 'btn btn-success',
        cancelButton: 'btn btn-danger'
      },
      buttonsStyling: true
    })
    
    swalWithBootstrapButtons.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#39db23',
      confirmButtonText: 'Yes, delete it!',
      cancelButtonColor:'#e81e3c',
      cancelButtonText: 'No, cancel!',
      reverseButtons: true
    }).then((result) => {
      if (result.isConfirmed) {
        this.employeeService.deleteEmployee(this.newEmployee.id,this.newEmployee.departmentId).subscribe(
          (response)=>{
            this.getAll()
          },
          (error)=>{
            console.log(error);
          }
        )
        swalWithBootstrapButtons.fire(
          'Deleted!',
          'Your file has been deleted.',
          'success'
        )
      } else if (
        result.dismiss === Swal.DismissReason.cancel
      ) {
        swalWithBootstrapButtons.fire(
          'Cancelled',
          'Data Not Deleted',
          'error'          
        )
      }
    })
    
  }

  EmployeeClick()
  {
    this.getAllDep(); 
    this.clrRec();
  }

  Display()
  {
    let role = localStorage.getItem("role")
    let add = localStorage.getItem("AddRole")
    let del = localStorage.getItem("DeleteRole")
    let edit = localStorage.getItem("EditRole")
    this.editDeleteDisplay= (role == "Admin");
    this.editButton = (edit=="true");
    this.addButton = (add == "true");
    this.deleteButton = (del == "true");
  }
}
