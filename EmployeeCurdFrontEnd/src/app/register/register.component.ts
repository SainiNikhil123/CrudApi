import { Router } from '@angular/router';
import { RoleService } from './../role.service';
import { Role } from './../role';
import { UserService } from './../user.service';
import { Component, OnInit } from '@angular/core';
import { User } from '../user';
import Swal from 'sweetalert2'

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  newReg:User = new User();
  RoleList:Role[]=[];
  selectedRole:number=0;

  constructor(private UserService: UserService, private RoleService:RoleService, private route:Router) { }

  ngOnInit(): void {
    this.getAllRole();
  }

  Register()
  {
    this.newReg.id = 0;
    this.newReg.roleId = this.selectedRole;
    //this.newReg.role = null;
    this.UserService.registerUser(this.newReg).subscribe(
      (response)=>{
        console.log(response);
        this.route.navigate([""]);
        Swal.fire({
          position: 'top-end',
          icon: 'success',
          title: 'You are Sucessfully Registered',
          showConfirmButton: false,
          timer: 1500
        })        
      },
      (error)=>{
        console.log(error);
      }
    )
  }

  getAllRole()
  { 
    this.RoleService.getRole().subscribe(
      (response)=>{
        this.RoleList = response;
        console.log(this.RoleList);
      },
      (error)=>{
        console.log(error);
      }
    )
  }

  Dropdown(e:any)
  {
     console.log(e);
     this.selectedRole = e.target.value;
  }
}
