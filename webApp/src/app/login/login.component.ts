import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { routerTransition } from '../router.animations';
import { CustomAuthService } from '../services/custom-auth.service';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { FormValidatorService } from '../services/common/form-validator.service';
import { LoginModel } from '../models/user/login.model';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss'],
    animations: [routerTransition()]
})
export class LoginComponent implements OnInit {

    form = new FormGroup({
        username: new FormControl('', Validators.required),
        password: new FormControl('', Validators.required)
    });

    constructor(
        public router: Router,
        private authService: CustomAuthService,
        private spinner: NgxSpinnerService,
        private toastr: ToastrService,
        private validatorService: FormValidatorService,

    ) { }

    ngOnInit() { }

    onLoggedin() {
        if (!this.form.valid) {
            this.validatorService.validateAllFormFields(this.form);
        }
        else {
            this.spinner.show();
            this.authService.login(this.form.value)
                .subscribe(response => {
                    //console.log(response);
                    this.spinner.hide();
                    if (response && response.token) {
                        localStorage.setItem('currentUser', JSON.stringify(response));
                        localStorage.setItem('isLoggedin', 'true');
                        this.router.navigate(['/']);
                    }
                    else {
                        this.toastr.error("Please check your username and password are correct", "Login Failed");
                    }
                }, error => {

                    this.spinner.hide();
                    this.toastr.error("Please check your username and password are correct", "Login Failed");

                });
        }


    }

    get username() {
        return this.form.get("username");
    }

    get password() {
        return this.form.get("password");
    }
}
