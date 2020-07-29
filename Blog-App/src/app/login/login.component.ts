import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  model:any={};
  constructor(private authService:AuthService,private router:Router) { }

  ngOnInit(): void {
  }
  onLogin(){
    this.authService.login(this.model)
    .subscribe((user)=>{
     this.router.navigate(['/']);
    });
  }
}
