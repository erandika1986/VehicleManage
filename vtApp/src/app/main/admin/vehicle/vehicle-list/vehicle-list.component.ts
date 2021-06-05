import { AfterViewInit, Component, ElementRef, OnInit, ViewChild, ViewEncapsulation ,EventEmitter} from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { FuseConfirmDialogComponent } from '@fuse/components/confirm-dialog/confirm-dialog.component';
import { FuseProgressBarService } from '@fuse/components/progress-bar/progress-bar.service';
import { VehicleModel } from 'app/models/vehicle/vehicle.model';
import { VehicleService } from 'app/services/vehicle/vehicle.service';

import {MatPaginator} from '@angular/material/paginator';
import {MatSort, SortDirection} from '@angular/material/sort';
import {merge, Observable, of as observableOf} from 'rxjs';
import {catchError, map, startWith, switchMap} from 'rxjs/operators';


@Component({
  selector: 'app-vehicle-list',
  templateUrl: './vehicle-list.component.html',
  styleUrls: ['./vehicle-list.component.scss'],
  animations: fuseAnimations,
  encapsulation: ViewEncapsulation.None
})
export class VehicleListComponent implements OnInit, AfterViewInit {

  horizontalPosition: MatSnackBarHorizontalPosition = 'right';
  verticalPosition: MatSnackBarVerticalPosition = 'top';

  resultsLength = 1;
  isLoadingResults = true;
  isRateLimitReached = false;
  pageSize:number=10;

  dataSource = new MatTableDataSource([]);
  data: VehicleModel[] = [];

  dialogRef: any;
  confirmDialogRef: MatDialogRef<FuseConfirmDialogComponent>;

  displayedColumns = ["buttons", "registrationNo", "vehicelTypeName", "initialOdometerReading", "productionYear", "isActive"];



  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  @ViewChild('input') input: ElementRef;
  searchKey:EventEmitter<string> = new EventEmitter();
  
  constructor(
    private _route: ActivatedRoute,
    private _fuseProgressBarService: FuseProgressBarService,
    public _router: Router,
    private _matDialog: MatDialog,
    private _snackBar: MatSnackBar,
    private _vehicleService: VehicleService
  ) { }

  ngOnInit(): void {
    //this.loadVehicles();
  }

  ngAfterViewInit() {
    //this.exampleDatabase = new ExampleHttpDatabase(this._httpClient);

    // If the user changes the sort order, reset back to the first page.
    this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 0);
    this.refreshDataSet("");

  }

  addNewVehicle() {
    this._router.navigate(['admin/vehicle/list/' + 0 ]);
  }

  editVehicle(item: VehicleModel) {
    this._router.navigate(['admin/vehicle/list/' + item.id ]);
  }

  deleteVehicleType(item: VehicleModel) {

  }

  refreshDataSet(filterText:string)
  {
    merge(this.sort.sortChange, this.paginator.page,this.searchKey)
      .pipe(
        startWith({}),
        switchMap(() => {
          this.isLoadingResults = true;
          return this._vehicleService.getAllVehicles(this.pageSize,this.paginator.pageIndex+1,this.sort.active,this.sort.direction,this.input.nativeElement.value).pipe(catchError(() => observableOf(null)));
        }),
        map(data => {
          // Flip flag to show that loading has finished.
          this.isLoadingResults = false;
          this.isRateLimitReached = data === null;

          if (data === null) {
            return [];
          }
          // Only refresh the result length if there is new data. In case of rate
          // limit errors, we do not want to reset the paginator to zero, as that
          // would prevent users from re-triggering requests.
          this.resultsLength = data.totalRecordCount;
          return data.data;
        })
      ).subscribe(data =>{    
        this.data = data;
      });
  }

  applyFilter(filterValue: string) {
    this.searchKey.emit(filterValue);
    
    //this.refreshDataSet(filterValue);
  }

}
