import { Injectable } from "@angular/core";
import { Login } from '../_models/login';
import { HttpClient, HttpHeaders} from '@angular/common/http'
import {map} from 'rxjs/operators';
import { JwtHelperService } from "@auth0/angular-jwt";

import {environment} from '../../environments/environment';
import { Register } from '../_models/Register';
import { BehaviorSubject } from 'rxjs';
import { User } from '../_models/user';
import { Router } from '@angular/router';

export const SkipAuthHeaders="SkipAuthInterceptor";
const headers=new HttpHeaders().set(SkipAuthHeaders,'skip');

@Injectable({
    providedIn:"root"
})
export class AuthService{
    baseUrl:string=environment.apiUrl+"account/";
    currentUserSubject= new BehaviorSubject<User>(this.getParsedUser());
    onCurrentUserChange=this.currentUserSubject.asObservable();
    private isLoggedInSubject=new BehaviorSubject<boolean>(this.isLoggedIn());
    onLoginStatusChange=this.isLoggedInSubject.asObservable();
    constructor(
        private http:HttpClient,
        private router:Router
    ){

    }
    login(loginData:Login){
        return this.http.post<User>(this.baseUrl+"login",loginData,{headers})
            .pipe(map((result:any)=>{
                localStorage.setItem("token",result.token);
                localStorage.setItem("user",JSON.stringify(result.user))
                this.currentUserSubject.next(this.getParsedUser());
                this.isLoggedInSubject.next(true);
                return result.user;
            }));

    }
    logout(){
        localStorage.removeItem("token");
        localStorage.removeItem("user");
        this.isLoggedInSubject.next(false);
        this.router.navigate(['/']);
    }
    register(registrationData:Register){
        return this.http.post(this.baseUrl+'register',registrationData,{headers});
    }
    getToken(){
        return localStorage.getItem("token");
    }
    getParsedUser(){
        return JSON.parse(localStorage.getItem('user'));
    }
    isTokenExpired():boolean {
        const helper = new JwtHelperService();
        return helper.isTokenExpired(this.getToken());
    }
    isLoggedIn(){
        return !this.isTokenExpired();
    }
}