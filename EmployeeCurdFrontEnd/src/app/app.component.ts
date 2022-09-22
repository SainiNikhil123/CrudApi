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

  isAuthorized()
{
  const token= localStorage.getItem("jwt");
  
    if(token)
    {
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
  this.router.navigate(['login']);
}
}


