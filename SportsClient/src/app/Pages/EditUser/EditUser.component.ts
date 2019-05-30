import { Component } from '@angular/core';
import { Router } from "@angular/router";
import { OnInit } from "@angular/core";
import { RestApiService } from '../../Services/rest.service';
import { LocalSateService } from '../../Services/localSatet.service';
import { LogInModel } from '../../Models/logIn.model';
import { Test } from '../../Models/test.model';
import { ResponseBoolean } from '../../Models/responseBoolean.model';
import { Athlete } from '../../Models/athlete.model';
@Component({
  selector: 'app-EditUser',
  templateUrl: './editUser.component.html'
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