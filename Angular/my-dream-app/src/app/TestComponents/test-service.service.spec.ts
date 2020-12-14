/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { TestServiceService } from './test-service.service';

describe('Service: TestService', () => {

let service: TestServiceService;

  beforeEach(() => {
    service = new TestServiceService();
  });

  it('should return users',()=>{
    expect(service.getUsers().length).toBe(3);
  })

  it('should add users',()=>{
    service.addUser();
    expect(service.getUsers().length).toBe(4);
  })

  it('should delete users',()=>{
    service.deleteUser();
    expect(service.getUsers().length).toBe(2);
  })
});
