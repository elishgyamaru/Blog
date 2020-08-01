import { Injectable } from '@angular/core';
import { Subject, Observable } from 'rxjs';
import { BlogPreview } from '../_models/Blog-Preview';
import { Blog } from '../_models/Blog';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BlogService {
previewBlog= new Subject<BlogPreview>();
onPreviewBlogChange=this.previewBlog.asObservable();
baseUrl:string=environment.apiUrl+'blog/';

constructor(private http:HttpClient
                      
  ) { }

createNewBlog(blog:Blog, isDraft=false){
  blog.isDraft=isDraft;
  return this.http.post(this.baseUrl,blog);
}
getAllBlog():Observable<Blog[]>{
  return this.http.get<Blog[]>(this.baseUrl);
}
getAlPublishedBlogs():Observable<Blog[]>{
  return this.http.get<Blog[]>(this.baseUrl+"published");
}
getAllDrafts():Observable<Blog[]>{
  return this.http.get<Blog[]>(this.baseUrl+"drafts");
}
getBlog(id:string){
  return this.http.get(this.baseUrl+id);
}
}
