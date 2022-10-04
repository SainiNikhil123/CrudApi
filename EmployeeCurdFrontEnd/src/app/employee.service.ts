import { EmployeeListDto } from './employeeList-dto';
import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  BaseUrl:string = "http://localhost:65158/api/employee/";

  constructor(private httpClient : HttpClient) { }

  getAllEmployees():Observable<any>
  {
    return this.httpClient.get<any>(this.BaseUrl);
  }
  getEmployee(id:number):Observable<any>
  {
    return this.httpClient.get<any>(this.BaseUrl+id);
  }
  saveEmployee(newEmployee:EmployeeListDto):Observable<EmployeeListDto>
  {
    return this.httpClient.post<EmployeeListDto>(this.BaseUrl,newEmployee);
  }
  updateEmployee(editEmployee:EmployeeListDto):Observable<EmployeeListDto>
  {
    return this.httpClient.put<EmployeeListDto>(this.BaseUrl,editEmployee);
  }
  deleteEmployee(id:number,depid:number):Observable<any>
  {
    return this.httpClient.delete<any>(this.BaseUrl + id +"?depid="+ depid);
  }
}
