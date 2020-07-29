import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AuthService } from '../_services/auth.service';
import { Router } from '@angular/router';
import { MatchValidator } from '../_validators/MatchValidator';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  form:any={};
  constructor(private fb: FormBuilder,
              private authService:AuthService,
              private router:Router
              ) { }

  ngOnInit(): void {
    this.form= this.fb.group({
      "UserName":[null,Validators.required],
      "Password":[null,Validators.required],
      "ConfirmPassword":[null,Validators.required],
      "Email":[null,[Validators.email,Validators.required]]
    },{
      validators:MatchValidator("Password","ConfirmPassword")
    });
  }

  onRegister(){
    this.authService.register(this.form.value)
      .subscribe(()=>{
        console.log("Registered Succesfuly");
        this.router.navigateByUrl("");
      },
      (error)=>{
        console.log(error);
      })
  }
}
