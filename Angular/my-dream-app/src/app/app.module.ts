import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http'
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RouterModule } from '@angular/router';
import { QuestionComponent } from './Question/Question.component';
import { UserComponent } from './User/User.component';
import { HomeComponent } from './Home/Home.component';
import { FormsModule } from '@angular/forms';
import { LoginPartialComponent } from './login-partial/login-partial.component';
import { RegisterFormComponent } from './register-form/register-form.component';
import { LoginFormComponent } from './login-form/login-form.component';
import { CookieService } from 'ngx-cookie-service';
import { AddQuestionComponent } from './add-question/add-question.component';

@NgModule({
  declarations: [AppComponent, HomeComponent, QuestionComponent, UserComponent, LoginPartialComponent, RegisterFormComponent, LoginFormComponent, AddQuestionComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    NgbModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent },
      { path: 'Home/:searchText', component: HomeComponent },
      { path: 'Question/:id', component: QuestionComponent },
      { path: 'User/:id', component: UserComponent },
      { path: 'Login', component: LoginFormComponent },
      { path: 'Register', component: RegisterFormComponent },
      { path: 'AskQuestion', component: AddQuestionComponent },
    ]),
  ],
  providers: [CookieService],

  bootstrap: [AppComponent],
})
export class AppModule {}
