import { NgModule, Component } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LogInComponent } from './Pages/LogInPage/LogInPage.component';
import { DashboardComponent } from './Pages/Dashboard/Dashboard.component';
import { DetailComponent } from './Pages/Detail/Detail.component';
import { CreateTestComponent } from './Pages/CreateTest/CreateTest.component';
import { CreatUserComponent } from './Pages/CreateUser/CreateUser.component';
import { UserComponent } from './Pages/Users/User.component';
import { AddAthleteComponent } from './Pages/AddAthlete/AddAthlete.component';

const routes: Routes = [
  {path:'home',component :LogInComponent},
  {path:'',redirectTo:'home',pathMatch: "full"},
  {path:'Dashboard',component : DashboardComponent},
  {path:'detail',component:DetailComponent},
  {path:'createTest',component:CreateTestComponent},
  {path:'createUser',component:CreatUserComponent},
  {path:'User',component:UserComponent},
  {path:'addAthlete',component:AddAthleteComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
