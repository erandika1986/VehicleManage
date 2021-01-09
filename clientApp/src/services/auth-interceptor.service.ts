import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpSentEvent, HttpHeaderResponse, HttpProgressEvent, HttpUserEvent, HttpEvent, HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { CustomAuthService } from './custom-auth.service';
import { Router } from '@angular/router';
import { Observable, throwError } from 'rxjs';
import { map, filter, scan, catchError, tap } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class AuthInterceptorService implements HttpInterceptor {

  constructor(private router: Router,
    private authService: CustomAuthService,
    private toastr: ToastrService) { }

  intercept(req: HttpRequest<any>,
    next: HttpHandler): Observable<HttpEvent<any>> {

    const idToken = localStorage.getItem('currentUser');

    if (idToken) {
      const token: string = 'Bearer ' + JSON.parse(idToken).token;

      const cloned = req.clone({
        setHeaders: { Authorization: token }

      });

      return next.handle(cloned).pipe(map((event: HttpEvent<any>) => {
        if (event instanceof HttpResponse) {
          // console.log('event--->>>', event);
        }
        return event;
      }),
        catchError((error: HttpErrorResponse) => {
          if (error.status === 401) {
            this.authService.signOut();
            this.toastr.error('UnAuthorized user attempt. Please login with valid credentials.', '401 Unauthorized');
            this.router.navigate(['']);
          }
          return throwError(error);
        }));
    } else {
      return next.handle(req).pipe(map((event: HttpEvent<any>) => {
        if (event instanceof HttpResponse) {
          console.log('event--->>>', event);
        }
        return event;
      }),
        catchError((error: HttpErrorResponse) => {
          if (error.status === 401) {
            this.authService.signOut();
            this.toastr.error('UnAuthorized user attempt. Please login with valid credentials.', '401 Unauthorized');
            this.router.navigate(['']);
          }
          return throwError(error);
        }));
    }
  }
}
