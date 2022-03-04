import { HttpEventType } from '@angular/common/http';
import { Component, Inject, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
import { FuseProgressBarService } from '@fuse/components/progress-bar/progress-bar.service';
import { Upload } from 'app/models/common/upload';
import { UserMasterDataModel } from 'app/models/user/user.master.data.model';
import { User } from 'app/models/user/user.model';
import { UserService } from 'app/services/user/user.service';
import { EMPTY, Observable } from 'rxjs';

@Component({
  selector: 'user-form-dialog',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class UserDetailComponent implements OnInit {

  horizontalPosition: MatSnackBarHorizontalPosition = 'right';
  verticalPosition: MatSnackBarVerticalPosition = 'top';
  
  action: string;
  user: User;
  masterData:UserMasterDataModel;
  userForm: FormGroup;
  dialogTitle: string;

  constructor(public matDialogRef: MatDialogRef<UserDetailComponent>,
    private _fuseProgressBarService: FuseProgressBarService,
    private _userService:UserService,
    private _snackBar: MatSnackBar,
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

  upload$: Observable<Upload> = EMPTY;
  precentage:any;
  onFileChange(event: any,type:number) 
  {

    let fi = event.srcElement;
    const formData = new FormData();
    formData.set("id",this.user.id.toString());
    formData.set("type",type.toString());
    
    if(fi.files.length>0)
    {
        this._fuseProgressBarService.show();
        for (let index = 0; index < fi.files.length; index++) {
          
          formData.append('file', fi.files[index], fi.files[index].name);
        }

        this._userService.uploadUserImage(formData).subscribe(res=>
          {
            this.precentage =res;
            if(res.state=="DONE")
            {
              //item.isUploading=false;
              this._fuseProgressBarService.hide();
              this._userService.onUserUpdated.next(true);
              this.getUser(this.user.id);
              //this.getVehicleFitnessreportList();
              this._snackBar.open("Image has been uploaded successfully", 'Success', {
                duration: 2500,
                horizontalPosition: this.horizontalPosition,
                verticalPosition: this.verticalPosition,
              });
            }
            //progress
          },error=>{
            this._fuseProgressBarService.hide();
            //item.isUploading=false;
            this._snackBar.open("Network error has been occured. Please try again.", 'Error', {
              duration: 2500,
              horizontalPosition: this.horizontalPosition,
              verticalPosition: this.verticalPosition,
            });
          });
/*         this._quotationService.uploadQuotationFiles(formData)
          .subscribe(response=>{
 
          },error=>{
            console.log("Error occured");
            
          }); */
    }    
  }

  downloadPercentage:number=0;
  isDownloading:boolean;
  downloadFile(type:number,fileName:string)
  { 
    this._fuseProgressBarService.show();
    this.isDownloading=true;
    this._userService.downloadUserImage(this.user.id,type)
      .subscribe(response=>{

        console.log(response);
        

        if (response.type === HttpEventType.DownloadProgress) {
          this.downloadPercentage = Math.round(100 * response.loaded / response.total);
        }
        
        if (response.type === HttpEventType.Response) {
          if(response.status == 204)
          {
            this.isDownloading=false;
            this.downloadPercentage=0;
            this._fuseProgressBarService.hide();
          }
          else
          {
            const objectUrl: string = URL.createObjectURL(response.body);
            const a: HTMLAnchorElement = document.createElement('a') as HTMLAnchorElement;
    
            a.href = objectUrl;
            a.download = fileName;
            document.body.appendChild(a);
            a.click();
    
            document.body.removeChild(a);
            URL.revokeObjectURL(objectUrl);
            this.isDownloading=false;
            this.downloadPercentage=0;
            this._fuseProgressBarService.hide();
          }

        }
      },error=>{
        this._fuseProgressBarService.hide();
        this.isDownloading=false;
        this.downloadPercentage=0;
      });
  }

  getUser(id:number)
  {
    this._fuseProgressBarService.show();
    this._userService.getUserById(id)
      .subscribe(response=>{
        this._fuseProgressBarService.hide();
        this.user = response;
        this.userForm = this.createExistingUserForm();
      },error=>{
        this._fuseProgressBarService.hide();
      });
  }

  get rolesName()
  {
    return this.userForm.get('roles').value?
              this.masterData.roles.filter(t=>this.userForm.get('roles').value.indexOf(t.id)>=0)
              .map(x=>x.name):
              null
  }


  get id()
  {
    return this.userForm.get('id').value;
  }

}
