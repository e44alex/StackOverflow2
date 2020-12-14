import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { LoginPartialComponent } from '../login-partial/login-partial.component';
import { AuthServiceService } from '../Shared/auth-service.service';
import { Encryption } from '../Shared/Encryption';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css'],
})
export class LoginFormComponent implements OnInit {
  username: string;
  password: string;

  constructor(
    private loginService: AuthServiceService,
    private cookieService: CookieService,
    private router: Router
  ) {}

  ngOnInit(): void {}

  OnLogin(loginForm: NgForm) {
    this.loginService
      .authenticate(this.username, this.password)
      .then((token: string) => {
        token = Encryption.Encrypt(token);
        this.cookieService.set('token', token);
        this.cookieService.set('username', this.username);
      });
    console.log(loginForm);
    LoginPartialComponent.authenticated = true;
    LoginPartialComponent.email = this.username;
    this.loginService.getId(this.username).then((value: string) => {
      LoginPartialComponent.id = value;
    });
    this.router.navigate(['/']);
  }
}
