import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { AuthServiceService } from '../Shared/auth-service.service';

@Component({
  selector: 'app-login-partial',
  templateUrl: './login-partial.component.html',
  styleUrls: ['./login-partial.component.css']
})
export class LoginPartialComponent implements OnInit {

  static email:string;
  static id:string;
  static authenticated:boolean;

    constructor(private cookieService: CookieService,
    private authService: AuthServiceService,
    private router: Router) { }

  ngOnInit(): void {

    LoginPartialComponent.authenticated = (this.cookieService.check('token') && this.cookieService.check('username'));
    LoginPartialComponent.email = this.cookieService.get('username')
    
    this.authService.getId(LoginPartialComponent.email).then((value:string)=>{
      LoginPartialComponent.id = value
    })
    console.log("login partial init");
    
  }

  OnLogout(){
    LoginPartialComponent.authenticated =false;
    LoginPartialComponent.email = "";
    LoginPartialComponent.id = "";
    this.cookieService.delete('token');
    this.cookieService.delete('username');
    this.router.navigate(['/']);
  }

  get getAuth(){
    return LoginPartialComponent.authenticated;
  }

  get getEmail(){
    return LoginPartialComponent.email;
  }
  get getID(){
    return LoginPartialComponent.id;
  }

}
