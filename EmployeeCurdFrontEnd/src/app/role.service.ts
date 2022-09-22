import { Role } from './role';
import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RoleService {

  BaseUrl:string = "http://localhost:65158/api/Role/";

  constructor(private httpClient :HttpClient) { }

  getRole():Observable<any>
  {
    return this.httpClient.get<any>(this.BaseUrl);
  }
  saveRole(newRole:Role):Observable<Role>
  {
    return this.httpClient.post<Role>(this.BaseUrl,newRole);
  }
  updateRole(editRole:Role):Observable<Role>
  {
    return this.httpClient.put<Role>(this.BaseUrl,editRole);
  }
}
