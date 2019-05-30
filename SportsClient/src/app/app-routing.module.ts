import { NgModule, Component } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LogInComponent } from './Pages/LogInPage/logInPage.component';
import { DashboardComponent } from './Pages/Dashboard/dashboard.component';
import { DetailComponent } from './Pages/Detail/detail.component';
import { CreateTestComponent } from './Pages/CreateTest/createTest.component';
import { CreatUserComponent } from './Pages/CreateUser/createUser.component';
import { UserComponent } from './Pages/Users/user.component';
import { AddAthleteComponent } from './Pages/AddAthlete/addAthlete.component';
import { RegisterComponent } from './Pages/Registration/register.component';
import { EditUserComponent } from './Pages/EditUser/editUser.component';
import { EditTestComponent } from './Pages/EditTest/editTest.component';

const routes: Routes = [
  {path:'home',component :LogInComponent},
  {path:'',redirectTo:'home',pathMatch: "full"},
  {path:'Dashboard',component : DashboardComponent},
  {path:'detail',component:DetailComponent},
  {path:'createTest',component:CreateTestComponent},
  {path:'createUser',component:CreatUserComponent},
  {path:'User',component:UserComponent},
  {path:'addAthlete',component:AddAthleteComponent},
  {path:'Register',component:RegisterComponent},
  {path:"editResult",component:EditUserComponent},
  {path:"editTest",component:EditTestComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
