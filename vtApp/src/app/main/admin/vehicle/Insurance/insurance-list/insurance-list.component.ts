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
import { VehicleInsuranceModel, VehicleInsuranceReactiveForm } from 'app/models/vehicle/vehicle-insurance.model';
import { RouteService } from 'app/services/route/route.service';
import { VehicleInsuranceService } from 'app/services/vehicle/vehicle-insurance.service';
import { InsuranceDetailComponent } from '../insurance-detail/insurance-detail.component';

@Component({
  selector: 'insurance-list',
  templateUrl: './insurance-list.component.html',
  styleUrls: ['./insurance-list.component.scss'],
  animations: fuseAnimations,
  encapsulation: ViewEncapsulation.None
})
export class InsuranceListComponent implements OnInit, AfterViewInit,OnChanges   {
  horizontalPosition: MatSnackBarHorizontalPosition = 'right';
  verticalPosition: MatSnackBarVerticalPosition = 'top';
  
  @Input() vehicleId:number = 0; // decorate the property with @Input()
  @Input() regNo:string="";

  @ViewChild(MatPaginator) paginator: MatPaginator;

  @ViewChild(MatSort) sort: MatSort;

  dataSource = new MatTableDataSource([]);

  totalNumberOfRecords: number;

  displayedColumns = ['buttons','imageURL', 'insuranceDate','validTill'];

  dialogRef: any;
  confirmDialogRef: MatDialogRef<FuseConfirmDialogComponent>;
  
  constructor(
    private _insuranceService:VehicleInsuranceService,
    private _fuseProgressBarService: FuseProgressBarService,
    private _matDialog: MatDialog,
    private _routeService: RouteService,
    private _snackBar: MatSnackBar,
    public _router: Router
    ) { }

  ngOnInit(): void {

  }

  ngAfterViewInit() {


  }

  ngOnChanges(changes: SimpleChanges) {

    if (changes['vehicleId']) {
      this.getVehicleInsuranceList();
    }
 }

  getVehicleInsuranceList()
  {
    this._fuseProgressBarService.show();
    this._insuranceService.getAllVehicleInsuranceDetails(this.vehicleId)
      .subscribe(response=>{
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
    let insurance: VehicleInsuranceModel = new VehicleInsuranceModel();
    insurance.vehicleId = this.vehicleId;
    insurance.registrationNo = this.regNo;
    insurance.isActive=true;

    this.dialogRef = this._matDialog.open(InsuranceDetailComponent, {
      panelClass: 'insurance-detail-form-dialog',
      data: {
        insurance: insurance,
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
        var reactiveObject = formData.getRawValue() as VehicleInsuranceReactiveForm;
        this.save(reactiveObject);

      });
  }

  edit(item:VehicleInsuranceModel)
  {
    console.log(item);
    
    this.dialogRef = this._matDialog.open(InsuranceDetailComponent, {
      panelClass: 'insurance-detail-form-dialog',
      data: {
        insurance: item,
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
            var reactiveObject = formData.getRawValue() as VehicleInsuranceReactiveForm;
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

  view(item:VehicleInsuranceModel)
  {
    console.log(item);
    
    this.dialogRef = this._matDialog.open(InsuranceDetailComponent, {
      panelClass: 'insurance-detail-form-dialog',
      data: {
        insurance: item,
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
            var reactiveObject = formData.getRawValue() as VehicleInsuranceReactiveForm;
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
        this._insuranceService.deleteVehicleInsuranceRecord(id)
          .subscribe(response=>{
            this._fuseProgressBarService.hide();
            if (response.isSuccess) {
              this._snackBar.open(response.message, 'Success', {
                duration: 2500,
                horizontalPosition: this.horizontalPosition,
                verticalPosition: this.verticalPosition,
              });

              this.getVehicleInsuranceList();
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


  save(reactiveObject:VehicleInsuranceReactiveForm)
  {
    this._fuseProgressBarService.show();
    let object:VehicleInsuranceModel = new VehicleInsuranceModel();
    object.id = reactiveObject.id;
    object.vehicleId = reactiveObject.vehicleId;
    object.isActive = reactiveObject.isActive;
    object.updatedBy=0;
    object.createdBy=0;
    object.insuranceYear = reactiveObject.insuranceDate.getFullYear();
    object.insuranceMonth = reactiveObject.insuranceDate.getMonth() + 1
    object.insuranceDay = reactiveObject.insuranceDate.getDate();
    object.validTillYear = reactiveObject.validTillDate.getFullYear();
    object.validTillMonth = reactiveObject.validTillDate.getMonth() + 1
    object.validTillDay = reactiveObject.validTillDate.getDate();

    this._insuranceService.saveVehicleInsurance(object)
    .subscribe(response=>{
      this._fuseProgressBarService.hide();
      
      if (response.isSuccess) {
        this._snackBar.open(response.message, 'Success', {
          duration: 2500,
          horizontalPosition: this.horizontalPosition,
          verticalPosition: this.verticalPosition,
        });

        this.getVehicleInsuranceList();
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
