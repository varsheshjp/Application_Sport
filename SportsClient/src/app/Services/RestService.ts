import { Injectable } from "@angular/core";
import { Observable, throwError } from "rxjs";
import { LogInModel } from '../Models/LogIn';
import { ResponseToken } from '../Models/ResponseToken';
import { Test } from '../Models/Test';
import { catchError } from 'rxjs/operators';
import { Athlete } from '../Models/Athlete';
import { ResponseBoolean } from '../Models/ResponseBoolean';
import { User } from '../Models/User';
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
        return this.http.get<Test[]>(endpoint + "Test/getTestList", { headers: new HttpHeaders({ 'Authorization': 'Bearer ' + sessionStorage.getItem("token") })}).pipe(
            catchError(err => {
                sessionStorage.removeItem("token");
                return throwError("Error thrown from catchError");
            })
        );
    }
    deleteTest(test: Test): Observable<ResponseBoolean> {
        return this.http.post<ResponseBoolean>(endpoint + "Test/deleteTest", test, { headers: new HttpHeaders({ 'Authorization': 'Bearer ' + sessionStorage.getItem("token"), 'Content-Type': 'application/json' }) }).pipe(
            catchError(err => {
                sessionStorage.removeItem("token");
                return throwError("Error thrown from catchError");
            })
        );
    }
    createTest(test:Test):Observable<ResponseBoolean>{
        return this.http.post<ResponseBoolean>(endpoint + "Test/createTest",{Type:test.type,Date:test.date,Number:test.number},{ headers: new HttpHeaders({ 'Authorization': 'Bearer ' + sessionStorage.getItem("token")})}).pipe(
            catchError(err => {
                sessionStorage.removeItem("token");
                return throwError("Error thrown from catchError");
            })
        );
    }

    getAthleteList(test: Test): Observable<Athlete[]> {
        return this.http.get<Athlete[]>(endpoint + "Test/getAthleteByTest/" + test.id,{ headers: new HttpHeaders({ 'Authorization': 'Bearer ' + sessionStorage.getItem("token")})}).pipe(
            catchError(err => {
                sessionStorage.removeItem("token");
                return throwError("Error thrown from catchError");
            })
        );
    }
    deleteAthlete(athlete:Athlete):Observable<ResponseBoolean>{
        return this.http.post<ResponseBoolean>(endpoint + "Test/deleteAthlete",{Id:athlete.id,TestId:athlete.testId},{ headers: new HttpHeaders({ 'Authorization': 'Bearer ' + sessionStorage.getItem("token")})}).pipe(
            catchError(err => {
                sessionStorage.removeItem("token");
                return throwError("Error thrown from catchError");
            })
        );
    }
    createAthlete(athlete:Athlete):Observable<ResponseBoolean>{
        return this.http.post<ResponseBoolean>(endpoint + "Test/addAthlete",{TestId:athlete.testId,UserId:athlete.userId,Result:athlete.result},{ headers: new HttpHeaders({ 'Authorization': 'Bearer ' + sessionStorage.getItem("token")})}).pipe(
            catchError(err => {
                sessionStorage.removeItem("token");
                return throwError("Error thrown from catchError");
            })
        );
    }
    
    getUsersList():Observable<User[]>{
        return this.http.get<User[]>(endpoint+"Test/getAthleteList",{ headers: new HttpHeaders({ 'Authorization': 'Bearer ' + sessionStorage.getItem("token")})}).pipe(
            catchError(err => {
                sessionStorage.removeItem("token");
                return throwError("Error thrown from catchError");
            })
        );
    }
    deleteUser(user:User):Observable<ResponseBoolean>{
        return this.http.get<ResponseBoolean>(endpoint + "Test/deleteAthleteUser/"+user.id,{ headers: new HttpHeaders({ 'Authorization': 'Bearer ' + sessionStorage.getItem("token")})}).pipe(
            catchError(err => {
                sessionStorage.removeItem("token");
                return throwError("Error thrown from catchError");
            })
        );
    }
    createUser(user:User):Observable<ResponseBoolean>{
        return this.http.post<ResponseBoolean>(endpoint + "Test/CreateUser",{Name:user.name,Type:"Athlete"},{ headers: new HttpHeaders({ 'Authorization': 'Bearer ' + sessionStorage.getItem("token")})}).pipe(
            catchError(err => {
                sessionStorage.removeItem("token");
                return throwError("Error thrown from catchError");
            })
        );
    }
}