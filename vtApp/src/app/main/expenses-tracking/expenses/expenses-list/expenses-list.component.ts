import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
import { Subject } from 'rxjs';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { FuseConfirmDialogComponent } from './../../../../../@fuse/components/confirm-dialog/confirm-dialog.component';
import { FuseProgressBarService } from './../../../../../@fuse/components/progress-bar/progress-bar.service';
import { DailyVehicleBeatModel } from 'app/models/dialy-beat/daily-vehicle-beat.model';
import { DailyBeatEditModelComponent } from 'app/main/vehicle-tracking/daily-beat/daily-beat-edit-model/daily-beat-edit-model.component';

import { FormGroup } from '@angular/forms';
import { DailyBeatOrderDetailComponent } from './../../../vehicle-tracking/daily-beat/daily-beat-order-detail/daily-beat-order-detail.component';
import { DailyBeatService } from 'app/services/daily-beats/daily-beat.service';
import { ExpensesDataSource } from './../../../../models/expenses/expenses-datasource';
import { ExpensesService } from './../../../../services/expenses/expenses.service';
import { ExpensesMasterDataModel } from './../../../../models/expenses/expenses.master.data.model';
import { BasicExpensesModel } from 'app/models/expenses/basic-expenses.model';
import { ExpensesModel } from 'app/models/expenses/expenses.model';
import { ExpensesDetailComponent } from '../expenses-detail/expenses-detail.component';
import { fuseAnimations } from '@fuse/animations';

@Component({
  selector: 'expenses-list',
  templateUrl: './expenses-list.component.html',
  styleUrls: ['./expenses-list.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations   : fuseAnimations
})
export class ExpensesListComponent implements OnInit {

  horizontalPosition: MatSnackBarHorizontalPosition = 'right';
  verticalPosition: MatSnackBarVerticalPosition = 'top';

    expensesMasterData:ExpensesMasterDataModel;
    
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

    displayedColumns = ["buttons",'expenseCategoryName','amount','createdBy'];

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

     this._expensesService.onExpensesMasterDataRecieved.subscribe(response=>{
       this.expensesMasterData = response;
     });

    this._expensesService.onExpensesDetailsSaved.subscribe(response=>{

      this.saveExpense(response);
    });
    
  }

  onChangePage(pe:PageEvent) {
    this.dataSource.pageSize = pe.pageSize;
  }

  editExpense(item:BasicExpensesModel)
  {
    
     this._expensesService.getExpensesById(item.id,item.expenseCategoryId).subscribe(response=>{
      
        console.log("dev");
        console.log(response);
        
        this.dialogRef = this._matDialog.open(ExpensesDetailComponent, {
          panelClass: 'expense-form-dialog',
          data      : {
              masterData:this.expensesMasterData,
              data: response,
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
          
          this.saveExpense(response[1].getRawValue())
      });
    
     })
    
  
 
  }

  deleteExpense(item:BasicExpensesModel)
  {
    this.confirmDialogRef = this._matDialog.open(FuseConfirmDialogComponent, {
      disableClose: false
    });

    this.confirmDialogRef.componentInstance.confirmMessage = 'Are you sure you want to delete this record?';

    this.confirmDialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._fuseProgressBarService.show();

        this._expensesService.deleteExpense(item.id)
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

  view(item:DailyVehicleBeatModel)
  {

  }


  ngOnDestroy(): void
  {
      // Unsubscribe from all subscriptions
      this._unsubscribeAll.next();
      this._unsubscribeAll.complete();
  }

  saveExpense(item:ExpensesModel)
  {
      this._fuseProgressBarService.show();
      
      item.expenseYear = item.expenseDate.getFullYear();
      item.expenseMonth = item.expenseDate.getMonth()+1;
      item.expenseDay = item.expenseDate.getDate();

      this._expensesService.saveExpenseDetail(item)
        .subscribe(response=>
        {
          this.dataSource._saveRecord.next(true);
          this._fuseProgressBarService.hide();
        },error=>{
          this._fuseProgressBarService.hide();
        });
  }
}
