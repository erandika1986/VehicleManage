import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { LoginModel } from '../models/user/login.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CustomAuthService {

  constructor(private http: HttpClient) { }

  login(loginModel: LoginModel): Observable<any> {
    return this.http.post<any>(environment.apiUrl + 'Auth/login', loginModel);
  }

  isLoggedInUser() {
    const userSession = localStorage.getItem('currentUser');
    if (userSession) {
      return true;
    } else {
      return false;
    }
  }

  signOut() {
    const userSession = localStorage.getItem('currentUser');
    if (userSession) {
      localStorage.removeItem('currentUser');
    }
  }
}
