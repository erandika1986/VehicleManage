import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { VehicleETPaginatedItemsModel } from 'models/vehicle/vehicle-e-t-paginated.items.model';
import { environment } from 'environments/environment';
import { VehicleResponseModel } from 'models/vehicle/vehicle-response.model';
import { VehicleEmissionTestModel } from 'models/vehicle/vehicle-emission-test.model';
import { ResponseModel } from 'models/common/response.model';

@Injectable({
  providedIn: 'root'
})
export class VehicleEmissionTestService {

  constructor(private httpClient: HttpClient) { }

  // get
  getAllVehicleET(vehicleId: number, pageSize: number, currentPage: number): Observable<VehicleETPaginatedItemsModel> {
    return this.httpClient.
      get<VehicleETPaginatedItemsModel>
      (environment.apiUrl + 'VehicleEmissionTest/' + vehicleId + '/' + pageSize + '/' + currentPage);
  }

  // add new
  addNewVehicleET(model: VehicleEmissionTestModel): Observable<VehicleResponseModel> {
    return this.httpClient.
      post<VehicleResponseModel>
      (environment.apiUrl + 'VehicleEmissionTest', model);
  }

  // get by id
  getVehicleETById(id: number): Observable<VehicleEmissionTestModel> {
    return this.httpClient.
      get<VehicleEmissionTestModel>
      (environment.apiUrl + 'VehicleEmissionTest/' + id);
  }



  // delete existing record
  deleteVehicleET(id: number): Observable<ResponseModel> {
    return this.httpClient.
      delete<ResponseModel>(environment.apiUrl + 'VehicleEmissionTest/' + id);
  }

  // get
  getLatestRecordForVehicle(vehicleId: number): Observable<VehicleEmissionTestModel> {
    return this.httpClient.
      get<VehicleEmissionTestModel>
      (environment.apiUrl + 'VehicleEmissionTest/getLatestRecordForVehicle/' + vehicleId);
  };
}
