import { WarehouseModel } from 'app/models/warehouse/warehouse.model';
import { Component, ElementRef, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { FuseConfirmDialogComponent } from '@fuse/components/confirm-dialog/confirm-dialog.component';
import { FuseProgressBarService } from '@fuse/components/progress-bar/progress-bar.service';
import { WarehouseService } from 'app/services/warehouse/warehouse.service';
import { WherehouseDetailComponent } from '../wherehouse-detail/wherehouse-detail.component';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'wherehouse-list',
  templateUrl: './wherehouse-list.component.html',
  styleUrls: ['./wherehouse-list.component.scss'],
  animations: fuseAnimations,
  encapsulation: ViewEncapsulation.None
})
export class WherehouseListComponent implements OnInit {

  horizontalPosition: MatSnackBarHorizontalPosition = 'right';
  verticalPosition: MatSnackBarVerticalPosition = 'top';

  dataSource = new MatTableDataSource([]);


  dialogRef: any;
  confirmDialogRef: MatDialogRef<FuseConfirmDialogComponent>;

  displayedColumns = ["buttons", "id","name", "address", "phone", "managerName", "floorSpace", "createdOn", "updatedOn"];

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

  editWarehouse(warehouse:WarehouseModel): void {
    this.dialogRef = this._matDialog.open(WherehouseDetailComponent, {
      panelClass: 'route-form-dialog',
      data: {
        warehouse: warehouse,
        action: "edit"
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
            this.saveWarehouse(formData.getRawValue());


            break;
          /**
           * Delete
           */
          case 'delete':

            this.deleteWarehouse(formData.getRawValue());

            break;
        }
      });
  }

  addNewWarehouse(): void {

    let warehouse: WarehouseModel = new WarehouseModel();
    warehouse.id = 0;
    warehouse.address = "";
    warehouse.phone = "";
    warehouse.floorSpace = 0;
   /*warehouse.createdOn = "";
    warehouse.updateOn = "";*/
    warehouse.isActive = true;

    this.dialogRef = this._matDialog.open(WherehouseDetailComponent, {
      panelClass: 'route-form-dialog',
      data: {
        warehouse: warehouse,
        action: "add"
      }
    });

    this.dialogRef.afterClosed()
      .subscribe(response => {
        if (!response) {
          return;
        }

        const formData: FormGroup = response;
        this.saveWarehouse(formData.getRawValue());

      });
  }

  loadWarehouses() {
    this._fuseProgressBarService.show();
    this._warehouseService.GetAllWarehouses()
      .subscribe(response => {
        console.log(response);
        
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


saveWarehouse(vm: WarehouseModel) {
  this._fuseProgressBarService.show();
  this._warehouseService.SaveWarehouse(vm)
    .subscribe(response => {

      this._fuseProgressBarService.hide();
      if (response.isSuccess) {
        this._snackBar.open(response.message, 'Success', {
          duration: 2500,
          horizontalPosition: this.horizontalPosition,
          verticalPosition: this.verticalPosition,
        });

        this.loadWarehouses();;
      }
      else {
        this._snackBar.open(response.message, 'Error', {
          duration: 2500,
          horizontalPosition: this.horizontalPosition,
          verticalPosition: this.verticalPosition,
        });
      }
    }, error => {
      this._fuseProgressBarService.hide();
      this._snackBar.open("Network error has been occured. Please try again.", 'Error', {
        duration: 2500,
        horizontalPosition: this.horizontalPosition,
        verticalPosition: this.verticalPosition,
      });
    });
}

deleteWarehouse(warehouse:WarehouseModel){

  this.confirmDialogRef = this._matDialog.open(FuseConfirmDialogComponent, {
    disableClose: false
  });

  this.confirmDialogRef.componentInstance.confirmMessage = 'Are you sure you want to delete this record?';

  this.confirmDialogRef.afterClosed().subscribe(result => {
    if (result) {
      this._fuseProgressBarService.show();
      this._warehouseService.delete(warehouse.id)
        .subscribe(response => {

          this._fuseProgressBarService.hide();
          if (response.isSuccess) {
            this._snackBar.open(response.message, 'Success', {
              duration: 2500,
              horizontalPosition: this.horizontalPosition,
              verticalPosition: this.verticalPosition,
            });

            this.loadWarehouses();
          }
          else {
            this._snackBar.open(response.message, 'Error', {
              duration: 2500,
              horizontalPosition: this.horizontalPosition,
              verticalPosition: this.verticalPosition,
            });
          }
        }, error => {
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

 

}
