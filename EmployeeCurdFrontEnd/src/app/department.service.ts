import { Department } from './department';
import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DepartmentService {

  BaseUrl:string = "http://localhost:65158/api/department";

  constructor(private httpClient :HttpClient) { }

  getDepartment():Observable<any>
  {
    return this.httpClient.get<any>(this.BaseUrl);
  }
  saveDepartment(newDep:Department):Observable<Department>
  {
    return this.httpClient.post<Department>(this.BaseUrl,newDep);
  }
  updateDepartment(editDep:Department):Observable<Department>
  {
    return this.httpClient.put<Department>(this.BaseUrl,editDep);
  }
}
