import { Component } from '@angular/core';
import { Router } from "@angular/router";
import { OnInit } from "@angular/core";
import { RestApiService } from '../../Services/RestService';
import { LogInModel } from '../../Models/LogIn';
import { Test } from '../../Models/Test';
import { LocalSateService } from '../../Services/LocalSatetService';
import { ResponseBoolean } from '../../Models/ResponseBoolean';
import { User } from '../../Models/User';
import { Athlete } from '../../Models/Athlete';

@Component({
  selector: 'app-EditUser',
  templateUrl: './EditUser.component.html'
})
export class EditUserComponent implements OnInit{
    public athlete:Athlete;
    constructor(private _api:RestApiService,private _router: Router,private _localSate:LocalSateService){
        this.athlete=this._localSate.getAthlete();
    }
    ngOnInit(): void {
        if(sessionStorage.getItem("token")==null){
            console.log("at ngoninit not log in");
            this._router.navigate(["/home"]);
        }
    }
    EditUser(){
        console.log(this.athlete);
        this._api.editUserResult(this.athlete).subscribe((data)=>{
            this._router.navigate(["/detail"]);
        });
    }
}