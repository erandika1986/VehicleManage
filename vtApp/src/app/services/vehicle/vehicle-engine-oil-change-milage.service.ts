import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'environments/environment';
import { VehicleEngineOilMilageModel } from 'app/models/vehicle/vehicle-engine-oil-milage.model';
import { VehicleResponseModel } from 'app/models/vehicle/vehicle-response.model';
import { ResponseModel } from 'app/models/common/response.model';

@Injectable({
  providedIn: 'root'
})
export class VehicleEngineOilChangeMilageService {

  constructor(private httpClient: HttpClient) { }

  // get
  getAllVehicleEngineOilMilage(vehicleId: number): Observable<VehicleEngineOilMilageModel[]> {
    return this.httpClient.
      get<VehicleEngineOilMilageModel[]>
      (environment.apiUrl + 'VehicleEngineOilMilage/getAllVehicleEngineOilMilage/' + vehicleId);
  }

  // add new
  saveVehicleEngineOilMilage(model: VehicleEngineOilMilageModel): Observable<VehicleResponseModel> {
    return this.httpClient.
      post<VehicleResponseModel>
      (environment.apiUrl + 'VehicleEngineOilMilage/saveVehicleEngineOilMilage', model);
  }

  // get by id
  getVehicleEngineOilMilageById(id: number): Observable<VehicleEngineOilMilageModel> {
    return this.httpClient.
      get<VehicleEngineOilMilageModel>
      (environment.apiUrl + 'VehicleEngineOilMilage/getVehicleEngineOilMilageById/' + id);
  }


  // delete existing record
  deleteVehicleEngineOilMilage(id: number): Observable<ResponseModel> {
    return this.httpClient.
      delete<ResponseModel>(environment.apiUrl + 'VehicleEngineOilMilage/deleteVehicleEngineOilMilage/' + id);
  }

  // get
  getLatestRecordForVehicle(vehicleId: number): Observable<VehicleEngineOilMilageModel> {
    return this.httpClient.
      get<VehicleEngineOilMilageModel>
      (environment.apiUrl + 'VehicleEngineOilMilage/getLatestRecordForVehicle/' + vehicleId);
  };
}
