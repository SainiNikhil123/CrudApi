import { Component } from '@angular/core';
import { Router } from '@angular/router'; 

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'EmployeeCurdFrontEnd';

  constructor(private router: Router) { }

  ngOnInit(): void {

    this.MenuDisplay();
    this.UserName

  }
  ngDoCheck():void{

    this.MenuDisplay();

  }
  
  UserName:any="";
  dispComp:boolean = false;
  dispUser:boolean = false;

  isAuthorized()
{
  const token= localStorage.getItem("jwt");
  
    if(token)
    {
      let tokenData = token.split('.')[1]
      let decodeJsonData = window.atob(tokenData)
      let decodeTokenData = JSON.parse(decodeJsonData)
      this.UserName = decodeTokenData.unique_name;
      //console.log( this.UserName)
      return true;
    }
    else
    { 
      return false;
    }  
}

Logout()
{
  localStorage.removeItem("jwt");
  localStorage.removeItem("EditRole");
  localStorage.removeItem("DeleteRole");
  localStorage.removeItem("AddRole");
  localStorage.removeItem("role");
  this.router.navigate(['login']);
}

MenuDisplay()
{
 let role = localStorage.getItem("role")
 this.dispComp = (role == "Admin");
 this.dispUser = (role == "Admin" || role == "Employee");

}
}





