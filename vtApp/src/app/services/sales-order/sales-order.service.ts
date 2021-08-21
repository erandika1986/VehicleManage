import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ResponseModel } from 'app/models/common/response.model';
import { BasicSalesOrderDetailModel } from 'app/models/sales-order/basic.sales.order.detail.model';
import { SalesOrderFilter } from 'app/models/sales-order/sales.order.filter';
import { SalesOrderMasterDataModel } from 'app/models/sales-order/sales.order.master.data.model';
import { SalesOrderModel } from 'app/models/sales-order/sales.order.model';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SalesOrderService {

  constructor(private httpClient: HttpClient) { }

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

  delete(id: number): Observable<ResponseModel> {
    return this.httpClient.
      delete<ResponseModel>(environment.apiUrl + 'SalesOrder/deleteSalesOrder/' + id);
  }

  getSalesOrderMasterData(): Observable<SalesOrderMasterDataModel> {
    return this.httpClient.
      get<SalesOrderMasterDataModel>(environment.apiUrl + 'SalesOrder/getSalesOrderMasterData');
  }
}
