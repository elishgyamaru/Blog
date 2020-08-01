import { Component, OnInit } from '@angular/core';
import { Blog } from 'src/app/_models/Blog';
import { BlogService } from 'src/app/_services/blog.service';

@Component({
  selector: 'app-published-blogs',
  templateUrl: './published-blogs.component.html',
  styleUrls: ['./published-blogs.component.css']
})
export class PublishedBlogsComponent implements OnInit {
  blogs:Blog[];
  constructor(private blogService:BlogService) { }

  ngOnInit() {
    this.blogService.getAlPublishedBlogs().subscribe(
      (returnedBlogs)=>{
        this.blogs=returnedBlogs;
      }
    )
  }

}
