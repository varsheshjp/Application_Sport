import { Component } from '@angular/core';
import { Router } from "@angular/router";
import { OnInit } from "@angular/core";
import { LogInModel } from '../../Models/logIn.model';
import { Test } from '../../Models/test.model';

import { RestApiService } from '../../Services/rest.service';
import { LocalSateService } from '../../Services/localSatet.service';
import { ResponseBoolean } from '../../Models/responseBoolean.model';

@Component({
  selector: 'app-Dashboard',
  templateUrl: './dashboard.component.html'
})
export class DashboardComponent implements OnInit{
    public testList:Test[]=[];
    constructor(private _api:RestApiService,private _router: Router,private _localSate:LocalSateService){
    }
    ngOnInit(): void {
        if(sessionStorage.getItem("token")==null){
            console.log("at ngoninit not log in");
            this._router.navigate(["/home"]);
        }
        else{
        this._api.getTestList().subscribe((data:Test[])=>{
            console.log(data);
            this.testList=data;
        });}
    }
    DeleteTest(test:Test){
        this._api.deleteTest(test).subscribe((data:ResponseBoolean)=>{
            console.log(data.responseData);
            this._router.navigate(["/home"]);
        });
        
    }
    ViewTest(test:Test){
        this._localSate.setTest(test);
        this._router.navigate(["/detail"]);
    }
    EditTest(test:Test){
        this._localSate.setTest(test);
        this._router.navigate(["/editTest"]);
    }
    Logout(){
        sessionStorage.removeItem("token");
        this._router.navigate(["/home"]);
    }
    CreateTest(){
        this._router.navigate(["/createTest"]);
    }
}