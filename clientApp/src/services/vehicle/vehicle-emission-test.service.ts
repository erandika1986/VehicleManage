import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { VehicleResponseModel } from 'src/app/models/vehicle/vehicle-response.model';
import { ResponseModel } from 'src/app/models/common/response.model';
import { VehicleEmissionTestModel } from 'src/app/models/vehicle/vehicle-emission-test.model';
import { VehicleETPaginatedItemsModel } from 'src/app/models/vehicle/vehicle-e-t-paginated.items.model';

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
