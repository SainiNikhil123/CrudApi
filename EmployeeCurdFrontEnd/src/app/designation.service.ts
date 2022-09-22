import { Designation } from './designation';
import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http'
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DesignationService {

  BaseUrl:string="http://localhost:65158/api/designation";

  constructor(private httpClient :HttpClient) { }

  getDesignation():Observable<any>
  {
    return this.httpClient.get<any>(this.BaseUrl);
  }
  saveDesignation(newDes:Designation):Observable<Designation>
  {
    return this.httpClient.post<Designation>(this.BaseUrl,newDes);
  }
  updateDesignation(editDes:Designation):Observable<Designation>
  {
    return this.httpClient.put<Designation>(this.BaseUrl,editDes);
  }
}
