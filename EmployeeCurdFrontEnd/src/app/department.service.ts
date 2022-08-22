import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DepartmentService {

  constructor(private httpClient :HttpClient) { }

  getDepartment():Observable<any>
  {
    return this.httpClient.get<any>("http://localhost:65158/api/department");
  }
}
