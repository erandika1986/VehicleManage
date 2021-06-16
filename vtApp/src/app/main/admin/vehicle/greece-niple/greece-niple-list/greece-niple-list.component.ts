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
import { VehicleGreeceNipleModel, VehicleGreeceNipleReactiveForm } from 'app/models/vehicle/vehicle-greece-niple';
import { RouteService } from 'app/services/route/route.service';
import { VehicleGreeceNipleService } from 'app/services/vehicle/vehicle-greece-niple.service';
import { GreeceNipleDetailComponent } from '../greece-niple-detail/greece-niple-detail.component';

@Component({
  selector: 'greece-niple-list',
  templateUrl: './greece-niple-list.component.html',
  styleUrls: ['./greece-niple-list.component.scss'],
  animations: fuseAnimations,
  encapsulation: ViewEncapsulation.None
})
export class GreeceNipleListComponent implements OnInit, AfterViewInit,OnChanges {

  horizontalPosition: MatSnackBarHorizontalPosition = 'right';
  verticalPosition: MatSnackBarVerticalPosition = 'top';
  
  @Input() vehicleId:number = 0; // decorate the property with @Input()
  @Input() regNo:string="";

  @ViewChild(MatPaginator) paginator: MatPaginator;

  @ViewChild(MatSort) sort: MatSort;

  dataSource = new MatTableDataSource([]);

  totalNumberOfRecords: number;

  displayedColumns = ['buttons','greeceNipleReplaceDate', 'nextGreeceNipleReplaceDate','createdOn','createdBy','updatedOn','updatedBy'];

  dialogRef: any;
  confirmDialogRef: MatDialogRef<FuseConfirmDialogComponent>;
  
  constructor(private _greeceNipleService:VehicleGreeceNipleService,
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
        this.getVehicleGreeceNipleLicenceList();
      }
   }
  getVehicleGreeceNipleLicenceList() {
    this._fuseProgressBarService.show();
    this._greeceNipleService.getAllVehicleGreeceNiple(this.vehicleId)
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
    let greeceNiple: VehicleGreeceNipleModel = new VehicleGreeceNipleModel();
    greeceNiple.vehicleId = this.vehicleId;
    greeceNiple.registrationNo = this.regNo;
    greeceNiple.isActive=true;

    this.dialogRef = this._matDialog.open(GreeceNipleDetailComponent, {
      panelClass: 'greece-niple-form-dialog',
      data: {
        greeceNiple: greeceNiple,
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
        var reactiveObject = formData.getRawValue() as VehicleGreeceNipleReactiveForm
        this.save(reactiveObject);

      });
  }

  edit(item:VehicleGreeceNipleModel)
  {
    console.log(item);
    
    this.dialogRef = this._matDialog.open(GreeceNipleDetailComponent, {
      panelClass: 'greece-niple-form-dialog',
      data: {
        greeceNiple: item,
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
            var reactiveObject = formData.getRawValue() as VehicleGreeceNipleReactiveForm;
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

  view(item:VehicleGreeceNipleModel)
  {
    console.log(item);
    
    this.dialogRef = this._matDialog.open(GreeceNipleDetailComponent, {
      panelClass: 'greece-niple-form-dialog',
      data: {
        greeceNiple: item,
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
            var reactiveObject = formData.getRawValue() as VehicleGreeceNipleReactiveForm;
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
        this._greeceNipleService.deleteVehicleGreeceNiple(id)
          .subscribe(response=>{
            this._fuseProgressBarService.hide();
            if (response.isSuccess) {
              this._snackBar.open(response.message, 'Success', {
                duration: 2500,
                horizontalPosition: this.horizontalPosition,
                verticalPosition: this.verticalPosition,
              });

              this.getVehicleGreeceNipleLicenceList();
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


  save(reactiveObject:VehicleGreeceNipleReactiveForm)
  {
    this._fuseProgressBarService.show();
    let object:VehicleGreeceNipleModel = new VehicleGreeceNipleModel();
    object.id = reactiveObject.id;
    object.vehicleId = reactiveObject.vehicleId;
    object.isActive = reactiveObject.isActive;
    object.greeceNipleReplacYear = reactiveObject.greeceNipleReplaceDate.getFullYear();
    object.greeceNipleReplacMonth = reactiveObject.greeceNipleReplaceDate.getMonth() + 1
    object.greeceNipleReplacDay = reactiveObject.greeceNipleReplaceDate.getDate();
    object.nextGreeceNipleReplaceYear = reactiveObject.nextGreeceNipleReplaceDate.getFullYear();
    object.nextGreeceNipleReplaceMonth = reactiveObject.nextGreeceNipleReplaceDate.getMonth() + 1
    object.nextGreeceNipleReplaceDay = reactiveObject.nextGreeceNipleReplaceDate.getDate();

    this._greeceNipleService.saveVehicleGreeceNiple(object)
    .subscribe(response=>{
      this._fuseProgressBarService.hide();
      
      if (response.isSuccess) {
        this._snackBar.open(response.message, 'Success', {
          duration: 2500,
          horizontalPosition: this.horizontalPosition,
          verticalPosition: this.verticalPosition,
        });

        this.getVehicleGreeceNipleLicenceList();
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

}
