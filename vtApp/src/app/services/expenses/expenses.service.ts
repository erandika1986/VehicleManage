import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { filter } from 'rxjs/operators';
import { ExpensesFilterModel } from './../../models/expenses/expenses.filter.model';
import { Subject, Observable } from 'rxjs';
import { ExpensesPaginatedItemsModel } from './../../models/expenses/expenses-paginated-items.model';
import { environment } from './../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ExpensesService {

  onFilterChanged: Subject<ExpensesFilterModel>;
  onSearchTextChanged : Subject<string>;
 // onMasterDataRecieved:Subject<VehicleBeatMasterDataModel>;
  onDailyBeatSaved:Subject<any>;


  constructor(
    private httpClient:HttpClient,
  ) {
    this.onFilterChanged = new Subject();
    this.onSearchTextChanged =  new Subject();
   // this.onMasterDataRecieved = new Subject();
    this.onDailyBeatSaved = new Subject();
   }

  gellAllExpeses(filter:ExpensesFilterModel): Observable<ExpensesPaginatedItemsModel>{
    return this.httpClient.post<ExpensesPaginatedItemsModel>(environment.apiUrl + 'Expenses/getAllExpenses',filter);
  }

  
}
