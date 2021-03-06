import { Component, OnInit } from '@angular/core';
import { Blog } from 'src/app/_models/Blog';
import { BlogService } from 'src/app/_services/blog.service';

@Component({
  selector: 'app-draft',
  templateUrl: './draft.component.html',
  styleUrls: ['./draft.component.css']
})
export class DraftComponent implements OnInit {
  blogs:Blog[];
  constructor(private blogService:BlogService) { }

  ngOnInit() {
    this.blogService.getAllDrafts().subscribe(
      (returnedBlogs)=>{
        this.blogs=returnedBlogs;
      }
    )
  }

}
