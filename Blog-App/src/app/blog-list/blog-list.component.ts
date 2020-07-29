import { Component, OnInit } from '@angular/core';
import { Blog } from '../_models/Blog';
import { BlogService } from '../_services/blog.service';

@Component({
  selector: 'app-blog-list',
  templateUrl: './blog-list.component.html',
  styleUrls: ['./blog-list.component.css']
})
export class BlogListComponent implements OnInit {
  blogs:Blog[];
  constructor(private blogService:BlogService) { }

  ngOnInit() {
    this.blogService.getAllBlog().subscribe(
      (returnedBlogs)=>{
        this.blogs=returnedBlogs;
      }
    )
  }

}
