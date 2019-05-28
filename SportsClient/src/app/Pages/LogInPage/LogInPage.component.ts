import { Component } from '@angular/core';
import { Router } from "@angular/router";
import { OnInit } from "@angular/core";
import { RestApiService } from '../../Services/RestService';
import { LogInModel } from '../../Models/LogIn';

@Component({
  selector: 'app-Log-In',
  templateUrl: './LogInPage.component.html'
})
export class LogInComponent implements OnInit{
  title = 'Log In';
  public logIn:LogInModel;
  public Username:string;
  public Password:string;
  constructor(private _api:RestApiService,private _router: Router){
    this.logIn=new LogInModel();
    let token=sessionStorage.getItem("token");
    if(token!=null){
      this._router.navigate(['/Dashboard']);
    }
  }
  ngOnInit(): void {
  }
  public LogInButton(){
    console.log(this.logIn);
    this._api.postLogin(this.logIn).subscribe((data)=>{
      if(data.loginResult=="fail"){
        sessionStorage.setItem("token",null);
        console.log("fail");
      }
      else if(data.loginResult=="success"){
        sessionStorage.setItem("token",data.token);
        console.log(data.token);
        this._router.navigate(['/Dashboard']);
      }
    });
  }
}