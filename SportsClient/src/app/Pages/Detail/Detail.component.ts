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
    selector: 'app-Detail',
    templateUrl: './detail.component.html'
})
export class DetailComponent implements OnInit {
    test: Test;
    athleteList: Athlete[] = [];
    constructor(private _api: RestApiService, private _router: Router, private _localState: LocalSateService) { }
    ngOnInit(): void {
        if (sessionStorage.getItem("token") == null) {
            console.log("at ng on init not log in");
            this._router.navigate(["/home"]);
        }
        else {
            this.test = this._localState.getTest();
            this._api.getAthleteList(this.test).subscribe((data: Athlete[]) => {
                this.athleteList = data;
                this._api.getUsersList().subscribe((data2) => {
                    for (let j in this.athleteList) {
                        for (let i in data2) {
                            if (this.athleteList[j].userId == data2[i].id) {
                                this.athleteList[j].name = data2[i].name;
                            }
                        }
                    }
                })
            });
        }
    }
    Delete(athlete: Athlete) {

        this._api.deleteAthlete(athlete).subscribe((data) => {
            this.athleteList = this.athleteList.filter(item => item !== athlete);
        });
    }
    Add() {
        this._router.navigate(["/addAthlete"]);
    }
    Edit(athlete: Athlete) {
        this._localState.setAthlete(athlete);
        this._router.navigate(["/editResult"]);
    }

}