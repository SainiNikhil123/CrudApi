import { EmployeeListDto } from './employeeList-dto';
import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  constructor(private httpClient : HttpClient) { }

  getAllEmployees():Observable<any>
  {
    return this.httpClient.get<any>("http://localhost:65158/api/employee");
  }
  getEmployee(id:number):Observable<any>
  {
    return this.httpClient.get<any>("http://localhost:65158/api/employee/"+id);
  }
  saveEmployee(newEmployee:EmployeeListDto):Observable<EmployeeListDto>
  {
    return this.httpClient.post<EmployeeListDto>("http://localhost:65158/api/employee",newEmployee);
  }
  updateEmployee(editEmployee:EmployeeListDto):Observable<EmployeeListDto>
  {
    return this.httpClient.put<EmployeeListDto>("http://localhost:65158/api/employee",editEmployee);
  }
  deleteEmployee(id:number,depid:number):Observable<any>
  {
    return this.httpClient.delete<any>("http://localhost:65158/api/employee/"+id +"?depid="+ depid);
  }
}
