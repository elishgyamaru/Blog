import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http'
import {FormsModule, ReactiveFormsModule} from '@angular/forms';

import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LoginComponent } from './login/login.component';
import { AppRoutingModule } from './app-routing.module';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { NewBlogComponent } from './blogger-menu/new-blog/new-blog.component';

import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { BloggerMenuComponent } from './blogger-menu/blogger-menu.component';
import { MenuComponent } from './menu/menu.component';

import { QuillModule } from 'ngx-quill';
import { PreviewComponent } from './preview/preview.component';
import { ProfileComponent } from './profile/profile.component';
import { AngularMaterialModules } from './angular-material.modules';
import { AuthInterceptor } from './_interceptors/auth-interceptor';
import { BlogListComponent } from './blog-list/blog-list.component';
import { TagListComponent } from './tag-list/tag-list.component';
import { BlogDetailComponent } from './blog-detail/blog-detail.component';
import { BlogItemComponent } from './blog-item/blog-item.component';

@NgModule({
   declarations: [
      AppComponent,
      NavbarComponent,
      LoginComponent,
      HomeComponent,
      RegisterComponent,
      BloggerMenuComponent,
      MenuComponent,
      NewBlogComponent,
      PreviewComponent,
      ProfileComponent,
      BlogListComponent,
      BlogItemComponent,
      TagListComponent,
      BlogDetailComponent,
      BlogItemComponent
   ],
   imports: [
      BrowserModule,
      BrowserAnimationsModule,
      AppRoutingModule,
      HttpClientModule,
      FormsModule,
      ReactiveFormsModule,
      BrowserAnimationsModule,
      BsDropdownModule.forRoot(),
      TabsModule.forRoot(),
      QuillModule.forRoot(),
      AngularMaterialModules
   ],
   providers: [
      {provide:HTTP_INTERCEPTORS, useClass:AuthInterceptor, multi:true}
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
