import { Empdeptbl } from './empdeptbl';
export class Employee {
    id:any;
    empName:any;
    address:any;
    number:any;
    salary:any;
    designationId:any;
    designation:any;
    departmentId:any;
    department:any;
    employees:Empdeptbl; 
    
    
       
    constructor()
    {
        this.id=null;
        this.empName=null;
        this.address=null;
        this.salary=null;
        this.departmentId=null;
        this.department=null;
        this.designationId=null;
        this.designation=null;
        this.employees= this.departmentId;
              
    }

}

