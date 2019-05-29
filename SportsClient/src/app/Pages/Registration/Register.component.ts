import { Component } from '@angular/core';
import { Router } from "@angular/router";
import { OnInit } from "@angular/core";
import { RestApiService } from '../../Services/RestService';
import { LogInModel } from '../../Models/LogIn';
import { Test } from '../../Models/Test';
import { LocalSateService } from '../../Services/LocalSatetService';
import { Athlete } from '../../Models/Athlete';
import { Register } from '../../Models/Register';

@Component({
    selector: 'app-Register',
    templateUrl: './Register.component.html'
})
export class RegisterComponent implements OnInit {
    public register: Register;
    constructor(private _api: RestApiService, private _router: Router, private _localState: LocalSateService) {
        this.register = new Register();
    }
    ngOnInit(): void {
        
    }
    Register() {
        this._api.register(this.register).subscribe((data) => {
            if (data.loginResult == "fail") {
                sessionStorage.setItem("token", null);
                console.log("fail");
            }
            else if (data.loginResult == "success") {
                sessionStorage.setItem("token", data.token);
                console.log(data.token);
                this._router.navigate(['/Dashboard']);
            }
        });
    }
}