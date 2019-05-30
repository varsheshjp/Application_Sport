import { Injectable } from "@angular/core";
import { Observable, throwError } from "rxjs";
import { LogInModel } from '../Models/logIn.model';
import { ResponseToken } from '../Models/responseToken.model';
import { Test } from '../Models/test.model';
import { ResponseBoolean } from '../Models/responseBoolean.model';
import { Athlete } from '../Models/athlete.model';
import { User } from '../Models/user.model';
import { Register } from '../Models/register.model';

import {
    HttpEvent,
    HttpInterceptor,
    HttpHandler,
    HttpRequest, HttpClient, HttpHeaders
} from '@angular/common/http';
const endpoint = 'https://localhost:5001/api/';
const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' })
};
@Injectable({
    providedIn: 'root'
})
export class RestApiService {
    public token: string;
    constructor(private http: HttpClient) {
    }
    postLogin(Data: LogInModel): Observable<ResponseToken> {
        return this.http.post<ResponseToken>(endpoint + "Auth/token", Data, httpOptions);
    }

    getTestList(): Observable<Test[]> {
        return this.http.get<Test[]>(endpoint + "Test/getTestList");
    }
    deleteTest(test: Test): Observable<ResponseBoolean> {
        return this.http.post<ResponseBoolean>(endpoint + "Test/deleteTest", test);
    }
    createTest(test:Test):Observable<ResponseBoolean>{
        return this.http.post<ResponseBoolean>(endpoint + "Test/createTest",{Type:test.type,Date:test.date,Number:test.number});
    }

    getAthleteList(test: Test): Observable<Athlete[]> {
        return this.http.get<Athlete[]>(endpoint + "Test/getAthleteByTest/" + test.id);
    }
    deleteAthlete(athlete:Athlete):Observable<ResponseBoolean>{
        return this.http.post<ResponseBoolean>(endpoint + "Test/deleteAthlete",{Id:athlete.id,TestId:athlete.testId});
    }
    createAthlete(athlete:Athlete):Observable<ResponseBoolean>{
        return this.http.post<ResponseBoolean>(endpoint + "Test/addAthlete",{TestId:athlete.testId,UserId:athlete.userId,Result:athlete.result});
    }
    getUsersList():Observable<User[]>{
        return this.http.get<User[]>(endpoint+"Test/getAthleteList");
    }
    deleteUser(user:User):Observable<ResponseBoolean>{
        return this.http.get<ResponseBoolean>(endpoint + "Test/deleteAthleteUser/"+user.id);
    }
    createUser(user:User):Observable<ResponseBoolean>{
        return this.http.post<ResponseBoolean>(endpoint + "Test/CreateUser",{Name:user.name,Type:"Athlete"});
    }
    register(register:Register):Observable<ResponseToken>{
        return this.http.post<ResponseToken>(endpoint + "Auth/register", {Username:register.username,Password:register.password,ConfirmPassword:register.confirmPassword});
    }
    editUserResult(athlete:Athlete):Observable<ResponseBoolean>{
        return this.http.post<ResponseBoolean>(endpoint + "Test/editAthlete",{Id:athlete.id,Result:athlete.result});
    }
    editTest(test:Test):Observable<ResponseBoolean>{
        return this.http.post<ResponseBoolean>(endpoint + "Test/editTest",{Id:test.id,Date:test.date,Type:test.type});
    }
}