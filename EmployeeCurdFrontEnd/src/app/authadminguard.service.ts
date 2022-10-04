import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthadminguardService implements CanActivate {

  constructor(private route:Router, private jwtHelper: JwtHelperService) { }

  canActivate()
  {  
    const token = localStorage.getItem("jwt");
    const role = localStorage.getItem("role");

    if(token && !this.jwtHelper.isTokenExpired(token) && role == "Admin")
    {
      return true;
    }    
    this.route.navigate([""]);
    alert("Not Authorized")
    return false;
  }
}