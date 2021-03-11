import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable()
export class AuthGuard implements CanActivate {
    constructor(private router: Router) { }

    public canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {

        const currentUser = localStorage.getItem('IsLoggedInUser');

        console.log(currentUser);

        if (currentUser) {
            return true;
        }

        this.router.navigate(['/pages/auth/login']);
        return false;
    }
}