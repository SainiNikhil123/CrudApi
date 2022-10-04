import { Claim } from './claim';
import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http'
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ClaimService {

  constructor(private httpClient :HttpClient) { }

  BaseUrl:string = "http://localhost:65158/api/Claims";

  getClaim():Observable<any>
  {
    return this.httpClient.get<any>(this.BaseUrl);
  }
  addClaim(newClaim:Claim):Observable<Claim>
  {
    return this.httpClient.post<Claim>(this.BaseUrl,newClaim);
  }
  updateClaim(editClaim:Claim):Observable<Claim>
  {
    return this.httpClient.put<Claim>(this.BaseUrl,editClaim);
  }
}
