import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
import { ProductReturnMasterDataModel } from 'app/models/product-return/product.return.master.data.model';
import { Subject } from 'rxjs';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { FuseConfirmDialogComponent } from '@fuse/components/confirm-dialog/confirm-dialog.component';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { ProductReturnDataSource } from './../../../../models/product-return/product.return.datasource';
import { ProductReturnService } from 'app/services/inventory/product-return.service';
import { FuseProgressBarService } from '@fuse/components/progress-bar/progress-bar.service';
import { Router } from '@angular/router';
import { ProductReturnModel } from 'app/models/product-return/product.return.model';
import { BasicProductReturnModel } from 'app/models/product-return/basic.product.return.model';

@Component({
  selector: 'product-return-list',
  templateUrl: './product-return-list.component.html',
  styleUrls: ['./product-return-list.component.scss']
})
export class ProductReturnListComponent implements OnInit {

  horizontalPosition: MatSnackBarHorizontalPosition = 'right';
  verticalPosition: MatSnackBarVerticalPosition = 'top';

    ProductReturnMasterData:ProductReturnMasterDataModel;
    
    // Private
    private _unsubscribeAll: Subject<any>;

    @ViewChild(MatPaginator,{static:true}) 
    paginator: MatPaginator;
  
    @ViewChild(MatSort, {static:true}) 
    sort: MatSort;
  
    dialogRef: any;
    confirmDialogRef: MatDialogRef<FuseConfirmDialogComponent>;

    dataSource: ProductReturnDataSource;

    pageSizes:number[] =[25,50,75,100,200,500];

    displayedColumns = ["buttons",'selectedProduct','selectedClient','returnDate','createdByUser','updatedByUser'];

  constructor
  (
    private _productReturnService:ProductReturnService,
    private _fuseProgressBarService: FuseProgressBarService,
    public _router: Router,
    private _matDialog: MatDialog,
    private _snackBar: MatSnackBar) {
    this._unsubscribeAll = new Subject();

    this._productReturnService.onSearchTextChanged.subscribe(searchValue=>
    {

      if ( !this.dataSource )
      {
          return;
      }

      this.dataSource.filter = searchValue;

    });

     this._productReturnService.onProductReturnMasterDataRecieved.subscribe(response=>
    {
       this.ProductReturnMasterData = response;
     });

    this._productReturnService.onProductReturnDetailsSaved.subscribe(response=>
    {
      if(response != null){
           this.saveExpense(response);
    }
    

    });
   }

  ngOnInit(): void {

    this.dataSource = new ProductReturnDataSource(this._productReturnService, this.paginator, this.sort);

  }

  onChangePage(pe:PageEvent) {

    this.dataSource.pageSize = pe.pageSize;
  }

  editProductReturn(item:ProductReturnModel)
  {   
  }

  viewProducReturnDetails(item:ProductReturnModel)
  {
   
  }

  deleteProductReturn(item:BasicProductReturnModel)
  {
    this.confirmDialogRef = this._matDialog.open(FuseConfirmDialogComponent, {
      disableClose: false
    });

    this.confirmDialogRef.componentInstance.confirmMessage = 'Are you sure you want to delete this record?';

    this.confirmDialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._fuseProgressBarService.show();

        this._productReturnService.deleteProductReturn(item.id)
        .subscribe(response=>{
  
          if (response.isSuccess) {
            this._snackBar.open(response.message, 'Success', {
              duration: 2500,
              horizontalPosition: this.horizontalPosition,
              verticalPosition: this.verticalPosition,
            });

            this._fuseProgressBarService.hide();
            this.dataSource._saveRecord.next(true);
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
        })

      }
      this.confirmDialogRef = null;
    });
  }

  ngOnDestroy(): void
  {
      // Unsubscribe from all subscriptions
      this._unsubscribeAll.next();
      this._unsubscribeAll.complete();
  }

  saveExpense(item:ProductReturnModel)
  { 
      this._fuseProgressBarService.show();
      
      this._productReturnService.SaveProductReturn(item)
        .subscribe(response=>
        {
          this.dataSource._saveRecord.next(true);
          this._fuseProgressBarService.hide();

          this._snackBar.open(response.message, 'Success', {
            duration: 2500,
            horizontalPosition: this.horizontalPosition,
            verticalPosition: this.verticalPosition,
          });

        },error=>{
          this._fuseProgressBarService.hide();
        });
  }

}
