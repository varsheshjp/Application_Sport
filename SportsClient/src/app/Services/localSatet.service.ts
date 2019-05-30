import { Injectable } from "@angular/core";
import { Athlete } from '../Models/athlete.model';
import { Test } from '../Models/test.model';

@Injectable({
    providedIn: 'root'
})
export class LocalSateService{
    public setTest(test:Test){
        sessionStorage.setItem("test",JSON.stringify(test));
    }
    public getTest():Test{
        return JSON.parse(sessionStorage.getItem("test"));
    }
    public setAthlete(athlete:Athlete){
        sessionStorage.setItem("athlete",JSON.stringify(athlete));
    }
    public getAthlete():Athlete{
        return JSON.parse(sessionStorage.getItem("athlete"));
    }
}