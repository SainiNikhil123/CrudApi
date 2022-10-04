import { ClaimDto } from './claim-dto';
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

  loginUser(login:any):Observable<any>
  {
    return this.httpClient.post<any>(this.BaseUrl+"Authenticate",login);
  }

  getClaimBYId(id:any):Observable<any>
  {
    return this.httpClient.get<any>(this.BaseUrl+"Claims?id="+id);
  }

  updateClaims(editClm:ClaimDto):Observable<any>
  {
    return this.httpClient.put<any>(this.BaseUrl+"Claim",editClm);
  }
}
