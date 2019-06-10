import { Component } from '@angular/core';
import { Router } from "@angular/router";
import { OnInit } from "@angular/core";
import { RestApiService } from '../../Services/rest.service';
import { LogInModel } from '../../Models/logIn.model';
import { Test } from '../../Models/test.model';
import { ResponseBoolean } from '../../Models/responseBoolean.model';
import { Athlete } from '../../Models/athlete.model';
import { User } from '../../Models/user.model';
import { Store } from '@ngrx/store';
import { AppState } from '../../app.state';
import { Observable } from 'rxjs';

@Component({
    selector: 'app-AddAthlete',
    templateUrl: './addAthlete.component.html'
})
export class AddAthleteComponent implements OnInit {
    public userList: User[];
    athlete: Athlete;
    public testOb: Observable<Test>;
    constructor(private _api: RestApiService, private _router: Router, private _Sate: Store<AppState>) {
        this.athlete = new Athlete();
        this.testOb = this._Sate.select('selectedTest');
    }
    ngOnInit(): void {
        if (sessionStorage.getItem("token") == null) {
            console.log("at ngoninit not log in");
            this._router.navigate(["/home"]);
        }
        this._api.getUsersList().subscribe((data: User[]) => {
            this.userList = data;
        });
    }
    AddAthlete() {
        var a = this.testOb.subscribe((data) => {
            this.athlete.testId = data.id;
            this._api.createAthlete(this.athlete).subscribe((data) => {
                a.unsubscribe();
                this._router.navigate(["/detail"]);
            });
        });
    }
}