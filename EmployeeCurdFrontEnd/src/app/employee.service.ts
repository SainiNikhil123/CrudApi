import { Empdeptbl } from './empdeptbl';
import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { Observable } from 'rxjs';
import { Employee } from './employee';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  constructor(private httpClient : HttpClient) { }

  getAllEmployee():Observable<any>
  {
    return this.httpClient.get<any>("http://localhost:65158/api/employee");
  }
  saveEmployee(newEmployee:Employee):Observable<Employee>
  {
    return this.httpClient.post<Employee>("http://localhost:65158/api/employee",newEmployee);
  }
  updateEmployee(editEmployee:Employee):Observable<Employee>
  {
    return this.httpClient.post<Employee>("http://localhost:65158/api/employee",editEmployee);
  }
  deleteEmployee(id:number,depid:number):Observable<any>
  {
    return this.httpClient.delete<any>("http://localhost:65158/api/employee/"+id +"?depid="+ depid);
  }
}
