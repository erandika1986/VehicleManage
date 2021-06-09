import { AfterViewInit, Component, Input, OnChanges, OnInit, SimpleChanges, ViewChild, ViewEncapsulation } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { FuseConfirmDialogComponent } from '@fuse/components/confirm-dialog/confirm-dialog.component';
import { FuseProgressBarService } from '@fuse/components/progress-bar/progress-bar.service';
import { VehicleInsuranceModel } from 'app/models/vehicle/vehicle-insurance.model';
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

        const formData: FormGroup = response;

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



            break;
          /**
           * Delete
           */
          case 'delete':



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
        const formData: FormGroup = response[1];
        switch (actionType) {
          /**
           * Save
           */
          case 'save':



            break;
          /**
           * Delete
           */
          case 'delete':



            break;
        }
      });
  }

}
