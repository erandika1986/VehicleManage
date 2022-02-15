import { DataSource } from '@angular/cdk/table';
import { BehaviorSubject, merge, Observable, of as observableOf} from "rxjs";
import { ExpensesFilterModel } from './expenses.filter.model';
import { ExpensesService } from './../../services/expenses/expenses.service';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { BasicExpensesModel } from './basic-expenses.model';
import { startWith, switchMap, catchError, map } from 'rxjs/operators';
export class ExpensesDataSource extends DataSource<any>{

    public _filterChange = new BehaviorSubject(true);
    public _saveRecord = new BehaviorSubject(true);
    private _searchTextChange = new BehaviorSubject('');
    private _pageSize = new BehaviorSubject(25);
    private _expensesCategory = new BehaviorSubject(1);
    private _totalRecordNumber= new BehaviorSubject(0);
    expensesFilter:ExpensesFilterModel;

    constructor(private _expensesService: ExpensesService,
        private _matPaginator: MatPaginator,
        private _matSort: MatSort)
    {
        super();
        let fromDate = new Date();
        let toDate = new Date();
        
        this.expensesFilter = new ExpensesFilterModel();

        this.expensesFilter.fromDate = fromDate;
        this.expensesFilter.toDate = toDate;

        this.expensesFilter.fromYear = fromDate.getFullYear();
        this.expensesFilter.fromMonth = fromDate.getMonth()+1;
        this.expensesFilter.fromDay = fromDate.getDate();
        
        this.expensesFilter.toYear = toDate.getFullYear();
        this.expensesFilter.toMonth = toDate.getMonth()+1;
        this.expensesFilter.toDay = toDate.getDate();
       
        this._expensesService.onFilterChanged.subscribe(response=>{

            this.expensesFilter.fromDate = response.fromDate;
            this.expensesFilter.toDate = response.toDate;

            this.expensesFilter.fromYear = response.fromDate.getFullYear();
            this.expensesFilter.fromMonth = response.fromDate.getMonth()+1;
            this.expensesFilter.fromDay = response.fromDate.getDate();

            this.expensesFilter.toYear = response.toDate.getFullYear();
            this.expensesFilter.toMonth = response.toDate.getMonth()+1;
            this.expensesFilter.toDay = response.toDate.getDate();

            this.expensesFilter.selectedExpenseCategoryId = response.selectedExpenseCategoryId;
            this._matPaginator.pageIndex = 0;
            this._filterChange.next(true);
        });

        this._matSort.sortChange.subscribe(() => this._matPaginator.pageIndex = 0);
    }

    connect(): Observable<BasicExpensesModel[]>
    {
        console.log('Connecting....');

        const displayDataChanges = [
            this._matPaginator.page,
            this._searchTextChange,
            this._matSort.sortChange,
            this._filterChange,
            this._saveRecord
        ];

        return merge(...displayDataChanges)
        .pipe(
          startWith({}),
          switchMap(() => {
              this.expensesFilter.currentPage = this._matPaginator.pageIndex+1;
              this.expensesFilter.pageSize = this.pageSize;

            return this._expensesService!
            .gellAllExpeses(this.expensesFilter)
            .pipe(catchError(() => observableOf(null)));
          }),
          map(data => {
  
            if (data === null) {
              return [];
            }
            this.totalRecord =data.totalRecordCount;
            return data.data;
          }),
        )
    }

    disconnect(): void
    {

    }


    get filter(): string
    {
        return this._searchTextChange.value;
    }

    set filter(filter: string)
    {
        this._searchTextChange.next(filter);
    }

    get pageSize():number
    {
        return this._pageSize.value;
    }

    set pageSize(value:number)
    {
        this._pageSize.next(value);
    }

    get expenseCategory():number
    {
        return this._expensesCategory.value;
    }

    set expenseCategory(value:number)
    {
        this._expensesCategory.next(value);
    }

    get totalRecord():number
    {
        return this._totalRecordNumber.value;
    }

    set totalRecord(value:number)
    {
        this._totalRecordNumber.next(value);
    }
}
