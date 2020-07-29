import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { User } from '../_models/user';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  user:User;
  isLoggedIn:boolean=false;
  constructor(private authService:AuthService) { }

  ngOnInit(): void {
    this.authService.onCurrentUserChange.subscribe((user)=>{
      this.user=user;
    });
    this.authService.onLoginStatusChange.subscribe((status)=>{
      this.isLoggedIn=status;
    })
  }
  onLogOut(){
    this.authService.logout();
  }
}
