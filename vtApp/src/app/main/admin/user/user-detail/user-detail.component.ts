import { Component, Inject, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { UserMasterDataModel } from 'app/models/user/user.master.data.model';
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
  masterData:UserMasterDataModel;
  userForm: FormGroup;
  dialogTitle: string;

  constructor(public matDialogRef: MatDialogRef<UserDetailComponent>,
    @Inject(MAT_DIALOG_DATA) private _data: any,
    private _formBuilder: FormBuilder) { 

      console.log(_data);
      
              // Set the defaults
              this.action = _data.action;
              this.masterData = _data.masterData;


              if ( this.action === 'edit' )
              {
                  this.dialogTitle = 'Edit User';
                  this.user = _data.user;
                  this.userForm = this.createExistingUserForm();
              }
              else
              {
                  this.dialogTitle = 'New User';
                  this.user = new User();
                  this.userForm = this.createContactForm();
              }
      
          
    }

  ngOnInit(): void {
  }

  createExistingUserForm():FormGroup
  {
    console.log(this.user);
    
    return this._formBuilder.group({
      id: [this.user.id],
      firstName: [this.user.firstName,Validators.required],
      lastName: [this.user.lastName,Validators.required],
      username: [{value: this.user.username, disabled: true}, Validators.required],
      password: [''],
      email: [this.user.email],
      mobileNo: [this.user.mobileNo,Validators.required],
      personalAddress:[this.user.personalAddress,Validators.required],

      image:[''],
      timeZoneId:[this.user.timeZoneId,Validators.required],
      isActive: [true],
      nicno:[this.user.nicno,Validators.required],
      nicFrontImage:[''],
      nicBackImage:[''],
      drivingLicenceNo:[this.user.drivingLicenceNo],
      drivingLicenceFrontImage:[''],
      drivingLicenceBackImage:[''],
      roles: [this.user.roles,Validators.required]
    });
  }

  createContactForm(): FormGroup
  {
      return this._formBuilder.group({
        id: [0],
        firstName: ['',Validators.required],
        lastName: ['',Validators.required],
        username: ['',Validators.required],
        password: ['',Validators.required],
        email: [''],
        mobileNo: ['',Validators.required],
        personalAddress:['',Validators.required],

        image:[''],
        timeZoneId:[0,Validators.required],
        isActive: [true],
        nicno:['',Validators.required],
        nicFrontImage:[''],
        nicBackImage:[''],
        drivingLicenceNo:[''],
        drivingLicenceFrontImage:[''],
        drivingLicenceBackImage:[''],
        roles: [null,Validators.required]
      });
  }

  get rolesName()
  {
    return this.userForm.get('roles').value?
              this.masterData.roles.filter(t=>this.userForm.get('roles').value.indexOf(t.id)>=0)
              .map(x=>x.name):
              null
  }

}
