import { Component } from '@angular/core';
import { Router } from "@angular/router";
import { OnInit } from "@angular/core";
import { RestApiService } from '../../Services/rest.service';
import { LogInModel } from '../../Models/logIn.model';
import { Test } from '../../Models/test.model';
import { ResponseBoolean } from '../../Models/responseBoolean.model';

@Component({
  selector: 'app-CreateTest',
  templateUrl: './createTest.component.html'
})
export class CreateTestComponent implements OnInit{
    public test:Test;
    constructor(private _api:RestApiService,private _router: Router){
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