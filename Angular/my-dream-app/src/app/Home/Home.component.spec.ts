/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement, NO_ERRORS_SCHEMA } from '@angular/core';

import { HomeComponent } from './Home.component';
import { HttpClient } from 'selenium-webdriver/http';
import { DataServiceService } from '../Shared/data-service.service';
import { ActivatedRoute } from '@angular/router';
import { of } from 'rxjs';
import { Question } from '../Shared/Model';

describe('HomeComponent', () => {
  let component: HomeComponent;
  let fixture: ComponentFixture<HomeComponent>;
  let mockDataService;
  let mockHttpClient;
  let mockActivatedRoute;

  beforeEach(async(() => {
    mockDataService = jasmine.createSpyObj(['getQuestions'])
    mockDataService.getQuestions.and.returnValue(Promise.resolve({}));
    mockHttpClient = jasmine.createSpyObj(['get'])
    mockActivatedRoute = jasmine.createSpyObj(['params'])
    TestBed.configureTestingModule({
      declarations: [ HomeComponent ],
      providers: [
        {provide: DataServiceService, useValue: mockDataService},
        {provide: HttpClient, useValue: mockHttpClient},
        {provide: ActivatedRoute, useValue: mockActivatedRoute}
      ],
      schemas: [NO_ERRORS_SCHEMA]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should load data', () => {

    component.ngOnInit()

    expect(component.questions).toBeDefined()
  });
  
});
