export class ClaimDto {
    userId:any
    claimIds:number[]
    oldClaimId:number[]
    claims:any
    constructor(){
        this.userId="";
        this.claimIds=[];
        this.oldClaimId=[];
        this.claims="";
    }
}
