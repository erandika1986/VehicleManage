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
import { VehicleGearBoxOilMilageModel } from 'app/models/vehicle/vehicle-gear-box-oil-milage.model';
import { RouteService } from 'app/services/route/route.service';
import { VehicleGearBoxOilChangeMilageService } from 'app/services/vehicle/vehicle-gear-box-oil-change-milage.service';
import { GearBoxDetailComponent } from '../gear-box-detail/gear-box-detail.component';

@Component({
  selector: 'gear-box-list',
  templateUrl: './gear-box-list.component.html',
  styleUrls: ['./gear-box-list.component.scss'],
  animations: fuseAnimations,
  encapsulation: ViewEncapsulation.None
})
export class GearBoxListComponent implements OnInit, AfterViewInit,OnChanges {

  horizontalPosition: MatSnackBarHorizontalPosition = 'right';
  verticalPosition: MatSnackBarVerticalPosition = 'top';
  
  @Input() vehicleId:number = 0; // decorate the property with @Input()
  @Input() regNo:string="";

  @ViewChild(MatPaginator) paginator: MatPaginator;

  @ViewChild(MatSort) sort: MatSort;

  dataSource = new MatTableDataSource([]);

  totalNumberOfRecords: number;

  displayedColumns = ['buttons','gearBoxOilChangeMilage', 'nextGearBoxOilChangeMilage'];

  dialogRef: any;
  confirmDialogRef: MatDialogRef<FuseConfirmDialogComponent>;
  
  constructor(private _gearBoxOilService: VehicleGearBoxOilChangeMilageService,
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
        this.getVehicleGearBoxOilList();
      }
   }

  getVehicleGearBoxOilList() {
    this._fuseProgressBarService.show();
    this._gearBoxOilService.getAllVehicleGearBoxOilMilage(this.vehicleId)
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
    let gearBoxOil: VehicleGearBoxOilMilageModel = new VehicleGearBoxOilMilageModel();
    gearBoxOil.vehicleId = this.vehicleId;
    gearBoxOil.registrationNo = this.regNo;
    gearBoxOil.isActive=true;

    this.dialogRef = this._matDialog.open(GearBoxDetailComponent, {
      panelClass: 'gear-box-form-dialog',
      data: {
        gearBoxOil: gearBoxOil,
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
        var reactiveObject = formData.getRawValue() as VehicleGearBoxOilMilageModel
        this.save(reactiveObject);

      });
  }

  edit(item:VehicleGearBoxOilMilageModel)
  {
    this.dialogRef = this._matDialog.open(GearBoxDetailComponent, {
      panelClass: 'gear-box-form-dialog',
      data: {
        gearBoxOil: item,
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
            var reactiveObject = formData.getRawValue() as VehicleGearBoxOilMilageModel;
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

  view(item:VehicleGearBoxOilMilageModel)
  {
    this.dialogRef = this._matDialog.open(GearBoxDetailComponent, {
      panelClass: 'gear-box-form-dialog',
      data: {
        gearBoxOil: item,
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
            var reactiveObject = formData.getRawValue() as VehicleGearBoxOilMilageModel;
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
        this._gearBoxOilService.deleteVehicleGearBoxOilMilage(id)
          .subscribe(response=>{
            this._fuseProgressBarService.hide();
            if (response.isSuccess) {
              this._snackBar.open(response.message, 'Success', {
                duration: 2500,
                horizontalPosition: this.horizontalPosition,
                verticalPosition: this.verticalPosition,
              });

              this.getVehicleGearBoxOilList();
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


  save(model:VehicleGearBoxOilMilageModel)
  {
    this._fuseProgressBarService.show();


    this._gearBoxOilService.saveVehicleGearBoxOilMilage(model)
    .subscribe(response=>{
      this._fuseProgressBarService.hide();
      
      if (response.isSuccess) {
        this._snackBar.open(response.message, 'Success', {
          duration: 2500,
          horizontalPosition: this.horizontalPosition,
          verticalPosition: this.verticalPosition,
        });

        this.getVehicleGearBoxOilList();
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
