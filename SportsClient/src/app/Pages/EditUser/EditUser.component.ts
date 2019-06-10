import { Component } from '@angular/core';
import { Router } from "@angular/router";
import { OnInit } from "@angular/core";
import { RestApiService } from '../../Services/rest.service';
import { LogInModel } from '../../Models/logIn.model';
import { Test } from '../../Models/test.model';
import { ResponseBoolean } from '../../Models/responseBoolean.model';
import { Athlete } from '../../Models/athlete.model';
import { Observable } from 'rxjs';
import { Store } from '@ngrx/store';
import { AppState } from '../../app.state';
@Component({
  selector: 'app-EditUser',
  templateUrl: './editUser.component.html'
})
export class EditUserComponent implements OnInit{
    public athlete:Athlete;
    public athleteob:Observable<Athlete>;
    constructor(private _api:RestApiService,private _router: Router,private _State:Store<AppState>){
        this.athleteob=this._State.select('selectedAthlete');
    }
    ngOnInit(): void {
        if(sessionStorage.getItem("token")==null){
            console.log("at ngoninit not log in");
            this._router.navigate(["/home"]);
        }
        var sub=this.athleteob.subscribe((data)=>{this.athlete=data
            sub.unsubscribe();
        });
    }
    EditUser(){
        console.log(this.athlete);
        this._api.editUserResult(this.athlete).subscribe((data)=>{
            this._router.navigate(["/detail"]);
        });
    }
}