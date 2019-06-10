import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RestApiService } from './Services/rest.service';
import { HttpClient } from "@angular/common/http";
import { LogInComponent } from './Pages/LogInPage/logInPage.component';
import { HttpClientModule } from "@angular/common/http";
import { FormsModule } from "@angular/forms";
import { DashboardComponent } from './Pages/Dashboard/dashboard.component';
import { DetailComponent } from './Pages/Detail/detail.component';
import { CreateTestComponent } from './Pages/CreateTest/createTest.component';
import { CreatUserComponent } from './Pages/CreateUser/createUser.component';
import { UserComponent } from './Pages/Users/user.component';
import { AddAthleteComponent } from './Pages/AddAthlete/addAthlete.component';
import { RegisterComponent } from './Pages/Registration/register.component';
import { EditUserComponent } from './Pages/EditUser/editUser.component';
import { EditTestComponent } from './Pages/EditTest/editTest.component';
import { HTTP_INTERCEPTORS } from "@angular/common/http";
import { ApiInterceptor } from './Interceptor/api.interceptor';
import { StoreModule } from "@ngrx/store";
import { testreducer } from './reducer/test.reducer';
import { athletereducer } from "src/app/reducer/athlete.reducer";
import { selectedtestreducer } from './reducer/selectedTest.reducer';
import { selectedathletereducer } from './reducer/selectedAthlete.reducer';
@NgModule({
  declarations: [
    AppComponent,
    LogInComponent,
    DashboardComponent,
    DetailComponent,
    CreateTestComponent,
    CreatUserComponent,
    UserComponent,
    AddAthleteComponent,
    RegisterComponent,
    EditUserComponent,
    EditTestComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,HttpClientModule,FormsModule,
     StoreModule.forRoot({
      test: testreducer,
      athlete:athletereducer,
      selectedTest:selectedtestreducer,
      selectedAthlete:selectedathletereducer
    })
  ],
  providers: [RestApiService,HttpClient,
  { provide: HTTP_INTERCEPTORS, useClass: ApiInterceptor, multi: true }],
  bootstrap: [AppComponent]
})
export class AppModule { }