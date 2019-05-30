import { Component } from '@angular/core';
import { Router } from "@angular/router";
import { OnInit } from "@angular/core";
import { RestApiService } from '../../Services/rest.service';
import { LocalSateService } from '../../Services/localSatet.service';
import { LogInModel } from '../../Models/logIn.model';
import { Test } from '../../Models/test.model';
import { ResponseBoolean } from '../../Models/responseBoolean.model';

@Component({
  selector: 'app-EditTest',
  templateUrl: './editTest.component.html'
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