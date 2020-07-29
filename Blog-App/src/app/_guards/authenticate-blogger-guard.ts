import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { User } from '../_models/user';

@Injectable({
    providedIn:'root'
})
export class AuthenticateBloggerGuard implements CanActivate{
    constructor(private authService:AuthService){

    }
    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | import("@angular/router").UrlTree | import("rxjs").Observable<boolean | import("@angular/router").UrlTree> | Promise<boolean | import("@angular/router").UrlTree> {
        this.authService.onCurrentUserChange.subscribe((newUser)=>{
            // return newUser.roles.fi
            return true;
        })
        return true
    }
}
