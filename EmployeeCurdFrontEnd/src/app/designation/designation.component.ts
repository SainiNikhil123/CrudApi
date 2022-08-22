import { Component, OnInit } from '@angular/core';
import { Department } from '../department';
import { Designation } from '../designation';
import { DesignationService } from '../designation.service';

@Component({
  selector: 'app-designation',
  templateUrl: './designation.component.html',
  styleUrls: ['./designation.component.scss']
})
export class DesignationComponent implements OnInit {

  DesignationList :Designation[]=[];
  DepartmentList :Department[]=[];
  constructor(private designationService :DesignationService) { }

  ngOnInit(): void {
    this.getAll();
  }
  getAll()
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

}
