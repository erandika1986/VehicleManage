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
import { VehicleDifferentialOilChangeMilageModel } from 'app/models/vehicle/vehicle-differential-oil-change-milage.model';
import { VehicleEngineOilMilageModel } from 'app/models/vehicle/vehicle-engine-oil-milage.model';
import { RouteService } from 'app/services/route/route.service';
import { VehicleDifferentialOilChangeMilageService } from 'app/services/vehicle/vehicle-differential-oil-change-milage.service';
import { DifferentialOilDetailComponent } from '../differential-oil-detail/differential-oil-detail.component';

@Component({
  selector: 'differential-oil-list',
  templateUrl: './differential-oil-list.component.html',
  styleUrls: ['./differential-oil-list.component.scss'],
  animations: fuseAnimations,
  encapsulation: ViewEncapsulation.None
})
export class DifferentialOilListComponent implements OnInit, AfterViewInit,OnChanges {

  horizontalPosition: MatSnackBarHorizontalPosition = 'right';
  verticalPosition: MatSnackBarVerticalPosition = 'top';
  
  @Input() vehicleId:number = 0; // decorate the property with @Input()
  @Input() regNo:string="";

  @ViewChild(MatPaginator) paginator: MatPaginator;

  @ViewChild(MatSort) sort: MatSort;

  dataSource = new MatTableDataSource([]);

  totalNumberOfRecords: number;

  displayedColumns = ['buttons','differentialOilChangeMilage', 'nextDifferentialOilChangeMilage','createdOn','createdBy','updatedOn','updatedBy'];

  dialogRef: any;
  confirmDialogRef: MatDialogRef<FuseConfirmDialogComponent>;
  
  constructor(private _differentialOilService: VehicleDifferentialOilChangeMilageService,
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
      this.getVehicleDifferentialOiltList();
    }
 }
  getVehicleDifferentialOiltList() {
    this._fuseProgressBarService.show();
    this._differentialOilService.getAllVehicleDifferentialOilChangeMilage(this.vehicleId)
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
    let differentialOil: VehicleDifferentialOilChangeMilageModel = new VehicleDifferentialOilChangeMilageModel();
    differentialOil.vehicleId = this.vehicleId;
    differentialOil.registrationNo = this.regNo;
    differentialOil.isActive=true;

    this.dialogRef = this._matDialog.open(DifferentialOilDetailComponent, {
      panelClass: 'engine-oil-form-dialog',
      data: {
        differentialOil: differentialOil,
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
        var reactiveObject = formData.getRawValue() as VehicleDifferentialOilChangeMilageModel
        this.save(reactiveObject);

      });
  }

  edit(item:VehicleEngineOilMilageModel)
  {
    this.dialogRef = this._matDialog.open(DifferentialOilDetailComponent, {
      panelClass: 'engine-oil-form-dialog',
      data: {
        differentialOil: item,
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
            var reactiveObject = formData.getRawValue() as VehicleDifferentialOilChangeMilageModel;
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

  view(item:VehicleDifferentialOilChangeMilageModel)
  {
    this.dialogRef = this._matDialog.open(DifferentialOilDetailComponent, {
      panelClass: 'engine-oil-form-dialog',
      data: {
        differentialOil: item,
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
            var reactiveObject = formData.getRawValue() as VehicleDifferentialOilChangeMilageModel;
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
        this._differentialOilService.deleteVehicleDifferentialOilChangeMilage(id)
          .subscribe(response=>{
            this._fuseProgressBarService.hide();
            if (response.isSuccess) {
              this._snackBar.open(response.message, 'Success', {
                duration: 2500,
                horizontalPosition: this.horizontalPosition,
                verticalPosition: this.verticalPosition,
              });

              this.getVehicleDifferentialOiltList();
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


  save(model:VehicleDifferentialOilChangeMilageModel)
  {
    this._fuseProgressBarService.show();


    this._differentialOilService.saveVehicleDifferentialOilChangeMilage(model)
    .subscribe(response=>{
      this._fuseProgressBarService.hide();
      
      if (response.isSuccess) {
        this._snackBar.open(response.message, 'Success', {
          duration: 2500,
          horizontalPosition: this.horizontalPosition,
          verticalPosition: this.verticalPosition,
        });

        this.getVehicleDifferentialOiltList();
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
