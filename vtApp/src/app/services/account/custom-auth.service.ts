import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { LoginModel } from '../../models/user/login.model';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class CustomAuthService {

  constructor(private http: HttpClient, private router: Router) { }

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


  getFirstName(): string {

    let loggedInUser = localStorage.getItem("currentUser");
    if (loggedInUser) {
      return JSON.parse(loggedInUser).firstName;
    }
    return "Unkown User";
  }

  getEmail(): string {

    let loggedInUser = localStorage.getItem("currentUser");
    if (loggedInUser) {
      return JSON.parse(loggedInUser).email;
    }
    return "Unkown Email";
  }

  getProfilePic(): string {

    let loggedInUser = localStorage.getItem("currentUser");
    if (loggedInUser) {
      return JSON.parse(loggedInUser).profilePic;
    }
    return "";
  }

  getRole(): string {

    let loggedInUser = localStorage.getItem("currentUser");
    if (loggedInUser) {
      return JSON.parse(loggedInUser).role;
    }
    return "";
  }

  signOut() {
    const userSession = localStorage.getItem('currentUser');
    if (userSession) {
      this.removeAllSaveLocalStorageSession();
    }
    this.router.navigate(['/pages/auth/login']);
  }


  removeAllSaveLocalStorageSession() {
    localStorage.removeItem('currentUser');
    localStorage.removeItem('IsLoggedInUser');
  }
}
