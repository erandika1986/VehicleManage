import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
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
}
