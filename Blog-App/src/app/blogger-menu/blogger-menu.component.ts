import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-blogger-menu',
  templateUrl: './blogger-menu.component.html',
  styleUrls: ['./blogger-menu.component.css']
})
export class BloggerMenuComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }
  links = ['New', 'Draft', 'Published'];
  activeLink = this.links[0];
  background = '';
}
