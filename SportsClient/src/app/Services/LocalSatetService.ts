import { Injectable } from "@angular/core";
import { Test } from '../Models/Test';
import { Athlete } from '../Models/Athlete';
@Injectable({
    providedIn: 'root'
})
export class LocalSateService{
    public currentTest:Test;
    public currentAthlete:Athlete;
    public setTest(test:Test){
        sessionStorage.setItem("test",JSON.stringify(test));
    }
    public getTest():Test{
        return JSON.parse(sessionStorage.getItem("test"));
    }
}