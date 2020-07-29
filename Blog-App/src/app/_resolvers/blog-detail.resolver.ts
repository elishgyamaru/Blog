import { Injectable } from '@angular/core';
import { BlogService } from '../_services/blog.service';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Blog } from '../_models/Blog';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class BlogDetailResolver implements Resolve<Blog>{

constructor(private blogService:BlogService) { }
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Blog | Observable<Blog> | Promise<Blog> {
    return this.blogService.getBlog(route.params['id']).pipe(catchError(err=>{
      console.log(err);
      return of(null);
    }));
  }

}
