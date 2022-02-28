import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ExpensesFilterModel } from './../../models/expenses/expenses.filter.model';
import { Subject, Observable, BehaviorSubject } from 'rxjs';
import { ExpensesPaginatedItemsModel } from './../../models/expenses/expenses-paginated-items.model';
import { ExpensesMasterDataModel } from './../../models/expenses/expenses.master.data.model';
import { environment } from 'environments/environment';
import { ExpensesModel } from 'app/models/expenses/expenses.model';
import { ResponseModel } from 'app/models/common/response.model';
import { Upload } from './../../models/common/upload';
import { upload } from 'app/models/common/upload';

@Injectable({
  providedIn: 'root'
})
export class ExpensesService {

  onFilterChanged: Subject<ExpensesFilterModel>;
  onSearchTextChanged : Subject<string>;
  onExpensesMasterDataRecieved:Subject<ExpensesMasterDataModel>;  
  onExpensesDetailsSaved:Subject<any>;
  onClickViewOnly:BehaviorSubject<boolean>;


  constructor(
    private httpClient:HttpClient,
  ) {
    this.onFilterChanged = new Subject();
    this.onSearchTextChanged =  new Subject();
    this.onExpensesMasterDataRecieved = new Subject();
    this.onExpensesDetailsSaved = new Subject();
    this.onClickViewOnly = new BehaviorSubject(true);
   }

  gellAllExpeses(filter:ExpensesFilterModel): Observable<ExpensesPaginatedItemsModel>{
    return this.httpClient
      .post<ExpensesPaginatedItemsModel>
            (environment.apiUrl + 'Expense/getAllExpenses',filter);
  }
  
  getExpensesMasterData():Observable<ExpensesMasterDataModel>{
    return this.httpClient
      .get<ExpensesMasterDataModel>
            (environment.apiUrl+'Expense/getExpensesMasterData');
  }

  getExpensesById(id:number,expenseCategoryId:number):Observable<ExpensesModel>{
    return this.httpClient
      .get<ExpensesModel>
            (environment.apiUrl+'Expense/getExpenseById' + '/' + id + '/' + expenseCategoryId);
  }

  saveExpenseDetail(vm: ExpensesModel): Observable<ResponseModel> {
    return this.httpClient.
      post<ResponseModel>
            (environment.apiUrl + 'Expense/saveExpense', vm);
  }

  deleteExpense(id:number):Observable<ResponseModel>{
    return this.httpClient
      .delete<ResponseModel>
            (environment.apiUrl + 'Expense/deleteExpense' + '/' + id);
  }

  uploadExpenseReceiptImage(data: FormData): Observable<Upload> {
    return this.httpClient.post
                        (environment.apiUrl + 'Expense/uploadExpenseReceiptImage',
                         data,{reportProgress: true,observe: 'events'}).pipe(upload());;
  }

  downloadExpenseReceiptImage(expenseId :number, id: number): Observable<any> {
    return this.httpClient.get<any>
                      (environment.apiUrl +'Expense/downloadExpenseReceiptImage/'+  expenseId +"/" + id,
                      {headers:{'filedownload':''}, observe: 'events',reportProgress:true });
  }

  deleteExpenseReciptImage(expenseId:number, id:number):Observable<ResponseModel>{
    return this.httpClient.delete<ResponseModel>
                    (environment.apiUrl + 'Expense/deleteExpenseReceiptImage/' + expenseId + "/" + id);
  }
}
