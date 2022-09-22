import { Component, OnInit } from '@angular/core';
import { Department } from '../department';
import { DepartmentService } from '../department.service';

@Component({
  selector: 'app-department',
  templateUrl: './department.component.html',
  styleUrls: ['./department.component.scss']
})
export class DepartmentComponent implements OnInit {

  DepartmentList :Department[]=[];
  newDep:Department=new Department();
  editDep:Department=new Department();

  constructor(private departmentService :DepartmentService) { }

  ngOnInit(): void {
    this.getAll();
  }
  getAll()
  { 
    this.departmentService.getDepartment().subscribe(
      (response)=>{
        this.DepartmentList =response;
        console.log(this.DepartmentList);
      },
      (error)=>{
        console.log(error);
      }
    )
  }
  saveClick()
  {
    this.departmentService.saveDepartment(this.newDep).subscribe(
      (response)=>{
        this.getAll();
      },
      (error)=>{
        console.log(error);
      }
    )
  }

  editClick(dep:any)
  {
    this.editDep = dep;

  }

  updateClick()
  {
    this.departmentService.updateDepartment(this.editDep).subscribe(
      (response)=>{
        this.getAll();
      },
      (error)=>{
        console.log(error);
      }
    )
  }
}
