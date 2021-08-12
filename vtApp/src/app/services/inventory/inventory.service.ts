import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ResponseModel } from 'app/models/common/response.model';
import { InventoryBasicDetailModel } from 'app/models/inventory/inventory.basic.detail.model';
import { InventoryMasterDataModel } from 'app/models/inventory/inventory.master.data.model';
import { POInventoryReceievedDetailModel } from 'app/models/inventory/po.inventory.receieved.detail.model';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class InventoryService {

  constructor(private httpClient: HttpClient) { }

  getProductInvetorySummary(): Observable<InventoryBasicDetailModel[]> {
    return this.httpClient.
      get<InventoryBasicDetailModel[]>(environment.apiUrl + 'Inventory/getProductInvetorySummary');
  }

  getInventoryDetailsForPO(poId: number): Observable<POInventoryReceievedDetailModel> {
    return this.httpClient.
      get<POInventoryReceievedDetailModel>(environment.apiUrl + 'Inventory/getInventoryDetailsForPO' + poId);
  }

  addNewInventoryRecords(vm: POInventoryReceievedDetailModel): Observable<ResponseModel> {
    return this.httpClient.
      post<ResponseModel>(environment.apiUrl + 'Inventory/addNewInventoryRecords', vm);
  }

  delete(id: number): Observable<ResponseModel> {
    return this.httpClient.
      delete<ResponseModel>(environment.apiUrl + 'Inventory/deleteInventory' + id);
  }

  getInventoryMasterData(): Observable<InventoryMasterDataModel> {
    return this.httpClient.
      get<InventoryMasterDataModel>(environment.apiUrl + 'Inventory/getInventoryMasterData');
  }
}
