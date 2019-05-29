import { Component } from '@angular/core';
import { Router } from "@angular/router";
import { OnInit } from "@angular/core";
import { RestApiService } from '../../Services/RestService';
import { LogInModel } from '../../Models/LogIn';
import { Test } from '../../Models/Test';
import { LocalSateService } from '../../Services/LocalSatetService';
import { ResponseBoolean } from '../../Models/ResponseBoolean';
import { User } from '../../Models/User';
import { Athlete } from '../../Models/Athlete';

@Component({
  selector: 'app-EditTest',
  templateUrl: './EditTest.component.html'
})
export class EditTestComponent implements OnInit{
    public test:Test;
    constructor(private _api:RestApiService,private _router: Router,private _localSate:LocalSateService){
        this.test=this._localSate.getTest();
    }
    ngOnInit(): void {
        if(sessionStorage.getItem("token")==null){
            console.log("at ngoninit not log in");
            this._router.navigate(["/home"]);
        }
    }
    EditTest(){
        console.log(this.test);
        this._api.editTest(this.test).subscribe((data)=>{
            this._router.navigate(["/Dashboard"]);
        });
    }
}