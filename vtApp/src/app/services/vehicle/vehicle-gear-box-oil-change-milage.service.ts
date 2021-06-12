import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'environments/environment';
import { VehicleGearBoxOilMilageModel } from 'app/models/vehicle/vehicle-gear-box-oil-milage.model';
import { VehicleResponseModel } from 'app/models/vehicle/vehicle-response.model';
import { ResponseModel } from 'app/models/common/response.model';

@Injectable({
  providedIn: 'root'
})
export class VehicleGearBoxOilChangeMilageService {

  constructor(private httpClient: HttpClient) { }

  // get
  getAllVehicleGearBoxOilMilage(vehicleId: number): Observable<VehicleGearBoxOilMilageModel[]> {
    return this.httpClient.
      get<VehicleGearBoxOilMilageModel[]>
      (environment.apiUrl + 'VehicleGearBoxOilMilage/getAllVehicleGearBoxOilMilage/' + vehicleId );
  }

  // add new
  saveVehicleGearBoxOilMilage(model: VehicleGearBoxOilMilageModel): Observable<VehicleResponseModel> {
    return this.httpClient.
      post<VehicleResponseModel>
      (environment.apiUrl + 'VehicleGearBoxOilMilage/saveVehicleGearBoxOilMilage', model);
  }

  // get by id
  getVehicleGearBoxOilMilageById(id: number): Observable<VehicleGearBoxOilMilageModel> {
    return this.httpClient.
      get<VehicleGearBoxOilMilageModel>
      (environment.apiUrl + 'VehicleGearBoxOilMilage/getVehicleGearBoxOilMilageById/' + id);
  }


  // delete existing record
  deleteVehicleGearBoxOilMilage(id: number): Observable<ResponseModel> {
    return this.httpClient.
      delete<ResponseModel>(environment.apiUrl + 'VehicleGearBoxOilMilage/deleteVehicleGearBoxOilMilage/' + id);
  }

  // get
  getLatestRecordForVehicle(vehicleId: number): Observable<VehicleGearBoxOilMilageModel> {
    return this.httpClient.
      get<VehicleGearBoxOilMilageModel>
      (environment.apiUrl + 'VehicleGearBoxOilMilage/getLatestRecordForVehicle/' + vehicleId);
  };
}
