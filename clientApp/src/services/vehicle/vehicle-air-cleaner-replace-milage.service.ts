import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { VehicleResponseModel } from 'src/app/models/vehicle/vehicle-response.model';
import { ResponseModel } from 'src/app/models/common/response.model';
import { VehicleAirCleanerModel } from 'src/app/models/vehicle/vehicle-air-cleaner.model';
import { VehicleACRMPaginatedItemsModel } from 'src/app/models/vehicle/vehicle-a-c-paginated.items.model';

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
