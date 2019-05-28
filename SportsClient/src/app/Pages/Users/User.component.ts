import { Component } from '@angular/core';
import { Router } from "@angular/router";
import { OnInit } from "@angular/core";
import { RestApiService } from '../../Services/RestService';
import { LogInModel } from '../../Models/LogIn';
import { Test } from '../../Models/Test';
import { LocalSateService } from '../../Services/LocalSatetService';
import { Athlete } from '../../Models/Athlete';
import { User } from '../../Models/User';

@Component({
  selector: 'app-User',
  templateUrl: './User.component.html'
})
export class UserComponent implements OnInit{
    public userList:User[];
    constructor(private _api:RestApiService,private _router:Router,private _localState:LocalSateService){
        this.userList=[];
    }
    ngOnInit(): void {
        if(sessionStorage.getItem("token")==null){
            console.log("at ngoninit not log in");
            this._router.navigate(["/home"]);
        }
        else{
        this._api.getUsersList().subscribe((data:User[])=>{
            this.userList=data;
        })}
    }
    Delete(user:User){
        this._api.deleteUser(user).subscribe((data)=>
        {
            this.userList = this.userList.filter(item => item !== user);
        });
    }
}