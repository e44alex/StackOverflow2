/* tslint:disable:no-unused-variable */

import { TestBed, async, inject, getTestBed } from '@angular/core/testing';
import { DataServiceService } from './data-service.service';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing'
import { HttpClient } from '@angular/common/http';
import { Answer, Question, User } from './Model';
import { Guid } from 'guid-typescript';

describe('Service: DataService', () => {

  let service: DataServiceService;
  let httpMock: HttpTestingController;
  let httpClient: HttpClient;

  beforeEach(() => {
      TestBed.configureTestingModule({
        imports: [HttpClientTestingModule],
        providers: [DataServiceService]
      })
      service =  TestBed.inject(DataServiceService);
      httpMock = TestBed.get(HttpTestingController);
      httpClient = TestBed.inject(HttpClient);
    });

  it('should be created', () => {
    expect(service).toBeTruthy();  
  });

  it('should getQuestions', ()=> {

    service.getQuestions().then((data: Question[])=> {
      expect(data).toBeDefined();      
    });

    const req = httpMock.expectOne('https://localhost:44360/api/Questions');
    expect(req.request.method).toEqual("GET");
  })

  it('should getQuestion', ()=> {

    let guid = Guid.create().toString();

    service.getQuestion(guid).then((data: Question)=> {
      expect(data).toBeDefined();      
    });

    const req = httpMock.expectOne('https://localhost:44360/api/Questions/'+guid);
    expect(req.request.method).toEqual("GET");
  })

  it('should getAnswers', ()=> {

    service.getAnswers().then((data: Answer[])=> {
      expect(data).toBeDefined();      
    });

    const req = httpMock.expectOne('https://localhost:44360/api/Answers');
    expect(req.request.method).toEqual("GET");
  })

  it('should getAnswer', ()=> {

    let guid = Guid.create().toString();

    service.getAnswer(guid).then((data: Answer)=> {
      expect(data).toBeDefined();      
    });

    const req = httpMock.expectOne('https://localhost:44360/api/Answers/'+guid);
    expect(req.request.method).toEqual("GET");
  })

  it('should getUser', ()=> {

    let guid = Guid.create().toString();

    service.getUser(guid).then((data: User)=> {
      expect(data).toBeDefined();      
    });

    const req = httpMock.expectOne('https://localhost:44360/api/Users/'+guid);
    expect(req.request.method).toEqual("GET");
  })

  it('should sendAnswer',()=>{
    service.sendAnswer(new Answer(), 'token');
    const req = httpMock.expectOne('https://localhost:44360/api/Answers');
    expect(req.request.method).toEqual("POST");
  })

  it('should sendAnswer',()=>{
    service.sendQuestion(new Question(), 'token');
    const req = httpMock.expectOne('https://localhost:44360/api/Questions');
    expect(req.request.method).toEqual("POST");
  })

  it('should lkeAnswer',()=>{
    let guid = Guid.create().toString();
    service.likeAnswer(guid, 'username', 'token');
    const req = httpMock.expectOne('https://localhost:44360/like?answerId='+guid+'&username=username');
    expect(req.request.method).toEqual("GET");
  })


  afterEach(()=>{
    httpMock.verify();
  })

});
