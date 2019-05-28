import { Component } from '@angular/core';
import { Router } from "@angular/router";
import { OnInit } from "@angular/core";
import { RestApiService } from '../../Services/RestService';
import { LogInModel } from '../../Models/LogIn';
import { Test } from '../../Models/Test';
import { LocalSateService } from '../../Services/LocalSatetService';
import { ResponseBoolean } from '../../Models/ResponseBoolean';
import { Athlete } from '../../Models/Athlete';
import { User } from '../../Models/User';

@Component({
  selector: 'app-AddAthlete',
  templateUrl: './AddAthlete.component.html'
})
export class AddAthleteComponent implements OnInit{
    public userList:User[];
    athlete:Athlete;
    constructor(private _api:RestApiService,private _router: Router,private _localSate:LocalSateService){
        this.athlete=new Athlete();
    }
    ngOnInit(): void {
        if(sessionStorage.getItem("token")==null){
            console.log("at ngoninit not log in");
            this._router.navigate(["/home"]);
        }
        this._api.getUsersList().subscribe((data:User[])=>{
            this.userList=data;
        });
    }
    AddAthlete(){
        console.log(this._localSate.getTest().id);
        this.athlete.testId=this._localSate.getTest().id;
        this._api.createAthlete(this.athlete).subscribe((data)=>{
            this._router.navigate(["/detail"]);
        });
    }
}