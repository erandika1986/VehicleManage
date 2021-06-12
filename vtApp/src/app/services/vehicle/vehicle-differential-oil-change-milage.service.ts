import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'environments/environment';
import { VehicleDifferentialOilChangeMilageModel } from 'app/models/vehicle/vehicle-differential-oil-change-milage.model';
import { VehicleResponseModel } from 'app/models/vehicle/vehicle-response.model';
import { ResponseModel } from 'app/models/common/response.model';

@Injectable({
  providedIn: 'root'
})
export class VehicleDifferentialOilChangeMilageService {

  constructor(private httpClient: HttpClient) { }

  // get
  getAllVehicleDifferentialOilChangeMilage(vehicleId: number): Observable<VehicleDifferentialOilChangeMilageModel[]> {
    return this.httpClient.
      get<VehicleDifferentialOilChangeMilageModel[]>
      (environment.apiUrl + 'VehicleDifferentialOilChangeMilage/getAllVehicleDifferentialOilChangeMilage/' + vehicleId );
  };
  // get
  getLatestRecordForVehicle(vehicleId: number): Observable<VehicleDifferentialOilChangeMilageModel> {
    return this.httpClient.
      get<VehicleDifferentialOilChangeMilageModel>
      (environment.apiUrl + 'VehicleDifferentialOilChangeMilage/getLatestRecordForVehicle/' + vehicleId);
  };

  // add new
  saveVehicleDifferentialOilChangeMilage(model: VehicleDifferentialOilChangeMilageModel): Observable<VehicleResponseModel> {
    return this.httpClient.
      post<VehicleResponseModel>
      (environment.apiUrl + 'VehicleDifferentialOilChangeMilage/saveVehicleDifferentialOilChangeMilage', model);
  };

  // get by id
  getVehicleDifferentialOilChangeMilageById(id: number): Observable<VehicleDifferentialOilChangeMilageModel> {
    return this.httpClient.
      get<VehicleDifferentialOilChangeMilageModel>
      (environment.apiUrl + 'VehicleDifferentialOilChangeMilage/getVehicleDifferentialOilChangeMilageById/' + id);
  };



  // delete existing record
  deleteVehicleDifferentialOilChangeMilage(id: number): Observable<ResponseModel> {
    return this.httpClient.
      delete<ResponseModel>(environment.apiUrl + 'VehicleDifferentialOilChangeMilage/deleteVehicleDifferentialOilChangeMilage/' + id);
  };
}
