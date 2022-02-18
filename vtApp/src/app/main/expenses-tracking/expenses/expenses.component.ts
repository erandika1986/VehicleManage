import { Component, OnInit, ViewEncapsulation, ElementRef, ViewChild } from '@angular/core';
import { fuseAnimations } from './../../../../@fuse/animations/index';
import { FormGroup } from '@angular/forms';
import {  fromEvent, Subject } from 'rxjs';
import { Router } from '@angular/router';
import { FuseProgressBarService } from './../../../../@fuse/components/progress-bar/progress-bar.service';
import { FuseSidebarService } from './../../../../@fuse/components/sidebar/sidebar.service';
import { MatDialog } from '@angular/material/dialog';
import { takeUntil, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { VehicleBeatMasterDataModel } from './../../../models/dialy-beat/vehicle-beat-master-data.model';
import { DailyBeatService } from 'app/services/daily-beats/daily-beat.service';
import { ExpensesService } from './../../../services/expenses/expenses.service';
import { ExpensesMasterDataModel } from './../../../models/expenses/expenses.master.data.model';
import { ExpensesDetailComponent } from './expenses-detail/expenses-detail.component';

@Component({
  selector: 'app-expenses',
  templateUrl: './expenses.component.html',
  styleUrls: ['./expenses.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations   : fuseAnimations
})
export class ExpensesComponent implements OnInit {

 
  @ViewChild('filter', {static: true})
  filter: ElementRef;

  dialogRef: any;
  hasSelectedContacts: boolean;

  private _unsubscribeAll: Subject<any>;
  expensesMasterData:ExpensesMasterDataModel;

  constructor(
    private _expensesService:ExpensesService,
    private _fuseSidebarService: FuseSidebarService,
    private _fuseProgressBarService: FuseProgressBarService,
    public _router: Router,
    private _matDialog: MatDialog) {
      // Set the private defaults
      this._unsubscribeAll = new Subject();
     }

  ngOnInit(): void {

    fromEvent(this.filter.nativeElement, 'keyup')
    .pipe(
        takeUntil(this._unsubscribeAll),
        debounceTime(150),
        distinctUntilChanged()
    )
    .subscribe(() => {

      this._expensesService.onSearchTextChanged.next(this.filter.nativeElement.value)
    });

    this._expensesService.onExpensesMasterDataRecieved.subscribe(response=>{
      this.expensesMasterData = response;
    })
  }

  ngOnDestroy(): void
  {
      // Unsubscribe from all subscriptions
      this._unsubscribeAll.next();
      this._unsubscribeAll.complete();
  }

  toggleSidebar(name): void
  {
      this._fuseSidebarService.getSidebar(name).toggleOpen();
  }

  addNew()
  {
    console.log(this.expensesMasterData);
    
    this.dialogRef = this._matDialog.open(ExpensesDetailComponent, {
      panelClass: 'expense-form-dialog',
      data      : {
          masterData:this.expensesMasterData,
          action: 'new'
      }
  });

  this.dialogRef.afterClosed()
      .subscribe((response: FormGroup) => {
        console.log("Add New");     
        console.log(response);
          if ( !response )
          {
              return;
          }
       
          
          this._expensesService.onExpensesDetailsSaved.next(response.getRawValue());
      });
  }

}
