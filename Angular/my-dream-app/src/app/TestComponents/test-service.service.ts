import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TestServiceService {

users = [
  'user1', 'user2', 'user3' 
]

constructor() { }

getUsers(){
  return this.users;
}

addUser(){
  this.users.push('newUser');
}

deleteUser(){
  this.users.pop();
}

}
