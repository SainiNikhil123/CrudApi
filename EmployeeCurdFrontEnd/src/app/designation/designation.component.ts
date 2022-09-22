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
  newDes:Designation=new Designation();
  editDes:Designation=new Designation();
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
  saveClick()
  {
    this.designationService.saveDesignation(this.newDes).subscribe(
      (response)=>{
        this.getAll();
      },
      (error)=>{
        console.log(error);
      }
    )
  }

  editClick(des:any)
  {
    this.editDes = des;

  }

  updateClick()
  {
    this.designationService.updateDesignation(this.editDes).subscribe(
      (response)=>{
        this.getAll();
      },
      (error)=>{
        console.log(error);
      }
    )
  }

}
