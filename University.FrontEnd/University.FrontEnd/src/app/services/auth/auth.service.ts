import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private _http: HttpClient) { }

  authUser(userName: string, password: string){
    let url = "https://localhost:7273/api/v2/account/GetToken"
    let body = {
      UserName: userName,
      Password: password
    };
    return this._http.post(url, body);
  }

  createUser(userName: string, password: string, email: string){
    let url = "https://localhost:7273/api/v2/account/RegisterNewUser";
    let body ={
      username: userName,
      password: password,
      email: email
    };
    return this._http.post(url, body);
  }


}
