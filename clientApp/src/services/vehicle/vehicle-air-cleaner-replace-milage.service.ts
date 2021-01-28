import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { VehicleACRMPaginatedItemsModel } from 'models/vehicle/vehicle-a-c-paginated.items.model';
import { environment } from 'environments/environment';
import { VehicleAirCleanerModel } from 'models/vehicle/vehicle-air-cleaner.model';
import { VehicleResponseModel } from 'models/vehicle/vehicle-response.model';
import { ResponseModel } from 'models/common/response.model';

@Injectable({
  providedIn: 'root'
})
export class VehicleAirCleanerReplaceMilageService {

  constructor(private httpClient: HttpClient) { }

  // get
  getAllVehicleACRM(vehicleId: number, pageSize: number, currentPage: number): Observable<VehicleACRMPaginatedItemsModel> {
    return this.httpClient.
      get<VehicleACRMPaginatedItemsModel>
      (environment.apiUrl + 'VehicleAirCleaner/' + vehicleId + '/' + pageSize + '/' + currentPage);
  }

  // add new
  addNewVehicleACRM(model: VehicleAirCleanerModel): Observable<VehicleResponseModel> {
    return this.httpClient.
      post<VehicleResponseModel>
      (environment.apiUrl + 'VehicleAirCleaner', model);
  }

  // get by id
  getVehicleACRMById(id: number): Observable<VehicleAirCleanerModel> {
    return this.httpClient.
      get<VehicleAirCleanerModel>
      (environment.apiUrl + 'VehicleAirCleaner/' + id);
  }


  // delete existing record
  deleteVehicleACRM(id: number): Observable<ResponseModel> {
    return this.httpClient.
      delete<ResponseModel>(environment.apiUrl + 'VehicleAirCleaner/' + id);
  }

  // get
  getLatestRecordForVehicle(vehicleId: number): Observable<VehicleAirCleanerModel> {
    return this.httpClient.
      get<VehicleAirCleanerModel>
      (environment.apiUrl + 'VehicleAirCleaner/getLatestRecordForVehicle/' + vehicleId);
  };
}
