import { Router } from '@angular/router';
import { UserService } from './../user.service';
import { Component, OnInit } from '@angular/core';
import { Login } from '../login';
import { User } from '../user';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  Login:Login=new Login();
  Token:string="";
  invalidLogin:boolean=false;
  

  constructor(private UserService: UserService,private router: Router) { }

  ngOnInit(): void {
  }

  loginClick()
  {
    this.UserService.loginUser(this.Login).subscribe(
      (response)=>{
         this.Token=(<any>response).token;
         localStorage.setItem("jwt",this.Token);
         this.router.navigate([""]);
         console.log(this.Token);
      },
      (error)=>{
        this.invalidLogin = true;
        console.log(error);
        console.log(this.invalidLogin);   
      }
    )
  }
  RegClick()
  {
    this.router.navigate(['register']) 
  }

}
