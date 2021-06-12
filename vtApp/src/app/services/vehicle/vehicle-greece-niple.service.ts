import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
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
  getAllVehicleGreeceNiple(vehicleId: number): Observable<VehicleGreeceNipleModel[]> {
    return this.httpClient.
      get<VehicleGreeceNipleModel[]>
      (environment.apiUrl + 'VehicleGreeceNiple/getAllVehicleGreeceNiple/' + vehicleId);
  }

  // add new
  saveVehicleGreeceNiple(model: VehicleGreeceNipleModel): Observable<VehicleResponseModel> {
    return this.httpClient.
      post<VehicleResponseModel>
      (environment.apiUrl + 'VehicleGreeceNiple/saveVehicleGreeceNiple', model);
  }

  // get by id
  getVehicleGreeceNipleById(id: number): Observable<VehicleGreeceNipleModel> {
    return this.httpClient.
      get<VehicleGreeceNipleModel>
      (environment.apiUrl + 'VehicleGreeceNiple/getVehicleGreeceNipleById/' + id);
  }


  // delete existing record
  deleteVehicleGreeceNiple(id: number): Observable<ResponseModel> {
    return this.httpClient.
      delete<ResponseModel>(environment.apiUrl + 'VehicleGreeceNiple/deleteVehicleGreeceNiple/' + id);
  }

  // get
  getLatestRecordForVehicle(vehicleId: number): Observable<VehicleGreeceNipleModel> {
    return this.httpClient.
      get<VehicleGreeceNipleModel>
      (environment.apiUrl + 'VehicleGreeceNiple/getLatestRecordForVehicle/' + vehicleId);
  };
}
