import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthServiceService {
  
  getId(username: string): Promise<string> {
    return new Promise((resolve, reject) => {
      this.httpClient.get("https://localhost:44360/api/Users/getId/"+username).subscribe((data:string) => 
      {
        return resolve(data)
      })
    })
  }

  constructor(private httpClient: HttpClient) { }

  authenticate(username: string, password:string): Promise<string>{
    return new Promise((resolve, reject) =>{
      this.httpClient.get("https://localhost:44360/token?username="+username+"&password="+password)
      .subscribe((data:string) => {
        console.log(data)
        return resolve(data['access_token'])
      })
    })
  }

  checkLogin(username:string, token): Promise<boolean>{
    return new Promise((resolve, reject) => {
      const headers = new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
      })
      this.httpClient.get("https://localhost:44360/checkLogin?username="+username, {headers: headers })
      resolve(true)
    })
  }

}
