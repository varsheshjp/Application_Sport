import { Component } from '@angular/core';
import { Router } from "@angular/router";
import { OnInit } from "@angular/core";
import { RestApiService } from '../../Services/rest.service';
import { LogInModel } from '../../Models/logIn.model';
import { Test } from '../../Models/test.model';
import { ResponseBoolean } from '../../Models/responseBoolean.model';
import { User } from '../../Models/user.model';
@Component({
  selector: 'app-User',
  templateUrl: './user.component.html'
})
export class UserComponent implements OnInit{
    public userList:User[];
    constructor(private _api:RestApiService,private _router:Router){
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