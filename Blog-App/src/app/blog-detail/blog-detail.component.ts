import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Blog } from '../_models/Blog';

@Component({
  selector: 'app-blog-detail',
  templateUrl: './blog-detail.component.html',
  styleUrls: ['./blog-detail.component.css']
})
export class BlogDetailComponent implements OnInit {
  blog:Blog;
  constructor(private route:ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe((data)=>{
      this.blog=data['blog'];
      
    });
    console.log(this.route)
    console.log(this.blog)
  }

}
