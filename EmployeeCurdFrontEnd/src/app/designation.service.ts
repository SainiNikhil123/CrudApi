import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http'
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DesignationService {

  constructor(private httpClient :HttpClient) { }

  getDesignation():Observable<any>
  {
    return this.httpClient.get<any>("http://localhost:65158/api/designation");
  }
}
