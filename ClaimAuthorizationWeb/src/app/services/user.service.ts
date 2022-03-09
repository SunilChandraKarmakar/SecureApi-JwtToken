import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LoginViewModel } from '../models/login/login-view-model';
import { UpsertUserViewModel } from '../models/user/upsert-user-view-model';
import { UserViewModel } from '../models/user/user-view-model';

@Injectable({
  providedIn: 'root'
})

export class UserService {

  private appBaseUrl: string = 'https://localhost:7074/api/';
  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private _httpClient: HttpClient) { }

  getUsers(): Observable<UserViewModel[]> {
    const getAllUsersUrl: string = `${this.appBaseUrl}user/getUsers`;
    return this._httpClient.get<UserViewModel[]>(getAllUsersUrl);
  }

  registerUser(model: UpsertUserViewModel): Observable<UpsertUserViewModel> {
    const registerUserUrl: string = `${this.appBaseUrl}user/registerUser`;
    return this._httpClient.post<UpsertUserViewModel>(registerUserUrl, model, this.httpOptions);
  }

  login(model: LoginViewModel): Observable<UserViewModel> {
    const loginUserUrl: string = `${this.appBaseUrl}user/login`;
    return this._httpClient.post<UserViewModel>(loginUserUrl, model, this.httpOptions);
  }
}