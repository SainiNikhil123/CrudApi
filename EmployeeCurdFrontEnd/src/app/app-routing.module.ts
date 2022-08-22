import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Department } from './department';
import { DepartmentComponent } from './department/department.component';
import { DesignationComponent } from './designation/designation.component';
import { EmployeeComponent } from './employee/employee.component';

const routes: Routes = [
  {path:"employee",component:EmployeeComponent},
  {path:"department",component:DepartmentComponent},
  {path:"designation",component:DesignationComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
