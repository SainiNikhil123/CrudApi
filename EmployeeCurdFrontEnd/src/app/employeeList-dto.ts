import { Department } from './department';
import { Empdeptbl } from './empdeptbl';
export class EmployeeListDto {
     id:any;
     empName:any;
     address:any; 
     number:any;
     salary:any;
     designationId:any;
     designation:any;
     departmentId:any;
     department:any;
     departmentIds:any;
     departmenteditid:any;
     constructor()
     {
        this.id="";
        this.empName="";
        this.address=""; 
        this.number="";
        this.salary="";
        this.designationId="";
        this.designation="";
        this.departmentId="";
        this.department="";
        this.departmentIds="";
        this.departmenteditid="";
     }
}

