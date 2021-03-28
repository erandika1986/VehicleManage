import { AfterViewInit, Component, ElementRef, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { FuseConfirmDialogComponent } from '@fuse/components/confirm-dialog/confirm-dialog.component';
import { FuseProgressBarService } from '@fuse/components/progress-bar/progress-bar.service';
import { VehicleTypeModel } from 'app/models/vehicle/vehicle-type.model';
import { VehicleTypeService } from 'app/services/vehicle/vehicle-type.service';

@Component({
  selector: 'vehicle-type-list',
  templateUrl: './vehicle-type-list.component.html',
  styleUrls: ['./vehicle-type-list.component.scss'],
  animations: fuseAnimations,
  encapsulation: ViewEncapsulation.None
})
export class VehicleTypeListComponent implements OnInit, AfterViewInit {

  horizontalPosition: MatSnackBarHorizontalPosition = 'right';
  verticalPosition: MatSnackBarVerticalPosition = 'top';

  dataSource = new MatTableDataSource([]);


  dialogRef: any;
  confirmDialogRef: MatDialogRef<FuseConfirmDialogComponent>;

  displayedColumns = ["buttons", "id", "name", "engineOilChangeMilage", "fuelFilterChangeMilage", "gearBoxChangeMilage", "differentialOilChangeMilage", "engineOilNumber", "fuelFilterNumber", "gearBoxOilNumber", "differentialOilNumber", "airCleanerAge", "greeceNipleAge", "insuranceAge", "fitnessReportAge", "emitionTestAge", "revenueLicenceAge", "fuelTypeName"];

  @ViewChild(MatPaginator) paginator: MatPaginator;

  @ViewChild(MatSort) sort: MatSort;

  @ViewChild('input') input: ElementRef;

  constructor(
    private _route: ActivatedRoute,
    private _fuseProgressBarService: FuseProgressBarService,
    public _router: Router,
    private _matDialog: MatDialog,
    private _snackBar: MatSnackBar,
    private _vehicleTypeService: VehicleTypeService
  ) { }

  ngOnInit(): void {
    this.loadVehicleTypes();
  }

  ngAfterViewInit() {

  }

  addNewVehicleType() {

  }

  editVehicleType(item: VehicleTypeModel) {

  }

  deleteVehicleType(item: VehicleTypeModel) {

  }



  loadVehicleTypes() {
    this._fuseProgressBarService.show();
    this._vehicleTypeService.getAllVehicleTypes()
      .subscribe(response => {
        this._fuseProgressBarService.hide();
        console.log(response);

        this.dataSource = new MatTableDataSource(response);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
      }, error => {
        this._fuseProgressBarService.hide();
      })
  }



  applyFilter(filterValue: string) {
    filterValue = filterValue.trim(); // Remove whitespace
    filterValue = filterValue.toLowerCase(); // Datasource defaults to lowercase matches
    this.dataSource.filter = filterValue;
  }
}
