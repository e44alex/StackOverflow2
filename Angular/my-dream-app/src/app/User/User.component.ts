import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { LoginPartialComponent } from '../login-partial/login-partial.component';
import { DataServiceService } from '../Shared/data-service.service';
import { Encryption } from '../Shared/Encryption';
import { User } from '../Shared/Model';

@Component({
  selector: 'app-User',
  templateUrl: './User.component.html',
  styleUrls: ['./User.component.scss'],
})
export class UserComponent implements OnInit {
  user: User;
  isOwner: boolean;
  constructor(
    private dataService: DataServiceService,
    private route: ActivatedRoute,
    private router: Router,
    private cookieService: CookieService
  ) {}

  ngOnInit() {
    this.route.params.forEach((param: Params) => {
      if (param['id'] !== undefined) {
        this.dataService
          .getUser(param['id'])
          .then((user) => {
            this.user = user

            if (user.email == this.getEmail) {
              this.isOwner = true;
            }
            else{
              this.isOwner = false;
              console.log(this.user.id)
              console.log(this.getId)
            }

          });
      }
    });

    

    console.log(this.isOwner)
    
  }

  OnSaveButton() {
    this.dataService.updateUser(
      this.user,
      Encryption.Decrypt(this.cookieService.get('token'))
    );
  }

  OnResetButton() {
    this.router
      .navigate(['/'])
      .then(() => this.router.navigate(['/User', this.user.id]));
  }

  get getId(){
    return LoginPartialComponent.id;
  }

  get getAuth(){
    return LoginPartialComponent.authenticated;
  }

  get getEmail(){
    return LoginPartialComponent.email;
  }
}
