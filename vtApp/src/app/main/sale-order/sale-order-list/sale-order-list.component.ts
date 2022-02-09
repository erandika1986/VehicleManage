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
import { BasicSalesOrderDetailModel } from 'app/models/sales-order/basic.sales.order.detail.model';
import { SalesOrderFilter } from 'app/models/sales-order/sales.order.filter';
import { SalesOrderService } from 'app/services/sales-order/sales-order.service';

@Component({
  selector: 'sale-order-list',
  templateUrl: './sale-order-list.component.html',
  styleUrls: ['./sale-order-list.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations   : fuseAnimations
})
export class SaleOrderListComponent implements OnInit {

  horizontalPosition: MatSnackBarHorizontalPosition = 'right';
  verticalPosition: MatSnackBarVerticalPosition = 'top';

  dataSource = new MatTableDataSource([]);
  salesOrderFilter:SalesOrderFilter;

  dialogRef: any;
  confirmDialogRef: MatDialogRef<FuseConfirmDialogComponent>;
  
  displayedColumns = ["buttons", "orderNumber","statusInText","total","orderDate","ownerName","route","createdOn"];

  @ViewChild(MatPaginator) paginator: MatPaginator;

  @ViewChild(MatSort) sort: MatSort;

  @ViewChild('input') input: ElementRef;

  constructor(private _route: ActivatedRoute,
    private _fuseProgressBarService: FuseProgressBarService,
    public _router: Router,
    private _matDialog: MatDialog,
    private _snackBar: MatSnackBar,
    private _salesOrderService:  SalesOrderService) {

      this.salesOrderFilter = new SalesOrderFilter();
      this.salesOrderFilter.selectedCustomerId=0;
      this.salesOrderFilter.selectedRouteId=0;
      this.salesOrderFilter.selectedSalesPersonId=0;
      this.salesOrderFilter.selectedStatus=0;

     }

  ngOnInit(): void {

    this._salesOrderService.onFilterChanged.subscribe((response:SalesOrderFilter)=>{
      this.salesOrderFilter=response;
      this.loadAll();
    });

    this.loadAll();
  }


  loadAll() {
    this._fuseProgressBarService.show();
    this._salesOrderService.getAllSalesOrders(this.salesOrderFilter)
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

  editSalesOrder(item:BasicSalesOrderDetailModel)
  {
    this._salesOrderService.onClickViewOnly.next(false);
    this._router.navigate(['sale-order/list/' + item.id ]);
  }

  deleteSalesOrder(item:BasicSalesOrderDetailModel)
  {
    this.confirmDialogRef = this._matDialog.open(FuseConfirmDialogComponent, {
      disableClose: false
    });

    this.confirmDialogRef.componentInstance.confirmMessage = 'Are you sure you want to delete this record?';

    this.confirmDialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._fuseProgressBarService.show();
        this._salesOrderService. deleteSalesOrder(item.id)
          .subscribe(response => {

            this._fuseProgressBarService.hide();
            if (response.isSuccess) {
              this._snackBar.open(response.message, 'Success', {
                duration: 2500,
                horizontalPosition: this.horizontalPosition,
                verticalPosition: this.verticalPosition,
              });

              this.loadAll();
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

  viewSalesOrder(item:BasicSalesOrderDetailModel)
  {
    this._salesOrderService.onClickViewOnly.next(true);
    this._router.navigate(['sale-order/list/' + item.id ]);
  }

}
