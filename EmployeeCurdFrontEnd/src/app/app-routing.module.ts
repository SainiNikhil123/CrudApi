import { ClaimComponent } from './claim/claim.component';
import { LoginComponent } from './login/login.component';
import { UserComponent } from './user/user.component';
import { RolesComponent } from './roles/roles.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DepartmentComponent } from './department/department.component';
import { DesignationComponent } from './designation/designation.component';
import { EmployeeComponent } from './employee/employee.component';
import { RegisterComponent } from './register/register.component';
import { AuthGuardService } from './auth-guard.service';
import {AuthadminguardService} from './authadminguard.service'
import {AuthAdminEmpGuardService} from './auth-admin-emp-guard.service'

const routes: Routes = [
  {path:"",component:EmployeeComponent, canActivate: [AuthGuardService]},
  {path:"employee",component:EmployeeComponent, canActivate: [AuthGuardService]},
  {path:"department",component:DepartmentComponent, canActivate: [AuthadminguardService]},
  {path:"designation",component:DesignationComponent, canActivate: [AuthadminguardService]},
  {path:"role",component:RolesComponent,canActivate: [AuthadminguardService]},
  {path:"user",component:UserComponent,canActivate: [AuthAdminEmpGuardService]},
  {path:"register",component:RegisterComponent},
  {path:"login",component:LoginComponent},
  {path:"claim",component:ClaimComponent,canActivate: [AuthadminguardService]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
