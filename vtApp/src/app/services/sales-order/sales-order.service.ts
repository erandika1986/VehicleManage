import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { DropDownModel } from 'app/models/common/drop-down.modal';
import { ResponseModel } from 'app/models/common/response.model';
import { BasicSalesOrderDetailModel } from 'app/models/sales-order/basic.sales.order.detail.model';
import { ProductAvailabilityModel } from 'app/models/sales-order/product.availability.model';
import { SalesOrderFilter } from 'app/models/sales-order/sales.order.filter';
import { SalesOrderMasterDataModel } from 'app/models/sales-order/sales.order.master.data.model';
import { SalesOrderModel } from 'app/models/sales-order/sales.order.model';
import { SalesOrderNumber } from 'app/models/sales-order/sales.order.number.model';
import { SalesOrderProductModel } from 'app/models/sales-order/sales.order.product.model';
import { environment } from 'environments/environment';
import { BehaviorSubject, Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SalesOrderService {

  //Sales Order Details
  onFilterChanged: Subject<any>;
  onSearchTextChanged: Subject<any>;
  onPaginationChanged: Subject<PageEvent>;
  onPageIndexChanged: Subject<any>;
  onNewRecordAdded: Subject<any>;
  onSalesOrderDetailChanged:Subject<any>;


  onSalesOrderChanged: BehaviorSubject<any>;
  onSelectedSalesOrderChanged: BehaviorSubject<any>;
  onSalesOrderDataChanged: BehaviorSubject<any>;
  onMasterDataRecieved:Subject<any>;
  onSalesOrderUpdated:BehaviorSubject<any>;
  onNewSalesOrderAdded:Subject<any>;

  salesOrders: BasicSalesOrderDetailModel[];
  user: any;
  selectedUsers: string[] = [];

  searchText: string;
  filterBy: string;
  
  constructor(private httpClient: HttpClient) {

    this.onSearchTextChanged = new Subject();
    this.onFilterChanged = new Subject();
    this.onPaginationChanged = new Subject();
    this.onPageIndexChanged = new Subject();
    this.onNewRecordAdded = new Subject();
    this.onSalesOrderDetailChanged = new Subject();

    this.onSalesOrderChanged = new BehaviorSubject([]);
    this.onSelectedSalesOrderChanged = new BehaviorSubject([]); 
    this.onSalesOrderDataChanged = new BehaviorSubject([]);
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

  createNewSalesOrder(): Observable<number> {
    return this.httpClient.
      post<number>(environment.apiUrl + 'SalesOrder/createNewSalesOrder',null);
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

  getWarehouseProductAvailability(productId: number,salesOrderId:number): Observable<ProductAvailabilityModel[]> {
    return this.httpClient.
      get<ProductAvailabilityModel[]>(environment.apiUrl + 'SalesOrder/getWarehouseProductAvailability/' + productId +'/'+salesOrderId);
  }

  getSalesOrderNumber(): Observable<SalesOrderNumber> {
    return this.httpClient.
      get<SalesOrderNumber>(environment.apiUrl + 'SalesOrder/getSalesOrderNumber');
  }

  addProductToSalesOrder(productDetail:SalesOrderProductModel): Observable<ResponseModel> {
    return this.httpClient.
      post<ResponseModel>(environment.apiUrl + 'SalesOrder/addProductToSalesOrder',productDetail);
  }

  deleteSingleProductRoSalesOrder(productDetail:SalesOrderProductModel): Observable<ResponseModel> {
    return this.httpClient.
      post<ResponseModel>(environment.apiUrl + 'SalesOrder/deleteSingleProductRoSalesOrder',productDetail);
  }

  deleteSalesOrder(productId: number,salesOrderId:number): Observable<ResponseModel> {
    return this.httpClient.
      delete<ResponseModel>(environment.apiUrl + 'SalesOrder/deleteSalesOrder/' + productId +"/"+salesOrderId);
  }
}
