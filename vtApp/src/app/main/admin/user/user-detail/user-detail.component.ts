import { Component, Inject, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { User } from 'app/models/user/user.model';

@Component({
  selector: 'user-form-dialog',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class UserDetailComponent implements OnInit {

  action: string;
  user: User;
  userForm: FormGroup;
  dialogTitle: string;

  constructor(public matDialogRef: MatDialogRef<UserDetailComponent>,
    @Inject(MAT_DIALOG_DATA) private _data: any,
    private _formBuilder: FormBuilder) { 
              // Set the defaults
              this.action = _data.action;

              if ( this.action === 'edit' )
              {
                  this.dialogTitle = 'Edit User';
                  this.user = _data.user;
              }
              else
              {
                  this.dialogTitle = 'New User';
                  this.user = new User();
              }
      
              this.userForm = this.createContactForm();
    }

  ngOnInit(): void {
  }

  createContactForm(): FormGroup
  {
      return this._formBuilder.group({
        id: [0],
        firstName: ['',Validators.required],
        lastName: ['',Validators.required],
        userName: ['',Validators.required],
        email: ['',Validators.required],
        mobileNo: ['',Validators.required],
        personalAddress:['',Validators.required],
        password: ['',Validators.required],
        image:[''],
        timeZoneId:[0,Validators.required],
        isActive: [true],
        nicno:['',Validators.required],
        nicFrontImage:[''],
        nicBackImage:[''],
        drivingLicenceNo:[''],
        drivingLicenceFrontImage:[''],
        drivingLicenceBackImage:[''],
        role: [0]
      });
  }

}
