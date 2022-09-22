import { RoleService } from './../role.service';
import { Role } from './../role';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-roles',
  templateUrl: './roles.component.html',
  styleUrls: ['./roles.component.scss']
})
export class RolesComponent implements OnInit {

 
  RoleList :Role[]=[];
  newRole:Role=new Role();
  editRole:Role=new Role();

  constructor(private RoleService :RoleService) { }

  ngOnInit(): void {
    this.getAll();
  }
  getAll()
  { 
    this.RoleService.getRole().subscribe(
      (response)=>{
        this.RoleList =response;
        console.log(this.RoleList);
      },
      (error)=>{
        console.log(error);
      }
    )
  }
  saveClick()
  {
    this.RoleService.saveRole(this.newRole).subscribe(
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
    this.editRole = dep;

  }

  updateClick()
  {
    this.RoleService.updateRole(this.editRole).subscribe(
      (response)=>{
        this.getAll();
      },
      (error)=>{
        console.log(error);
      }
    )
  }

}
