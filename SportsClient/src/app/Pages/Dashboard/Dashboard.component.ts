import { Component } from '@angular/core';
import { Router } from "@angular/router";
import { OnInit } from "@angular/core";
import { LogInModel } from '../../Models/logIn.model';
import { Test } from '../../Models/test.model';

import { RestApiService } from '../../Services/rest.service';
import { ResponseBoolean } from '../../Models/responseBoolean.model';
import { Store } from "@ngrx/store";
import { AppState } from '../../app.state';
import { Observable, BehaviorSubject } from 'rxjs';
import * as TestActions from '../../actions/test.action';
import * as SelectedTestActions from '../../actions/selectedTest.action';
import { element } from 'protractor';
import { async } from "rxjs/internal/scheduler/async";
@Component({
    selector: 'app-Dashboard',
    templateUrl: './dashboard.component.html'
})
export class DashboardComponent implements OnInit {
    public input:BehaviorSubject<Test[]>;
    public testList: Observable<Test[]>;
    constructor(private _store: Store<AppState>, private _api: RestApiService, private _router: Router) {
        this.testList=this._store.select("test");
        if (sessionStorage.getItem("token") == null) {
            console.log("at ngoninit not log in");
            this._router.navigate(["/home"]);
        }
       
    }
    ngOnInit(): void {
         this._api.getTestList().subscribe((elements: Test[]) => {
            this._store.dispatch(new TestActions.AddTest(elements));
        });
    }
    DeleteTest(test: Test, id: number) {
        this._api.deleteTest(test).subscribe((data: ResponseBoolean) => {
            this._store.dispatch(new TestActions.RemoveTest(id));
        });
    }
    ViewTest(test: Test) {
        console.log(Test);
        this._store.dispatch(new SelectedTestActions.AddSelectedTest(test));
        this._router.navigate(["/detail"]);
    }
    EditTest(test: Test) {
        console.log(Test);
        this._store.dispatch(new SelectedTestActions.AddSelectedTest(test));
        this._router.navigate(["/editTest"]);
    }
    Logout() {
        sessionStorage.removeItem("token");
        this._router.navigate(["/home"]);
    }
    CreateTest() {
        this._router.navigate(["/createTest"]);
    }
    include(elements: Test[], test: Test) {
        for (let test_state of elements) {
            if (test_state.id == test.id) {
                return true;
            }
        }
        return false;
    }
}