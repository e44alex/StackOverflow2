import { Component } from '@angular/core';
import { LoginPartialComponent } from './login-partial/login-partial.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'my-dream-app';

  searchText: string;

  get getAuth(){
    return LoginPartialComponent.authenticated
  }

}
