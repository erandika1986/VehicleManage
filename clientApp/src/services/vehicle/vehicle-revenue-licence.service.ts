import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { VehicleRLPaginatedItemsModel } from 'models/vehicle/vehicle-r-l-paginated.items.model';
import { environment } from 'environments/environment';
import { VehicleResponseModel } from 'models/vehicle/vehicle-response.model';
import { VehicleRevenueLicenceModel } from 'models/vehicle/vehicle-revenue-licence.model';
import { ResponseModel } from 'models/common/response.model';

@Injectable({
  providedIn: 'root'
})
export class VehicleRevenueLicenceService {
  constructor(private httpClient: HttpClient) { }

  // get
  getAllVehicleRL(vehicleId: number, pageSize: number, currentPage: number): Observable<VehicleRLPaginatedItemsModel> {
    return this.httpClient.
      get<VehicleRLPaginatedItemsModel>
      (environment.apiUrl + 'VehicleRevenueLicence/' + vehicleId + '/' + pageSize + '/' + currentPage);
  }

  // add new
  addNewVehicleRL(model: VehicleRevenueLicenceModel): Observable<VehicleResponseModel> {
    return this.httpClient.
      post<VehicleResponseModel>
      (environment.apiUrl + 'VehicleRevenueLicence', model);
  }

  // get by id
  getVehicleRLById(id: number): Observable<VehicleRevenueLicenceModel> {
    return this.httpClient.
      get<VehicleRevenueLicenceModel>
      (environment.apiUrl + 'VehicleRevenueLicence/' + id);
  }


  // delete existing record
  deleteVehicleRL(id: number): Observable<ResponseModel> {
    return this.httpClient.
      delete<ResponseModel>(environment.apiUrl + 'VehicleRevenueLicence/' + id);
  }

  // get
  getLatestRecordForVehicle(vehicleId: number): Observable<VehicleRevenueLicenceModel> {
    return this.httpClient.
      get<VehicleRevenueLicenceModel>
      (environment.apiUrl + 'VehicleRevenueLicence/getLatestRecordForVehicle/' + vehicleId);
  };
}
