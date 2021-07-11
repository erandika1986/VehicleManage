import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { ResponseModel } from 'app/models/common/response.model';
import { PurchaseOrderMasterData } from 'app/models/po/purchase.order.master.data.model';
import { PurchaseOrder } from 'app/models/po/purchase.order.model';
import { PurchaseOrderSummary } from 'app/models/po/purchase.order.summary.model';
import { environment } from 'environments/environment';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PoService {

  onFilterChanged: Subject<any>;
  onSearchTextChanged: Subject<any>;
  onPaginationChanged: Subject<PageEvent>;
  onPageIndexChanged: Subject<any>;
  onNewRecordAdded: Subject<any>;
  onPODetailChanged:Subject<any>;
  
  constructor(private httpClient: HttpClient) { 
    this.onSearchTextChanged = new Subject();
    this.onFilterChanged = new Subject();
    this.onPaginationChanged = new Subject();
    this.onPageIndexChanged = new Subject();
    this.onNewRecordAdded = new Subject();
    this.onPODetailChanged = new Subject();
  }


  getAll(): Observable<PurchaseOrderSummary[]> {
    return this.httpClient.
      get<PurchaseOrderSummary[]>(environment.apiUrl + 'PurchaseOrder');
  }
  
  getById(id: number): Observable<PurchaseOrder> {
    return this.httpClient.
      get<PurchaseOrder>(environment.apiUrl + 'PurchaseOrder/' + id);
  }

  save(vm: PurchaseOrder): Observable<ResponseModel> {
    return this.httpClient.
      post<ResponseModel>(environment.apiUrl + 'PurchaseOrder', vm);
  }

  delete(id: number): Observable<ResponseModel> {
    return this.httpClient.
      delete<ResponseModel>(environment.apiUrl + 'PurchaseOrder/' + id);
  }

  getPurchaseOrderMasterData(): Observable<PurchaseOrderMasterData> {
    return this.httpClient.
      get<PurchaseOrderMasterData>(environment.apiUrl + 'PurchaseOrder/getPurchaseOrderMasterData');
  }

  getPONumber(): Observable<any> {
    return this.httpClient.
      get<any>(environment.apiUrl + 'PurchaseOrder/getPONumber');
  }
}
