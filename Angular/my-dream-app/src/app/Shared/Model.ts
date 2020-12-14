import { DecimalPipe } from '@angular/common';
import { Guid } from 'guid-typescript';

export class User {
    id: string;
    login: string;
    name:string;
    surname:string;
    phoneNumber: string
    email:string;
    rating: number;
    dateRegistered: Date;
    exerience: number;
    position: string
}

export class Question{
    id: string;
    topic: string;
    body: string;
    dateCreated: Date;
    lastActivity: Date;
    creator: User;
    opened: boolean;
    answers: Answer[]
}

export class Answer{
    id: string;
    question: Question;
    body:string;
    dateCreated: Date;
    creator: User;
    users: User[];
}

export class AnswerLiker{
    id: string;
    user: User;
    answer: Answer
}