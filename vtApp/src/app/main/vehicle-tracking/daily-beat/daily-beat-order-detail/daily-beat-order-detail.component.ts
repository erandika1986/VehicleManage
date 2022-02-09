import { Component, ElementRef, Inject, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { fuseAnimations } from '@fuse/animations';
import { FuseConfirmDialogComponent } from '@fuse/components/confirm-dialog/confirm-dialog.component';
import { FuseProgressBarService } from '@fuse/components/progress-bar/progress-bar.service';
import { DailyVehicleBeatModel } from 'app/models/dialy-beat/daily-vehicle-beat.model';
import { BasicSalesOrderDetailModel } from 'app/models/sales-order/basic.sales.order.detail.model';
import { DailyBeatService } from 'app/services/daily-beats/daily-beat.service';
import { SalesOrderService } from 'app/services/sales-order/sales-order.service';

@Component({
  selector: 'daily-beat-order-detail',
  templateUrl: './daily-beat-order-detail.component.html',
  styleUrls: ['./daily-beat-order-detail.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations,
})
export class DailyBeatOrderDetailComponent implements OnInit {

  horizontalPosition: MatSnackBarHorizontalPosition = 'right';
  verticalPosition: MatSnackBarVerticalPosition = 'top';

  model:DailyVehicleBeatModel;
  newSalesOrders:BasicSalesOrderDetailModel[]=[];
  assignedSalesOrders:BasicSalesOrderDetailModel[]=[];

  dialogRef: any;
  confirmDialogRef: MatDialogRef<FuseConfirmDialogComponent>;
  
  displayedColumns = ["buttons", "orderNumber","ownerName","route","total","orderDate","statusInText"];

  isReadOnly:boolean;

  @ViewChild(MatPaginator) paginator: MatPaginator;

  @ViewChild(MatSort) sort: MatSort;

  @ViewChild('input') input: ElementRef;


  constructor(public matDialogRef: MatDialogRef<DailyBeatOrderDetailComponent>,
    private _fuseProgressBarService: FuseProgressBarService,
    private _dailyBeatService:DailyBeatService,
    private _matDialog: MatDialog,
    private _salesOrder:SalesOrderService,
    private _snackBar: MatSnackBar,
    @Inject(MAT_DIALOG_DATA) private _data: any) {

      console.log(_data);
      
      this.model = _data.model;
      this.isReadOnly = _data.isReadOnly;
     }

  ngOnInit(): void {
    this.getNewSalesOrders();
  }

  getNewSalesOrders()
  {
      this._fuseProgressBarService.show();
      this._salesOrder.getNewSalesOrdersForSelectedDailyBeat(this.model.id)
        .subscribe(response=>{

          this.newSalesOrders = response;
          this.getAssignedSalesOrders();
        },error=>{

          this._fuseProgressBarService.hide();
        });
  }

  getAssignedSalesOrders()
  {
    this._salesOrder.getSalesOrdersForSelectedDailyBeat(this.model.id)
    .subscribe(response=>{

      this.assignedSalesOrders = response;
      this._fuseProgressBarService.hide();

    },error=>{
      this._fuseProgressBarService.hide();
    });
  }

  assignSaleOrderToDailyBeat(item:BasicSalesOrderDetailModel)
  {
    this._salesOrder.addSalesOrderToSelectedDailyBeat(item.id,this.model.id)
      .subscribe(response=>{

        this.getNewSalesOrders();
        
      },error=>{

      })
  }

  removeSaleOrderFromDailyBeat(item:BasicSalesOrderDetailModel)
  {

    this.confirmDialogRef = this._matDialog.open(FuseConfirmDialogComponent, {
      disableClose: false
    });

    this.confirmDialogRef.componentInstance.confirmMessage = 'Are you sure you want to delete this record?';

    this.confirmDialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._fuseProgressBarService.show();

        this._salesOrder.deleteSaleOrderFromDailyBeat(item.dailyVehicleBeatOrderId)
        .subscribe(response=>{
  
          if (response.isSuccess) {
            this._snackBar.open(response.message, 'Success', {
              duration: 2500,
              horizontalPosition: this.horizontalPosition,
              verticalPosition: this.verticalPosition,
            });

            this._fuseProgressBarService.hide();
            this.getNewSalesOrders();
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

}
