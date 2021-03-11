import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { VehicleGNPaginatedItemsModel } from 'app/models/vehicle/vehicle-g-n-paginated.items.model';
import { environment } from 'environments/environment';
import { VehicleGreeceNipleModel } from 'app/models/vehicle/vehicle-greece-niple';
import { VehicleResponseModel } from 'app/models/vehicle/vehicle-response.model';
import { ResponseModel } from 'app/models/common/response.model';

@Injectable({
  providedIn: 'root'
})
export class VehicleGreeceNipleService {

  constructor(private httpClient: HttpClient) { }

  // get
  getAllVehicleGN(vehicleId: number, pageSize: number, currentPage: number): Observable<VehicleGNPaginatedItemsModel> {
    return this.httpClient.
      get<VehicleGNPaginatedItemsModel>
      (environment.apiUrl + 'VehicleGreeceNiple/' + vehicleId + '/' + pageSize + '/' + currentPage);
  }

  // add new
  addNewVehicleGN(model: VehicleGreeceNipleModel): Observable<VehicleResponseModel> {
    return this.httpClient.
      post<VehicleResponseModel>
      (environment.apiUrl + 'VehicleGreeceNiple', model);
  }

  // get by id
  getVehicleGNById(id: number): Observable<VehicleGreeceNipleModel> {
    return this.httpClient.
      get<VehicleGreeceNipleModel>
      (environment.apiUrl + 'VehicleGreeceNiple/' + id);
  }


  // delete existing record
  deleteVehicleGN(id: number): Observable<ResponseModel> {
    return this.httpClient.
      delete<ResponseModel>(environment.apiUrl + 'VehicleGreeceNiple/' + id);
  }

  // get
  getLatestRecordForVehicle(vehicleId: number): Observable<VehicleGreeceNipleModel> {
    return this.httpClient.
      get<VehicleGreeceNipleModel>
      (environment.apiUrl + 'VehicleGreeceNiple/getLatestRecordForVehicle/' + vehicleId);
  };
}
