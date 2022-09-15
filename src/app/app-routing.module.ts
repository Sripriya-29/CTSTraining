import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AvailablebooksComponent } from './availablebooks/availablebooks.component';
import { HomePageComponent } from './home-page/home-page.component';
import { LoginComponent } from './login/login.component';
import { PublishbookComponent } from './publishbook/publishbook.component';
import { SignUpComponent } from './sign-up/sign-up.component';

const routes: Routes = [{path:'home',component:HomePageComponent},
{path:'Register',component:SignUpComponent},
{path:'Logout',component:HomePageComponent},
 {path:'Publish',component:PublishbookComponent},
{path:'Login',component:LoginComponent},
{path:'AvailableBooks',component:AvailablebooksComponent}];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
