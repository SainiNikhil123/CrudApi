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
}
