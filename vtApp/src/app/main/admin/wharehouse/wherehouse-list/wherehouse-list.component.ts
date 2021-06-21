import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { FuseConfirmDialogComponent } from '@fuse/components/confirm-dialog/confirm-dialog.component';
import { FuseProgressBarService } from '@fuse/components/progress-bar/progress-bar.service';
import { WarehouseService } from 'app/services/warehouse/warehouse.service';

@Component({
  selector: 'wherehouse-list',
  templateUrl: './wherehouse-list.component.html',
  styleUrls: ['./wherehouse-list.component.scss']
})
export class WherehouseListComponent implements OnInit {

  horizontalPosition: MatSnackBarHorizontalPosition = 'right';
  verticalPosition: MatSnackBarVerticalPosition = 'top';

  dataSource = new MatTableDataSource([]);


  dialogRef: any;
  confirmDialogRef: MatDialogRef<FuseConfirmDialogComponent>;

  displayedColumns = ["buttons", "id", "address", "phone", "managerName", "floorSpace"];

  @ViewChild(MatPaginator) paginator: MatPaginator;

  @ViewChild(MatSort) sort: MatSort;

  @ViewChild('input') input: ElementRef;

  constructor(private _route: ActivatedRoute,
    private _warehouseService: WarehouseService,
    private _fuseProgressBarService: FuseProgressBarService,
    private _matDialog: MatDialog,
    private _snackBar: MatSnackBar,
    public _router: Router) { }

  ngOnInit(): void {
    
    this.loadWarehouses();
  }

  editWarehouse(){

  }

  addNewWarehouse(){

  }

  loadWarehouses() {
    this._fuseProgressBarService.show();
    this._warehouseService.GetAllWarehouses()
      .subscribe(response => {
        this._fuseProgressBarService.hide();
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


saveWarehouse(){

}

deleteWarehouse(){

}

  

}
