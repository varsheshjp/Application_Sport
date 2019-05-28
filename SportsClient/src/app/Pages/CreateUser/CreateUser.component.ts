import { Component } from '@angular/core';
import { Router } from "@angular/router";
import { OnInit } from "@angular/core";
import { RestApiService } from '../../Services/RestService';
import { LogInModel } from '../../Models/LogIn';
import { Test } from '../../Models/Test';
import { LocalSateService } from '../../Services/LocalSatetService';
import { ResponseBoolean } from '../../Models/ResponseBoolean';
import { User } from '../../Models/User';

@Component({
  selector: 'app-CreateUser',
  templateUrl: './CreateUser.component.html'
})
export class CreatUserComponent implements OnInit{
    public user:User;
    constructor(private _api:RestApiService,private _router: Router,private _localSate:LocalSateService){
        this.user=new User();
    }
    ngOnInit(): void {
        if(sessionStorage.getItem("token")==null){
            console.log("at ngoninit not log in");
            this._router.navigate(["/home"]);
        }
    }
    CreateUser(){
        this._api.createUser(this.user).subscribe((data)=>{
            this._router.navigate(["/User"]);
        });
    }
}