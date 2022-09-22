import { UserService } from './../user.service';
import { Component, OnInit } from '@angular/core';
import { User } from '../user';
import { Router } from '@angular/router'; 

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit {

  UserList: User[]=[];

  constructor(private UserService: UserService,private router: Router) { }

  ngOnInit(): void {
  this.getAll();
  }

  onClick()
  {
    this.router.navigate(['register']) 
  }
  getAll()
  {
    this.UserService.getUser().subscribe(
      (response)=>{
        this.UserList=response;
        console.log(this.UserList)
      },
      (error)=>{
        console.log(error);
      }
    )
  }

}
