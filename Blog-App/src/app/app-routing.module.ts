import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { MenuComponent } from './menu/menu.component';
import { BloggerMenuComponent } from './blogger-menu/blogger-menu.component';
import { PreventUnsavedChangesGuard } from './_guards/prevent-unsaved-changes-guard';
import { ProfileComponent } from './profile/profile.component';
import { PreviewComponent } from './preview/preview.component';
import { NewBlogComponent } from './blogger-menu/new-blog/new-blog.component';
import { DraftComponent } from './blogger-menu/draft/draft.component';
import { PublishedComponent } from './blogger-menu/published/published.component';
import { BlogDetailComponent } from './blog-detail/blog-detail.component';
import { BlogDetailResolver } from './_resolvers/blog-detail.resolver';


const routes:Routes=[
  {path:"",component:HomeComponent,pathMatch:"full"},
  {path:"login",component:LoginComponent},
  {path:"register",component:RegisterComponent},
  {path:"preview",component:PreviewComponent},
  {path:"dashboard",redirectTo:"dashboard/blog/new"},
  {path:"dashboard",
  component:MenuComponent,
  children:[
    { path:"blog",
      component:BloggerMenuComponent,
      children:[
        { path:"",redirectTo:"dashboard/blog/new",pathMatch:"full"},
        { path:"new",component:NewBlogComponent,canDeactivate:[PreventUnsavedChangesGuard]},
        { path:"draft",component:DraftComponent},
        { path:"published",component:PublishedComponent}
      ]
    },
    {path:"admin",component:BloggerMenuComponent},
    {path:"profile",component:ProfileComponent}
  ]},
  {path:"blog-detail/:id",component:BlogDetailComponent,resolve:{'blog':BlogDetailResolver}}
]

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports:[RouterModule]
})
export class AppRoutingModule { }
