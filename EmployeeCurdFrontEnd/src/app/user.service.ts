import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from './user';
import { Login } from './login';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  BaseUrl:string = "http://localhost:65158/api/User/";

  constructor(private httpClient :HttpClient) { }

  getUser():Observable<any>
  {
    return this.httpClient.get<any>(this.BaseUrl);
  }

  registerUser(reg:User):Observable<User>
  {
    return this.httpClient.post<User>(this.BaseUrl +"Register",reg);
  }

  loginUser(login:Login):Observable<Login>
  {
    return this.httpClient.post<Login>(this.BaseUrl+"Authenticate",login);
  }
}
