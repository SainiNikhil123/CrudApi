import { ClaimService } from './../claim.service';
import { Claim } from './../claim';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-claim',
  templateUrl: './claim.component.html',
  styleUrls: ['./claim.component.scss']
})
export class ClaimComponent implements OnInit {

  ClaimList :Claim[]=[];
  newClaim:Claim=new Claim();
  editClaim:Claim=new Claim();

  constructor(private ClaimService :ClaimService) { }

  ngOnInit(): void {
    this.getAll();
  }
  getAll()
  { 
    this.ClaimService.getClaim().subscribe(
      (response)=>{
        this.ClaimList =response;
        console.log(this.ClaimList);
      },
      (error)=>{
        console.log(error);
      }
    )
  }
  saveClick()
  {
    this.ClaimService.addClaim(this.newClaim).subscribe(
      (response)=>{
        this.getAll();
      },
      (error)=>{
        console.log(error);
      }
    )
  }

  editClick(dep:any)
  {
    this.editClaim = dep;

  }

  updateClick()
  {
    this.ClaimService.updateClaim(this.editClaim).subscribe(
      (response)=>{
        this.getAll();
      },
      (error)=>{
        console.log(error);
      }
    )
  }

}
