import { Component } from '@angular/core';
import { Router } from "@angular/router";
import { OnInit } from "@angular/core";
import { RestApiService } from '../../Services/rest.service';
import { LocalSateService } from '../../Services/localSatet.service';
import { LogInModel } from '../../Models/logIn.model';
import { Test } from '../../Models/test.model';
import { ResponseBoolean } from '../../Models/responseBoolean.model';
import { User } from '../../Models/user.model';

@Component({
  selector: 'app-CreateUser',
  templateUrl: './createUser.component.html'
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