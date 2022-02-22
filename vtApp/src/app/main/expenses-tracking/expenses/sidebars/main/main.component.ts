import { Component, OnInit } from '@angular/core';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';
import { DropDownModel } from './../../../../../models/common/drop-down.modal';
import { FormGroup } from '@angular/forms';
import { FormControl } from '@angular/forms';
import { Subject } from 'rxjs';
import { ExpensesService } from './../../../../../services/expenses/expenses.service';

@Component({
  selector: 'expenses-main-sidebar',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit {

  user: any;
  filterBy: string;
  vehicles:DropDownModel[]=[];
  expensesCategories:DropDownModel[]=[];
  vehicleExpenses:DropDownModel[]=[];

  filterForm:FormGroup;

  private _unsubscribeAll: Subject<any>;

  constructor(
    private _expensesService:ExpensesService
    ) {
    this._unsubscribeAll = new Subject();
   }

  ngOnInit(): void {
    this.getExpesesMasterData();
    this.filterForm = this.createFilterForm();
  }

  ngOnDestroy(): void
  {
      this._unsubscribeAll.next();
      this._unsubscribeAll.complete();
  }

  createFilterForm(): FormGroup {
    return new FormGroup({
   
      selectedExpenseCategoryId: new FormControl(0),
      fromDate: new FormControl(new Date()),
      toDate: new FormControl(new Date())
    });
  }

  getExpesesMasterData()
  {
    this._expensesService.getExpensesMasterData()
      .subscribe(response=>{
        console.log(response);
        
        let firstItem = new DropDownModel();
        firstItem.id=0;
        firstItem.name="--All--";
        this._expensesService.onExpensesMasterDataRecieved.next(response);
        this.vehicles = response.vehicles;

        this.expensesCategories = response.expensesCategories;
        this.expensesCategories.unshift(firstItem);

        this.vehicleExpenses = response.vehicleExpenses;
       
      })
  }
  
  dropdownFilterChanged()
  {
    this._expensesService.onFilterChanged.next(this.filterForm.getRawValue());
  }

  addEvent(type: string, event: MatDatepickerInputEvent<Date>) {
    this._expensesService.onFilterChanged.next(this.filterForm.getRawValue());
  }

}
