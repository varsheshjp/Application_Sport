import { Component } from '@angular/core';
import { Router } from "@angular/router";
import { OnInit } from "@angular/core";
import { RestApiService } from '../../Services/rest.service';
import { LocalSateService } from '../../Services/localSatet.service';
import { LogInModel } from '../../Models/logIn.model';
import { Test } from '../../Models/test.model';
import { ResponseBoolean } from '../../Models/responseBoolean.model';
import { Athlete } from '../../Models/athlete.model';
import { User } from '../../Models/user.model';

@Component({
  selector: 'app-AddAthlete',
  templateUrl: './addAthlete.component.html'
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