import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-register-form',
  templateUrl: './register-form.component.html',
  styleUrls: ['./register-form.component.css']
})
export class RegisterFormComponent implements OnInit {

  name:string;
  surname:string;
  password:string;
  email:string;
  passwConfirm: string;

  constructor() { }

  ngOnInit(): void {
  }

}
