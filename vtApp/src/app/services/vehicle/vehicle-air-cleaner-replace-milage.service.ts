import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'environments/environment';
import { VehicleAirCleanerModel } from 'app/models/vehicle/vehicle-air-cleaner.model';
import { VehicleResponseModel } from 'app/models/vehicle/vehicle-response.model';
import { ResponseModel } from 'app/models/common/response.model';

@Injectable({
  providedIn: 'root'
})
export class VehicleAirCleanerReplaceMilageService {

  constructor(private httpClient: HttpClient) { }

  // get
  getAllVehicleAirCleaner(vehicleId: number): Observable<VehicleAirCleanerModel[]> {
    return this.httpClient.
      get<VehicleAirCleanerModel[]>
      (environment.apiUrl + 'VehicleAirCleaner/getAllVehicleAirCleaner/' + vehicleId);
  }

  // add new
  saveVehicleAirCleaner(model: VehicleAirCleanerModel): Observable<VehicleResponseModel> {
    return this.httpClient.
      post<VehicleResponseModel>
      (environment.apiUrl + 'VehicleAirCleaner/saveVehicleAirCleaner', model);
  }

  // get by id
  getVehicleAirCleanerById(id: number): Observable<VehicleAirCleanerModel> {
    return this.httpClient.
      get<VehicleAirCleanerModel>
      (environment.apiUrl + 'VehicleAirCleaner/getVehicleAirCleanerById/' + id);
  }


  // delete existing record
  deleteVehicleAirCleaner(id: number): Observable<ResponseModel> {
    return this.httpClient.
      delete<ResponseModel>(environment.apiUrl + 'VehicleAirCleaner/deleteVehicleAirCleaner/' + id);
  }

  // get
  getLatestRecordForVehicle(vehicleId: number): Observable<VehicleAirCleanerModel> {
    return this.httpClient.
      get<VehicleAirCleanerModel>
      (environment.apiUrl + 'VehicleAirCleaner/getLatestRecordForVehicle/' + vehicleId);
  };
}
