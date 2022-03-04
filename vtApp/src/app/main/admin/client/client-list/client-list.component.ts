import { Component, ElementRef, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { FuseConfirmDialogComponent } from '@fuse/components/confirm-dialog/confirm-dialog.component';
import { FuseProgressBarService } from '@fuse/components/progress-bar/progress-bar.service';
import { CustomerModel } from 'app/models/customer/customer.model';
import { CustomerService } from 'app/services/customer/customer.service';
import { lowerCase } from 'lodash';
import { ClientDetailComponent } from '../client-detail/client-detail.component';

@Component({
  selector: 'client-list',
  templateUrl: './client-list.component.html',
  styleUrls: ['./client-list.component.scss'],
  animations: fuseAnimations,
  encapsulation: ViewEncapsulation.None
  
})
export class ClientListComponent implements OnInit {
  horizontalPosition: MatSnackBarHorizontalPosition = 'right';
  verticalPosition: MatSnackBarVerticalPosition = 'top';

  dataSource = new MatTableDataSource([]);


  dialogRef: any;
  confirmDialogRef: MatDialogRef<FuseConfirmDialogComponent>;

  displayedColumns = ["buttons","id" ,"name", "address","contactNo1","email"];

  @ViewChild(MatPaginator) paginator: MatPaginator;

  @ViewChild(MatSort) sort: MatSort;

  @ViewChild('input') input: ElementRef;


  constructor(private _route: ActivatedRoute,
    private _customerService: CustomerService,
    private _fuseProgressBarService: FuseProgressBarService,
    private _matDialog: MatDialog,
    private _snackBar: MatSnackBar,
    public _router: Router) { }

  ngOnInit(): void {

    this.loadCustomer();
  }




  editCustomer(customer: CustomerModel): void {

    console.log(customer);
    
    this.dialogRef = this._matDialog.open(ClientDetailComponent, {
      panelClass: 'client-form-dialog',
      data: {
        customer: customer,
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
            this.saveCustomer(formData.getRawValue());


            break;
          /**
           * Delete
           */
          case 'delete':

            this.deleteCustomer(formData.getRawValue());

            break;
        }
      });

  }

  addNewClient(): void {

    let customer: CustomerModel = new CustomerModel();
    customer.id = 0;
    customer.name = "";
    customer.priority = 0;
    customer.address = "";
    customer.isActive = true;
    customer.contactNo1 = "";
    customer.contactNo2 = "";
    customer.description = "";
    customer.email = "";
    customer.latitude = 0;
    customer.longitude = 0;
    customer.routeId = 0;

    this.dialogRef = this._matDialog.open(ClientDetailComponent, {
      panelClass: 'client-form-dialog',
      data: {
        customer: customer,
        action: "add"
      }
    });

    this.dialogRef.afterClosed()
      .subscribe(response => {
        if (!response) {
          return;
        }

        const formData: FormGroup = response;
        this.saveCustomer(formData.getRawValue());

      });
  }


  loadCustomer() {
    this._fuseProgressBarService.show();
    this._customerService.getAllCustomers()
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


  saveCustomer(vm: CustomerModel) {
    this._fuseProgressBarService.show();
    this._customerService.saveCustomer(vm)
      .subscribe(response => {

        this._fuseProgressBarService.hide();
        if (response.isSuccess) {
          this._snackBar.open(response.message, 'Success', {
            duration: 2500,
            horizontalPosition: this.horizontalPosition,
            verticalPosition: this.verticalPosition,
          });

          this.loadCustomer();;
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


  deleteCustomer(customer: CustomerModel) {
    this.confirmDialogRef = this._matDialog.open(FuseConfirmDialogComponent, {
      disableClose: false
    });

    this.confirmDialogRef.componentInstance.confirmMessage = 'Are you sure you want to delete this record?';

    this.confirmDialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._fuseProgressBarService.show();
        this._customerService.delete(customer.id)
          .subscribe(response => {

            this._fuseProgressBarService.hide();
            if (response.isSuccess) {
              this._snackBar.open(response.message, 'Success', {
                duration: 2500,
                horizontalPosition: this.horizontalPosition,
                verticalPosition: this.verticalPosition,
              });

              this.loadCustomer();
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