import { LoginComponent } from './login/login.component';
import { UserComponent } from './user/user.component';
import { RolesComponent } from './roles/roles.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Department } from './department';
import { DepartmentComponent } from './department/department.component';
import { DesignationComponent } from './designation/designation.component';
import { EmployeeComponent } from './employee/employee.component';
import { RegisterComponent } from './register/register.component';
import { AuthGuardService } from './auth-guard.service';

const routes: Routes = [
  {path:"",component:EmployeeComponent, canActivate: [AuthGuardService]},
  {path:"employee",component:EmployeeComponent, canActivate: [AuthGuardService]},
  {path:"department",component:DepartmentComponent, canActivate: [AuthGuardService]},
  {path:"designation",component:DesignationComponent, canActivate: [AuthGuardService]},
  {path:"role",component:RolesComponent,canActivate: [AuthGuardService]},
  {path:"user",component:UserComponent,canActivate: [AuthGuardService]},
  {path:"register",component:RegisterComponent},
  {path:"login",component:LoginComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
