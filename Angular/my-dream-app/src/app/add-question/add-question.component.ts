import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Guid } from 'guid-typescript';
import { CookieService } from 'ngx-cookie-service';
import { LoginPartialComponent } from '../login-partial/login-partial.component';
import { AuthServiceService } from '../Shared/auth-service.service';
import { DataServiceService } from '../Shared/data-service.service';
import { Encryption } from '../Shared/Encryption';
import { Question, User } from '../Shared/Model';

@Component({
  selector: 'app-add-question',
  templateUrl: './add-question.component.html',
  styleUrls: ['./add-question.component.css'],
})
export class AddQuestionComponent implements OnInit {
  questionTopic: string;
  questionBody: string;

  constructor(
    private apiClient: DataServiceService,
    private authService: AuthServiceService,
    private cookieService: CookieService,
    private router: Router
  ) {}

  ngOnInit(): void {}

  SendQuestion() {
    var question = new Question();
    question.id = Guid.create().toString();
    question.topic = this.questionTopic;
    question.body = this.questionBody;
    
    question.creator = new User();
    question.creator.id = LoginPartialComponent.id;

    this.apiClient.sendQuestion(question, Encryption.Decrypt(this.cookieService.get('token')));
  
    setTimeout(()=>{}, 150);

    this.router.navigate(['/Question', question.id]);

  }
}
