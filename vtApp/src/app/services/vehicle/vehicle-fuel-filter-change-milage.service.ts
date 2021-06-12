import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'environments/environment';
import { VehicleFuelFilterMilageModel } from 'app/models/vehicle/vehicle-fuel-filter-milage.model';
import { VehicleResponseModel } from 'app/models/vehicle/vehicle-response.model';
import { ResponseModel } from 'app/models/common/response.model';

@Injectable({
  providedIn: 'root'
})
export class VehicleFuelFilterChangeMilageService {

  constructor(private httpClient: HttpClient) { }

  // get
  getAllVehicleFuelFilterMilage(vehicleId: number): Observable<VehicleFuelFilterMilageModel[]> {
    return this.httpClient.
      get<VehicleFuelFilterMilageModel[]>
      (environment.apiUrl + 'VehicleFuelFilterMilage/getAllVehicleFuelFilterMilage/' + vehicleId);
  }

  // add new
  saveVehicleFuelFilterMilage(model: VehicleFuelFilterMilageModel): Observable<VehicleResponseModel> {
    return this.httpClient.
      post<VehicleResponseModel>
      (environment.apiUrl + 'VehicleFuelFilterMilage/saveVehicleFuelFilterMilage', model);
  }

  // get by id
  getVehicleFuelFilterMilageById(id: number): Observable<VehicleFuelFilterMilageModel> {
    return this.httpClient.
      get<VehicleFuelFilterMilageModel>
      (environment.apiUrl + 'VehicleFuelFilterMilage/getVehicleFuelFilterMilageById/' + id);
  }

  // delete existing record
  deleteVehicleFuelFilterMilage(id: number): Observable<ResponseModel> {
    return this.httpClient.
      delete<ResponseModel>(environment.apiUrl + 'VehicleFuelFilterMilage/deleteVehicleFuelFilterMilage/' + id);
  }

  // get
  getLatestRecordForVehicle(vehicleId: number): Observable<VehicleFuelFilterMilageModel> {
    return this.httpClient.
      get<VehicleFuelFilterMilageModel>
      (environment.apiUrl + 'VehicleFuelFilterMilage/getLatestRecordForVehicle/' + vehicleId);
  };
}
