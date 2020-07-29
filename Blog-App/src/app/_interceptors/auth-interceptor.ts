import { Injectable } from '@angular/core';
import { HttpInterceptor,  HttpHandler, HttpEvent, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService, SkipAuthHeaders } from '../_services/auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor{
    constructor(private authService:AuthService){}
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if(!req.headers.has(SkipAuthHeaders)){
            req=req.clone({
                headers:req.headers.set("Authorization", 'Bearer '+this.authService.getToken())
            });
        }
        req.headers.delete(SkipAuthHeaders);
        return next.handle(req);
    }
}
