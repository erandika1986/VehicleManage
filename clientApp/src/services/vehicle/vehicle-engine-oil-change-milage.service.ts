import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { VehicleEOCMPaginatedItemsModel } from 'models/vehicle/vehicle-e-o-c-paginated.items.model';
import { environment } from 'environments/environment';
import { VehicleResponseModel } from 'models/vehicle/vehicle-response.model';
import { VehicleEngineOilMilageModel } from 'models/vehicle/vehicle-engine-oil-milage.model';
import { ResponseModel } from 'models/common/response.model';

@Injectable({
  providedIn: 'root'
})
export class VehicleEngineOilChangeMilageService {

  constructor(private httpClient: HttpClient) { }

  // get
  getAllVehicleEOCM(vehicleId: number, pageSize: number, currentPage: number): Observable<VehicleEOCMPaginatedItemsModel> {
    return this.httpClient.
      get<VehicleEOCMPaginatedItemsModel>
      (environment.apiUrl + 'VehicleEngineOilMilage/' + vehicleId + '/' + pageSize + '/' + currentPage);
  }

  // add new
  addNewVehicleEOCM(model: VehicleEngineOilMilageModel): Observable<VehicleResponseModel> {
    return this.httpClient.
      post<VehicleResponseModel>
      (environment.apiUrl + 'VehicleEngineOilMilage', model);
  }

  // get by id
  getVehicleEOCMById(id: number): Observable<VehicleEngineOilMilageModel> {
    return this.httpClient.
      get<VehicleEngineOilMilageModel>
      (environment.apiUrl + 'VehicleEngineOilMilage/' + id);
  }


  // delete existing record
  deleteVehicleEOCM(id: number): Observable<ResponseModel> {
    return this.httpClient.
      delete<ResponseModel>(environment.apiUrl + 'VehicleEngineOilMilage/' + id);
  }

  // get
  getLatestRecordForVehicle(vehicleId: number): Observable<VehicleEngineOilMilageModel> {
    return this.httpClient.
      get<VehicleEngineOilMilageModel>
      (environment.apiUrl + 'VehicleEngineOilMilage/getLatestRecordForVehicle/' + vehicleId);
  };
}
