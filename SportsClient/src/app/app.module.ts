import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RestApiService } from './Services/RestService';
import { HttpClient } from "@angular/common/http";
import { LogInComponent } from './Pages/LogInPage/LogInPage.component';
import { HttpClientModule } from "@angular/common/http";
import { FormsModule } from "@angular/forms";
import { DashboardComponent } from './Pages/Dashboard/Dashboard.component';
import { LocalSateService } from './Services/LocalSatetService';
import { DetailComponent } from './Pages/Detail/Detail.component';
import { CreateTestComponent } from './Pages/CreateTest/CreateTest.component';
import { CreatUserComponent } from './Pages/CreateUser/CreateUser.component';
import { UserComponent } from './Pages/Users/User.component';
import { AddAthleteComponent } from './Pages/AddAthlete/AddAthlete.component';

@NgModule({
  declarations: [
    AppComponent,
    LogInComponent,
    DashboardComponent,
    DetailComponent,
    CreateTestComponent,
    CreatUserComponent,
    UserComponent,
    AddAthleteComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,HttpClientModule,FormsModule
  ],
  providers: [RestApiService,HttpClient,LocalSateService],
  bootstrap: [AppComponent]
})
export class AppModule { }
