import { Component, OnInit } from '@angular/core';
import { BlogService } from '../_services/blog.service';
import { BlogPreview } from '../_models/Blog-Preview';

@Component({
  selector: 'app-preview',
  templateUrl: './preview.component.html',
  styleUrls: ['./preview.component.css']
})
export class PreviewComponent implements OnInit {

  constructor(private blogService:BlogService) { }
  blog:BlogPreview;
  ngOnInit() {
    this.blogService.onPreviewBlogChange.subscribe((blog)=>{
      this.blog=blog;
    });
    console.log(this.blog)
  }

}
