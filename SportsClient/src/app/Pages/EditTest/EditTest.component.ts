import { Component } from '@angular/core';
import { Router } from "@angular/router";
import { OnInit } from "@angular/core";
import { RestApiService } from '../../Services/rest.service';
import { LogInModel } from '../../Models/logIn.model';
import { Test } from '../../Models/test.model';
import { ResponseBoolean } from '../../Models/responseBoolean.model';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { AppState } from '../../app.state';

@Component({
  selector: 'app-EditTest',
  templateUrl: './editTest.component.html'
})
export class EditTestComponent implements OnInit{
    public test:Test;
    public testob:Observable<Test>;
    constructor(private _api:RestApiService,private _router: Router,private _Sate:Store<AppState>){
        this.testob=this._Sate.select('selectedTest');
        var sub=this.testob.subscribe((data)=>{
            this.test=data;
        });
        sub.unsubscribe();
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