import { Component } from '@angular/core';
import { Router } from "@angular/router";
import { OnInit } from "@angular/core";
import { RestApiService } from '../../Services/RestService';
import { LogInModel } from '../../Models/LogIn';
import { Test } from '../../Models/Test';
import { LocalSateService } from '../../Services/LocalSatetService';
import { ResponseBoolean } from '../../Models/ResponseBoolean';

@Component({
  selector: 'app-CreateTest',
  templateUrl: './CreateTest.component.html'
})
export class CreateTestComponent implements OnInit{
    public test:Test;
    constructor(private _api:RestApiService,private _router: Router,private _localSate:LocalSateService){
        this.test=new Test();
    }
    ngOnInit(): void {
        if(sessionStorage.getItem("token")==null){
            console.log("at ngoninit not log in");
            this._router.navigate(["/home"]);
        }
    }
    CreateTest(){
        this.test.number=0;
        this._api.createTest(this.test).subscribe((data:ResponseBoolean)=>{
            this._router.navigate(["/Dashboard"]);
        });
    }
    
}