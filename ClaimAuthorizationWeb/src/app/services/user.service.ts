import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LoginViewModel } from '../models/login/login-view-model';
import { RegisterViewModel } from '../models/register/register-view-model';
import { UserEditViewModel } from '../models/user/user-edit-view-model';
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
    let userInfo = JSON.parse(localStorage.getItem('loginUserInfo'));
    const asseccPermission = new HttpHeaders ({
      'Authorization' : `Bearer ${userInfo.dataSet.token}`
    });  

    const getAllUsersUrl: string = `${this.appBaseUrl}user/getUsers`;
    return this._httpClient.get<UserViewModel[]>(getAllUsersUrl, {headers: asseccPermission});
  }

  registerUser(model: RegisterViewModel): Observable<RegisterViewModel> {
    const registerUserUrl: string = `${this.appBaseUrl}user/registerUser`;
    return this._httpClient.post<RegisterViewModel>(registerUserUrl, model, this.httpOptions);
  }

  login(model: LoginViewModel): Observable<UserViewModel> {
    const loginUserUrl: string = `${this.appBaseUrl}user/login`;
    return this._httpClient.post<UserViewModel>(loginUserUrl, model, this.httpOptions);
  }

  delete(id: string): Observable<UserViewModel> {
    const deleteUserUrl: string = `${this.appBaseUrl}user/${id}`;
    return this._httpClient.delete<UserViewModel>(deleteUserUrl);
  }

  get(id: string): Observable<UserEditViewModel> {
    const getUserUrl: string = `${this.appBaseUrl}user/${id}`;
    return this._httpClient.get<UserEditViewModel>(getUserUrl);
  }

  put(id: string, model: UserEditViewModel): Observable<UserEditViewModel> {
    const editUserUrl: string = `${this.appBaseUrl}user/${id}`;
    return this._httpClient.put<UserEditViewModel>(editUserUrl, model, this.httpOptions);
  }
}