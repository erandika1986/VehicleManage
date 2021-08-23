import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DropDownModel } from 'app/models/common/drop-down.modal';
import { ResponseModel } from 'app/models/common/response.model';
import { BasicSalesOrderDetailModel } from 'app/models/sales-order/basic.sales.order.detail.model';
import { SalesOrderFilter } from 'app/models/sales-order/sales.order.filter';
import { SalesOrderMasterDataModel } from 'app/models/sales-order/sales.order.master.data.model';
import { SalesOrderModel } from 'app/models/sales-order/sales.order.model';
import { SalesOrderNumber } from 'app/models/sales-order/sales.order.number.model';
import { environment } from 'environments/environment';
import { BehaviorSubject, Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SalesOrderService {

  onSalesOrderChanged: BehaviorSubject<any>;
  onSelectedSalesOrderChanged: BehaviorSubject<any>;
  onSalesOrderDataChanged: BehaviorSubject<any>;
  onSearchTextChanged: Subject<any>;
  onFilterChanged: Subject<any>;
  onMasterDataRecieved:Subject<any>;
  onSalesOrderUpdated:BehaviorSubject<any>;
  onNewSalesOrderAdded:Subject<any>;

  salesOrders: BasicSalesOrderDetailModel[];
  user: any;
  selectedUsers: string[] = [];

  searchText: string;
  filterBy: string;
  
  constructor(private httpClient: HttpClient) {
    this.onSalesOrderChanged = new BehaviorSubject([]);
    this.onSelectedSalesOrderChanged = new BehaviorSubject([]); 
    this.onSalesOrderDataChanged = new BehaviorSubject([]);
    this.onSearchTextChanged = new Subject();
    this.onFilterChanged = new Subject();
    this.onMasterDataRecieved = new Subject();
    this.onSalesOrderUpdated = new BehaviorSubject([]);
    this.onNewSalesOrderAdded = new Subject();
   }

  getMySalesOrders(filter:SalesOrderFilter): Observable<BasicSalesOrderDetailModel[]> {
    return this.httpClient.
      post<BasicSalesOrderDetailModel[]>(environment.apiUrl + 'SalesOrder/getMySalesOrders',filter);
  }

  getAllSalesOrders(filter:SalesOrderFilter): Observable<BasicSalesOrderDetailModel[]> {
    return this.httpClient.
      post<BasicSalesOrderDetailModel[]>(environment.apiUrl + 'SalesOrder/getAllSalesOrders',filter);
  }

  saveSalesOrder(vm: SalesOrderModel): Observable<ResponseModel> {
    return this.httpClient.
      post<ResponseModel>(environment.apiUrl + 'SalesOrder/saveSalesOrder', vm);
  }

  getSalesOrderById(id: number): Observable<SalesOrderModel> {
    return this.httpClient.
      get<SalesOrderModel>(environment.apiUrl + 'SalesOrder/getSalesOrderById/' + id);
  }

  delete(id: number): Observable<ResponseModel> {
    return this.httpClient.
      delete<ResponseModel>(environment.apiUrl + 'SalesOrder/deleteSalesOrder/' + id);
  }

  getSalesOrderMasterData(): Observable<SalesOrderMasterDataModel> {
    return this.httpClient.
      get<SalesOrderMasterDataModel>(environment.apiUrl + 'SalesOrder/getSalesOrderMasterData');
  }

  getCustomersByRouteId(id: number): Observable<DropDownModel[]> {
    return this.httpClient.
      get<DropDownModel[]>(environment.apiUrl + 'SalesOrder/getCustomersByRouteId/' + id);
  }

  getSalesOrderNumber(): Observable<SalesOrderNumber> {
    return this.httpClient.
      get<SalesOrderNumber>(environment.apiUrl + 'SalesOrder/getSalesOrderNumber');
  }
}
