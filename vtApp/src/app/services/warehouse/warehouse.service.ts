import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ResponseModel } from 'app/models/common/response.model';
import { WarehouseModel } from 'app/models/warehouse/warehouse.model';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class WarehouseService {

  constructor(private httpClient: HttpClient) { }

  getWarehouseById(id: number): Observable<WarehouseModel> {
    return this.httpClient.
      get<WarehouseModel>(environment.apiUrl + 'Warehouse' + "/" + id);
  }

  GetAllWarehouses(): Observable<WarehouseModel[]> {
    return this.httpClient.
      get<WarehouseModel[]>(environment.apiUrl + 'Warehouse');
  }

  SaveWarehouse(vm: WarehouseModel): Observable<ResponseModel> {
    return this.httpClient.
      post<ResponseModel>(environment.apiUrl + 'Warehouse', vm);
  }



  delete(id: number): Observable<ResponseModel> {
    return this.httpClient.
      delete<ResponseModel>(environment.apiUrl + 'Warehouse' + "/" + id);
  }
}
