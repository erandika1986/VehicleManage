import { FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
import { Component, OnInit, ViewEncapsulation, ViewChild, ElementRef } from '@angular/core';
import { fuseAnimations } from '@fuse/animations';
import { FuseConfirmDialogComponent } from '@fuse/components/confirm-dialog/confirm-dialog.component';
import { SupplierService } from 'app/services/supplier/supplier.service';
import { FuseProgressBarService } from '@fuse/components/progress-bar/progress-bar.service';
import { SupplierDetailComponent } from '../supplier-detail/supplier-detail.component';
import { SupplierModel } from 'app/models/supplier/supplier.model';

@Component({
  selector: 'supplier-list',
  templateUrl: './supplier-list.component.html',
  styleUrls: ['./supplier-list.component.scss'],
  animations: fuseAnimations,
  encapsulation: ViewEncapsulation.None
})
export class SupplierListComponent implements OnInit {

  horizontalPosition: MatSnackBarHorizontalPosition = 'right';
  verticalPosition: MatSnackBarVerticalPosition = 'top';

  dataSource = new MatTableDataSource([]);


  dialogRef: any;
  confirmDialogRef: MatDialogRef<FuseConfirmDialogComponent>;

  displayedColumns = ["buttons",  "name", "address", "phone1","phone2", 
                      "email1", "email2", "bank", "accountNo","branch","branchCode"];

  @ViewChild(MatPaginator) paginator: MatPaginator;

  @ViewChild(MatSort) sort: MatSort;

  @ViewChild('input') input: ElementRef;


  constructor(private _route: ActivatedRoute,
    private _supplierService: SupplierService,
    private _fuseProgressBarService: FuseProgressBarService,
    private _matDialog: MatDialog,
    private _snackBar: MatSnackBar,
    public _router: Router) { }

  ngOnInit(): void {

    this.loadSuppliers();
  }

  editSupplier(supplier:SupplierModel): void {
    this.dialogRef = this._matDialog.open(SupplierDetailComponent, {
      panelClass: 'route-form-dialog',
      data: {
        supplier: supplier,
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
            this.saveSupplier(formData.getRawValue());


            break;
          /**
           * Delete
           */
          case 'delete':

            this.deleteSupplier(formData.getRawValue());

            break;
        }
      });
  }

  addNewSupplier(): void {

    let supplier: SupplierModel = new SupplierModel();
    
    supplier.id = 0;
    supplier.name = "";
    supplier.address = "";
    supplier.description = "";
    supplier.email1 = "";
    supplier.email2 = "";
    supplier.phone1 = "";
    supplier.phone2 = "";
    supplier.bank = "";
    supplier.accountNo = "";
    supplier.branch = "";
    supplier.branchCode = "";
    supplier.isActive = true;

    this.dialogRef = this._matDialog.open(SupplierDetailComponent, {
      panelClass: 'route-form-dialog',
      data: {
        supplier: supplier,
        action: "add"
      }
    });

    this.dialogRef.afterClosed()
      .subscribe(response => {
        if (!response) {
          return;
        }

        const formData: FormGroup = response;
        this.saveSupplier(formData.getRawValue());

      });
  }

  loadSuppliers() {
    this._fuseProgressBarService.show();
    this._supplierService.GetAllSuppliers()
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


saveSupplier(vm: SupplierModel) {
  this._fuseProgressBarService.show();
  this._supplierService.SaveSupplier(vm)
    .subscribe(response => {

      this._fuseProgressBarService.hide();
      if (response.isSuccess) {
        this._snackBar.open(response.message, 'Success', {
          duration: 2500,
          horizontalPosition: this.horizontalPosition,
          verticalPosition: this.verticalPosition,
        });

        this.loadSuppliers();;
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

deleteSupplier(supplier:SupplierModel){

  this.confirmDialogRef = this._matDialog.open(FuseConfirmDialogComponent, {
    disableClose: false
  });

  this.confirmDialogRef.componentInstance.confirmMessage = 'Are you sure you want to delete this record?';

  this.confirmDialogRef.afterClosed().subscribe(result => {
    if (result) {
      this._fuseProgressBarService.show();
      this._supplierService.DeleteSupplier(supplier.id)
        .subscribe(response => {

          this._fuseProgressBarService.hide();
          if (response.isSuccess) {
            this._snackBar.open(response.message, 'Success', {
              duration: 2500,
              horizontalPosition: this.horizontalPosition,
              verticalPosition: this.verticalPosition,
            });

            this.loadSuppliers();
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
