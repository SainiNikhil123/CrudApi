import { HttpClient } from '@angular/common/http';
import { ClaimDto } from './../claim-dto';
import { Claim } from './../claim';
import { UserService } from './../user.service';
import { Component, OnInit } from '@angular/core';
import { User } from '../user';
import { Router } from '@angular/router'; 

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit {

  UserList: User[]=[];
  ClaimList: Claim[] =[];
  UserClaims: ClaimDto = new ClaimDto();
  editClaims: ClaimDto = new ClaimDto();
  
  dispUser:boolean = false;

  constructor(private UserService: UserService,private router: Router) { }

  ngOnInit(): void {
  this.getAll();
  this.getAllClaim();
  this.MenuDisplay();
  }

  MenuDisplay()
{
 let role = localStorage.getItem("role")
 
 this.dispUser = (role == "Admin" );

}

  getAllClaim()
  { 
    this.ClaimList=[
      {id: 1, name:"Edit Role", isselected:false},
      {id: 2, name:"Delete Role", isselected:false},
      {id: 3, name:"Add Role", isselected:false}
    ]
  }

  claimClick(id:any)
  {
    this.getAllClaim();
     this.UserService.getClaimBYId(id).subscribe(
      (response)=>{
         this.UserClaims = response;
         this.editClaims.userId = this.editClaims.userId;
         this.editClaims.oldClaimId = this.UserClaims.claimIds;
         for(let clm of this.UserClaims.claimIds) 
         {
           this.ClaimList.filter(x=>x.id == Number(clm)).map(x=>x.isselected = true);
         } 

    console.log(response);
  },
  (error)=>{
    console.log(error);
  })
  }

  checkboxvalue()
  {
    this.editClaims.claimIds = this.ClaimList.filter(x=>x.isselected==true).map(x=>x.id); 
  }

  updateClick()
  {
    debugger; 
    this.editClaims.userId = this.UserClaims.userId;
    this.editClaims.claims=null;
    this.UserService.updateClaims(this.editClaims).subscribe(
      (response)=>{
        this.getAll();
      },
      (error)=>{
        console.log(error);
      }
    )
    
  }

  onClick()
  {
    this.router.navigate(['register']) 
  }
  getAll()
  {
    this.UserService.getUser().subscribe(
      (response)=>{
        this.UserList=response;
        console.log(this.UserList)
      },
      (error)=>{
        console.log(error);
      }
    )
  }

}
