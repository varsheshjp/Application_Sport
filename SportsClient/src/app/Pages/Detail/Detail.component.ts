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
import * as AthleteActions from '../../actions/athlete.action';
import * as SelectedAthleteActions from '../../actions/selectedAthlete.action';
@Component({
    selector: 'app-Detail',
    templateUrl: './detail.component.html'
})
export class DetailComponent implements OnInit {
    testob: Observable<Test>;
    test:Test;
    athleteList: Observable<Athlete[]>
    constructor(private _store: Store<AppState>, private _api: RestApiService, private _router: Router, private _State: Store<AppState>) {
        this.athleteList = this._store.select('athlete');
        this.testob = this._State.select('selectedTest');
    }
    ngOnInit(): void {
        if (sessionStorage.getItem("token") == null) {
            console.log("at ng on init not log in");
            this._router.navigate(["/home"]);
        }
        else {

            this.testob.subscribe((data) => {
                this.test=data;
                this._api.getAthleteList(data).subscribe((data: Athlete[]) => {
                    let athleteList: Athlete[] = data;
                    this._api.getUsersList().subscribe((data2) => {
                        for (let j in athleteList) {
                            for (let i in data2) {
                                if (athleteList[j].userId == data2[i].id) {
                                    athleteList[j].name = data2[i].name;
                                }
                            }
                        }
                        this._store.dispatch(new AthleteActions.AddAthlete(athleteList));
                    });
                });
            });

        }
    }
    Delete(athlete: Athlete, id: number) {
        this._api.deleteAthlete(athlete).subscribe((data) => {
            this._store.dispatch(new AthleteActions.RemoveAthlete(id));
        });
    }
    Add() {
        this._router.navigate(["/addAthlete"]);
    }
    Edit(athlete: Athlete) {
        console.log(athlete);
        this._State.dispatch(new SelectedAthleteActions.AddSelectedAthlete(athlete));
        this._router.navigate(["/editResult"]);
    }

}