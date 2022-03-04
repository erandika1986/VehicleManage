import { DataSource } from '@angular/cdk/table';
import { BehaviorSubject, merge, Observable, of as observableOf} from "rxjs";
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { startWith, switchMap, catchError, map } from 'rxjs/operators';
import { ProductReturnFilterModel } from 'app/models/product-return/product.return.filter.model';
import { ProductReturnService } from 'app/services/inventory/product-return.service';
import { BasicProductReturnModel } from './basic.product.return.model';

export class ProductReturnDataSource extends DataSource<any>
{
    public _filterChange = new BehaviorSubject(true);
    public _saveRecord = new BehaviorSubject(true);
    private _searchTextChange = new BehaviorSubject('');
    private _pageSize = new BehaviorSubject(25);
    private _expensesCategory = new BehaviorSubject(1);
    private _totalRecordNumber= new BehaviorSubject(0);
    productReturnFilter:ProductReturnFilterModel;

    constructor(private _productReturnService: ProductReturnService,
        private _matPaginator: MatPaginator,
        private _matSort: MatSort)
    {
        super()
        this.productReturnFilter = new ProductReturnFilterModel();
        
        this._productReturnService.onFilterChanged.subscribe(response=>{

            this.productReturnFilter.selectedClientId = response.selectedClientId;
            this.productReturnFilter.selectedProductCategoryId = response.selectedProductCategoryId;
            this.productReturnFilter.selectedProductId = response.selectedProductId;
            this.productReturnFilter.selectedProductReturnStatus = response.selectedProductReturnStatus;
            this.productReturnFilter.selectedProductSubCategoryId = response.selectedProductSubCategoryId;
            this.productReturnFilter.selectedWarehouseId = response.selectedWarehouseId;
            
            this._matPaginator.pageIndex = 0;
            this._filterChange.next(true);
        });

        this._matSort.sortChange.subscribe(() => this._matPaginator.pageIndex = 0);
    }

    connect(): Observable<BasicProductReturnModel[]>
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
              this.productReturnFilter.currentPage = this._matPaginator.pageIndex+1;
              this.productReturnFilter.pageSize = this.pageSize;

            return this._productReturnService!
            .getAllVehicleReturnRecord(this.productReturnFilter)
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