import { Injectable } from '@angular/core';
import { CanDeactivate, UrlTree, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';

export interface CanDeactivateBase{
  CanDeactivate(): Observable<boolean|UrlTree>|Promise<boolean|UrlTree>|boolean|UrlTree
}

@Injectable({
  providedIn: 'root'
})
export class PreventUnsavedChangesGuard implements CanDeactivate<CanDeactivateBase> {

constructor() { }
  canDeactivate(component: CanDeactivateBase, currentRoute: ActivatedRouteSnapshot, currentState: RouterStateSnapshot, nextState?: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
    return component.CanDeactivate();
  }

}
