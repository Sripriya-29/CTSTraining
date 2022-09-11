import { Component, OnInit } from '@angular/core';
import { Login } from '../login';
import { UserModel } from '../users.component';
import { BooksService } from '../bookservice.component';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  users:UserModel[]=[];
  user:UserModel={
    userId:0,
    userName:'',
    emailId:'',
    password:'',
    roleId:'',
    firstName:'',
    lastName:'',
    active:true
  }



 uservalidations:Login[]=[];
  uservalidation:Login={
    username:'',
    password:''
  }
  constructor(private service:BooksService) { }





ngOnInit(): void {
  }
  onSubmit(){
    console.log(this.uservalidation.username);
    this.service.ValidateUsers(this.uservalidation).subscribe()
  }
}