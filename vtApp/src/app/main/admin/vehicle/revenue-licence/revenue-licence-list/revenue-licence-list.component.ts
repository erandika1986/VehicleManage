import { HttpEventType } from '@angular/common/http';
import { AfterViewInit, Component, Input, OnChanges, OnInit, SimpleChanges, ViewChild, ViewEncapsulation } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { FuseConfirmDialogComponent } from '@fuse/components/confirm-dialog/confirm-dialog.component';
import { FuseProgressBarService } from '@fuse/components/progress-bar/progress-bar.service';
import { Upload } from 'app/models/common/upload';
import { VehicleRevenueLicenceModel, VehicleRevenueLicenceReactiveForm } from 'app/models/vehicle/vehicle-revenue-licence.model';
import { RouteService } from 'app/services/route/route.service';
import { VehicleInsuranceService } from 'app/services/vehicle/vehicle-insurance.service';
import { VehicleRevenueLicenceService } from 'app/services/vehicle/vehicle-revenue-licence.service';
import { EMPTY, Observable } from 'rxjs';
import { RevenueLicenceDetailComponent } from '../revenue-licence-detail/revenue-licence-detail.component';

@Component({
  selector: 'revenue-licence-list',
  templateUrl: './revenue-licence-list.component.html',
  styleUrls: ['./revenue-licence-list.component.scss'],
  animations: fuseAnimations,
  encapsulation: ViewEncapsulation.None
})
export class RevenueLicenceListComponent implements OnInit, AfterViewInit,OnChanges {

  horizontalPosition: MatSnackBarHorizontalPosition = 'right';
  verticalPosition: MatSnackBarVerticalPosition = 'top';
  
  @Input() vehicleId:number = 0; // decorate the property with @Input()
  @Input() regNo:string="";

  @ViewChild(MatPaginator) paginator: MatPaginator;

  @ViewChild(MatSort) sort: MatSort;

  dataSource = new MatTableDataSource([]);

  totalNumberOfRecords: number;

  displayedColumns = ['buttons','imageURL', 'revenueLicenceDate','validTill','createdOn','createdBy','updatedOn','updatedBy'];

  dialogRef: any;
  confirmDialogRef: MatDialogRef<FuseConfirmDialogComponent>;
  
  constructor(    private _licenceService:VehicleRevenueLicenceService,
    private _fuseProgressBarService: FuseProgressBarService,
    private _matDialog: MatDialog,
    private _routeService: RouteService,
    private _snackBar: MatSnackBar,
    public _router: Router) { }

    ngOnInit(): void {

    }
  
    ngAfterViewInit() {
  
  
    }

    ngOnChanges(changes: SimpleChanges) {

      if (changes['vehicleId']) {
        this.getVehicleRevenueLicenceList();
      }
   }

   getVehicleRevenueLicenceList()
   {
     this._fuseProgressBarService.show();
     this._licenceService.getAllVehicleRevenueLicence(this.vehicleId)
       .subscribe(response=>{
         console.log(response);
         
         this._fuseProgressBarService.hide();
         this.totalNumberOfRecords = response.length;
         this.dataSource = new MatTableDataSource(response);
         this.dataSource.sort = this.sort;
         this.dataSource.paginator = this.paginator;
         
       },error=>{
         this._fuseProgressBarService.hide();
       });
   }
 
   add()
   {
     let licence: VehicleRevenueLicenceModel = new VehicleRevenueLicenceModel();
     licence.vehicleId = this.vehicleId;
     licence.registrationNo = this.regNo;
     licence.isActive=true;
 
     this.dialogRef = this._matDialog.open(RevenueLicenceDetailComponent, {
       panelClass: 'licence-detail-form-dialog',
       data: {
          licence: licence,
         action: "add",
         totalNoOfRecords:this.totalNumberOfRecords,
         isReadOnly:false
       }
     });
 
     this.dialogRef.afterClosed()
       .subscribe(response => {
         if (!response) {
           return;
         }
 
         let formData: FormGroup = response;
         var reactiveObject = formData.getRawValue() as VehicleRevenueLicenceReactiveForm
         this.save(reactiveObject);
 
       });
   }
 
   edit(item:VehicleRevenueLicenceModel)
   {
     console.log(item);
     
     this.dialogRef = this._matDialog.open(RevenueLicenceDetailComponent, {
       panelClass: 'licence-detail-form-dialog',
       data: {
        licence: item,
         action: "edit",
         totalNoOfRecords:this.totalNumberOfRecords,
         isReadOnly:false
       }
     });
 
     this.dialogRef.afterClosed()
       .subscribe(response => {
         if (!response) {
           return;
         }
         const actionType: string = response[0];
         const formData: FormGroup = response[1];
         switch (actionType) {
           /**
            * Save
            */
           case 'save':
             var reactiveObject = formData.getRawValue() as VehicleRevenueLicenceReactiveForm;
             this.save(reactiveObject);
 
             break;
           /**
            * Delete
            */
           case 'delete':
 
               this.delete(reactiveObject.id);
 
             break;
         }
       });
   }
 
   view(item:VehicleRevenueLicenceModel)
   {
     console.log(item);
     
     this.dialogRef = this._matDialog.open(RevenueLicenceDetailComponent, {
       panelClass: 'licence-detail-form-dialog',
       data: {
        licence: item,
         action: "edit",
         totalNoOfRecords:this.totalNumberOfRecords,
         isReadOnly:true
       }
     });
 
     this.dialogRef.afterClosed()
       .subscribe(response => {
         if (!response) {
           return;
         }
         const actionType: string = response[0];
         let formData: FormGroup = response[1];
         switch (actionType) {
           /**
            * Save
            */
           case 'save':
             var reactiveObject = formData.getRawValue() as VehicleRevenueLicenceReactiveForm;
             this.save(reactiveObject);
 
             break;
           /**
            * Delete
            */
           case 'delete':
 
               this.delete(reactiveObject.id);
 
             break;
         }
       });
   }
 
   delete(id:number)
   {
 
     this.confirmDialogRef = this._matDialog.open(FuseConfirmDialogComponent, {
       disableClose: false
     });
 
     this.confirmDialogRef.componentInstance.confirmMessage = 'Are you sure you want to delete this record?';
 
     this.confirmDialogRef.afterClosed().subscribe(result => {
       if (result) {
         this._fuseProgressBarService.show();
 
         this._fuseProgressBarService.show();
         this._licenceService.deleteVehicleRevenueLicence(id)
           .subscribe(response=>{
             this._fuseProgressBarService.hide();
             if (response.isSuccess) {
               this._snackBar.open(response.message, 'Success', {
                 duration: 2500,
                 horizontalPosition: this.horizontalPosition,
                 verticalPosition: this.verticalPosition,
               });
 
               this.getVehicleRevenueLicenceList();
             }
             else {
               this._snackBar.open(response.message, 'Error', {
                 duration: 2500,
                 horizontalPosition: this.horizontalPosition,
                 verticalPosition: this.verticalPosition,
               });
             }
           },error=>{
             this._fuseProgressBarService.hide();
             this._fuseProgressBarService.hide();
             this._snackBar.open("Network error has been occured. Please try again.", 'Error', {
               duration: 2500,
               horizontalPosition: this.horizontalPosition,
               verticalPosition: this.verticalPosition,
             });
           });
 
       }
       this.confirmDialogRef = null;
     });
 
 
   }
 
 
   save(reactiveObject:VehicleRevenueLicenceReactiveForm)
   {
     this._fuseProgressBarService.show();
     let object:VehicleRevenueLicenceModel = new VehicleRevenueLicenceModel();
     object.id = reactiveObject.id;
     object.vehicleId = reactiveObject.vehicleId;
     object.isActive = reactiveObject.isActive;
     object.note = reactiveObject.note;
     object.revenueLicenceYear = reactiveObject.revenueLicenceDate.getFullYear();
     object.revenueLicenceMonth = reactiveObject.revenueLicenceDate.getMonth() + 1
     object.revenueLicenceDay = reactiveObject.revenueLicenceDate.getDate();
     object.validTillYear = reactiveObject.validTillDate.getFullYear();
     object.validTillMonth = reactiveObject.validTillDate.getMonth() + 1
     object.validTillDay = reactiveObject.validTillDate.getDate();
 
     this._licenceService.saveVehicleRevenueLicence(object)
     .subscribe(response=>{
       this._fuseProgressBarService.hide();
       
       if (response.isSuccess) {
         this._snackBar.open(response.message, 'Success', {
           duration: 2500,
           horizontalPosition: this.horizontalPosition,
           verticalPosition: this.verticalPosition,
         });
 
         this.getVehicleRevenueLicenceList();
       }
       else {
         this._snackBar.open(response.message, 'Error', {
           duration: 2500,
           horizontalPosition: this.horizontalPosition,
           verticalPosition: this.verticalPosition,
         });
       }
     },error=>{
       this._fuseProgressBarService.hide();
       this._snackBar.open("Network error has been occured. Please try again.", 'Error', {
         duration: 2500,
         horizontalPosition: this.horizontalPosition,
         verticalPosition: this.verticalPosition,
       });
     });
     
   }
 
   upload$: Observable<Upload> = EMPTY;
   precentage:any;
   onFileChange(event: any,item:VehicleRevenueLicenceModel) 
   {
 
     let fi = event.srcElement;
     const formData = new FormData();
     formData.set("id",item.id.toString());
     
     if(fi.files.length>0)
     {
         this._fuseProgressBarService.show();
         for (let index = 0; index < fi.files.length; index++) {
           
           formData.append('file', fi.files[index], fi.files[index].name);
         }
         item.isUploading=true;
         this._licenceService.uploadRevenueLicenceImage(formData).subscribe(res=>
           {
             this.precentage =res;
             if(res.state=="DONE")
             {
               //item.isUploading=false;
               this._fuseProgressBarService.hide();
               this.getVehicleRevenueLicenceList();
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
   downloadFile(item:VehicleRevenueLicenceModel)
   {
     this._fuseProgressBarService.show();
     this.isDownloading=true;
     this._licenceService.downloadRevenueLicenceImage(item.id)
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
             a.download = item.imageName;
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



}
