import { Component, OnInit, SimpleChanges } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { DataServiceService } from '../Shared/data-service.service';
import { Answer, Question, User } from '../Shared/Model';
import { NgForm } from '@angular/forms';
import { Guid } from 'guid-typescript';
import { LoginPartialComponent } from '../login-partial/login-partial.component';
import { Encryption } from '../Shared/Encryption';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-Question',
  templateUrl: './Question.component.html',
  styleUrls: ['./Question.component.scss'],
})
export class QuestionComponent implements OnInit {
  question: Question;
  answer: Answer;

  constructor(
    private dataService: DataServiceService,
    private route: ActivatedRoute,
    private cookieService: CookieService,
    private router: Router
  ) {}

  
  

  ngOnInit() {
    console.log('question OnInit');
    this.route.params.forEach((param: Params) => {
      if (param['id'] !== undefined) {
        this.dataService
          .getQuestion(param['id'])
          .then((x) => {
            this.question = x;
            this.question.answers.sort( (a, b) => {
              if(a.dateCreated > b.dateCreated){
                return 1;
              }
              if(a.dateCreated < b.dateCreated){
                return -1;
              }
              return 0;
            })
          });
      }
    });
  }

  OnAnswerSubmit(form: NgForm) {
    console.log(form);
    console.log(form.value);
    this.answer = new Answer();
    this.answer.body = form.value.body;
    this.answer.dateCreated = new Date();
    this.answer.id = Guid.create().toString();
    this.answer.question = this.question;
    this.answer.creator = new User();
    this.answer.creator.id = LoginPartialComponent.id;

    this.dataService.sendAnswer(
      this.answer,
      Encryption.Decrypt(this.cookieService.get('token'))
    );

    this.router.navigate(['/']).then(() => {
      this.router.navigate(['/Question', this.question.id]);
    });
  }

  OnLikeButton(id: string) {
    var userId = LoginPartialComponent.id;
    this.question.answers.forEach((ans) => {
      if (ans.id == id) {

        //if there any likers -- check if I already liked it
        if(ans.users.length > 0){
          ans.users.forEach((us) => {
            if (us.id == userId) {
              return;
            }
          });
        }

        this.dataService.likeAnswer(
          ans.id,
          LoginPartialComponent.email,
          Encryption.Decrypt(this.cookieService.get('token'))
        );

        this.router.navigate(['/']).then(() => {
          this.router.navigate(['/Question', this.question.id]);
        });
        return;
      }
    });
  }

  get getAuth() {
    return LoginPartialComponent.authenticated;
  }
}
