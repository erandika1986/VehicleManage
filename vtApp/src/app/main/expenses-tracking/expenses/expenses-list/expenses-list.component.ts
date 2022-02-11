import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
import { VehicleBeatMasterDataModel } from './../../../../models/dialy-beat/vehicle-beat-master-data.model';
import { Subject } from 'rxjs';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { FuseConfirmDialogComponent } from './../../../../../@fuse/components/confirm-dialog/confirm-dialog.component';
import { DailyBeatDataSource } from './../../../../models/dialy-beat/daily-beat-datasource';
import { FuseProgressBarService } from './../../../../../@fuse/components/progress-bar/progress-bar.service';
import { DailyVehicleBeatModel } from 'app/models/dialy-beat/daily-vehicle-beat.model';
import { DailyBeatEditModelComponent } from 'app/main/vehicle-tracking/daily-beat/daily-beat-edit-model/daily-beat-edit-model.component';
import { ExpensesEditModelComponent } from './../expenses-edit-model/expenses-edit-model.component';
import { FormGroup } from '@angular/forms';
import { DailyBeatOrderDetailComponent } from './../../../vehicle-tracking/daily-beat/daily-beat-order-detail/daily-beat-order-detail.component';
import { DailyBeatService } from 'app/services/daily-beats/daily-beat.service';
import { ExpensesDataSource } from './../../../../models/expenses/expenses-datasource';
import { ExpensesService } from './../../../../services/expenses/expenses.service';

@Component({
  selector: 'expenses-list',
  templateUrl: './expenses-list.component.html',
  styleUrls: ['./expenses-list.component.scss']
})
export class ExpensesListComponent implements OnInit {

  horizontalPosition: MatSnackBarHorizontalPosition = 'right';
  verticalPosition: MatSnackBarVerticalPosition = 'top';

    masterData:VehicleBeatMasterDataModel;
    // Private
    private _unsubscribeAll: Subject<any>;

    @ViewChild(MatPaginator,{static:true}) 
    paginator: MatPaginator;
  
    @ViewChild(MatSort, {static:true}) 
    sort: MatSort;
  
    dialogRef: any;
    confirmDialogRef: MatDialogRef<FuseConfirmDialogComponent>;

    dataSource: ExpensesDataSource;

    pageSizes:number[] =[25,50,75,100,200,500];

    displayedColumns = ["buttons",'routeName', 'vehicleNumber','driverName', 'date', 'statusInText'];

  constructor(private _expensesService:ExpensesService,
    private _fuseProgressBarService: FuseProgressBarService,
    private _matDialog: MatDialog,
    private _snackBar: MatSnackBar) {
    this._unsubscribeAll = new Subject();
   }

  ngOnInit(): void {

    this.dataSource = new ExpensesDataSource(this._expensesService, this.paginator, this.sort);

    this._expensesService.onSearchTextChanged.subscribe(searchValue=>{

      if ( !this.dataSource )
      {
          return;
      }

      this.dataSource.filter = searchValue;

    });

    // this._expensesService.onMasterDataRecieved.subscribe(response=>{
    //   this.masterData = response;
    // });

  /*   this._expensesService.onDailyBeatSaved.subscribe(response=>{

      this.saveDailyBeat(response);
    }); */
    
  }

  onChangePage(pe:PageEvent) {
    this.dataSource.pageSize = pe.pageSize;
  }

  editDailyBeat(item:DailyVehicleBeatModel)
  {
    console.log(item);
    
    this.dialogRef = this._matDialog.open(ExpensesEditModelComponent, {
      panelClass: 'daily-beat-edit-form-dialog',
      data      : {
          masterData:this.masterData,
          data:item,
          action: 'edit'
      }
  });

  this.dialogRef.afterClosed()
      .subscribe((response: FormGroup) => {
          if ( !response )
          {
              return;
          }

          console.log(response);
          
          this.saveDailyBeat(response[1].getRawValue())
      });
  }

  deleteDailyBeat(item:DailyVehicleBeatModel)
  {
    this.confirmDialogRef = this._matDialog.open(FuseConfirmDialogComponent, {
      disableClose: false
    });

    this.confirmDialogRef.componentInstance.confirmMessage = 'Are you sure you want to delete this record?';

    this.confirmDialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._fuseProgressBarService.show();

        // this._expensesService.delete(item.id)
        // .subscribe(response=>{
  
        //   if (response.isSuccess) {
        //     this._snackBar.open(response.message, 'Success', {
        //       duration: 2500,
        //       horizontalPosition: this.horizontalPosition,
        //       verticalPosition: this.verticalPosition,
        //     });

        //     this._fuseProgressBarService.hide();
        //     this.dataSource._saveRecord.next(true);
        //   }
        //   else {
        //     this._snackBar.open(response.message, 'Error', {
        //       duration: 2500,
        //       horizontalPosition: this.horizontalPosition,
        //       verticalPosition: this.verticalPosition,
        //     });
        //   }   
        // },error=>{
        //   this._fuseProgressBarService.hide();
        //   this._snackBar.open("Network error has been occured. Please try again.", 'Error', {
        //     duration: 2500,
        //     horizontalPosition: this.horizontalPosition,
        //     verticalPosition: this.verticalPosition,
        //   });
        // })

      }
      this.confirmDialogRef = null;
    });
  }

  view(item:DailyVehicleBeatModel)
  {

  }

  manageSalesOrder(item:DailyVehicleBeatModel)
  {
    this.dialogRef = this._matDialog.open(DailyBeatOrderDetailComponent, {
      panelClass: 'daily-beat-order-detail',
      data      : {
        model:item,
        isReadOnly:item.status!=1?true:false
      }
  });
  }

  ngOnDestroy(): void
  {
      // Unsubscribe from all subscriptions
      this._unsubscribeAll.next();
      this._unsubscribeAll.complete();
  }

  saveDailyBeat(item:DailyVehicleBeatModel)
  {
      /* this._fuseProgressBarService.show();
      
      item.dateYear = item.date.getFullYear();
      item.dateMonth = item.date.getMonth()+1;
      item.dateDay = item.date.getDate();

      this._expensesService.saveDailyVehicleBeatRecord(item)
        .subscribe(response=>
        {
          this.dataSource._saveRecord.next(true);
          this._fuseProgressBarService.hide();
        },error=>{
          this._fuseProgressBarService.hide();
        }); */
  }
}
