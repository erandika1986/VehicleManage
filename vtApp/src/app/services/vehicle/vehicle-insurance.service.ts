import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { VehicleIPaginatedItemsModel } from 'app/models/vehicle/vehicle-i-paginated.items.model';
import { environment } from 'environments/environment';
import { VehicleInsuranceModel } from 'app/models/vehicle/vehicle-insurance.model';
import { VehicleResponseModel } from 'app/models/vehicle/vehicle-response.model';
import { ResponseModel } from 'app/models/common/response.model';

@Injectable({
  providedIn: 'root'
})
export class VehicleInsuranceService {

  constructor(private httpClient: HttpClient) { }

  // get
  getAllVehicleI(vehicleId: number, pageSize: number, currentPage: number): Observable<VehicleIPaginatedItemsModel> {
    return this.httpClient.
      get<VehicleIPaginatedItemsModel>
      (environment.apiUrl + 'VehicleInsurance/' + vehicleId + '/' + pageSize + '/' + currentPage);
  }

  // add new
  addNewVehicleI(model: VehicleInsuranceModel): Observable<VehicleResponseModel> {
    return this.httpClient.
      post<VehicleResponseModel>
      (environment.apiUrl + 'VehicleInsurance', model);
  }

  // get by id
  getVehicleIById(id: number): Observable<VehicleInsuranceModel> {
    return this.httpClient.
      get<VehicleInsuranceModel>
      (environment.apiUrl + 'VehicleInsurance/' + id);
  }


  // delete existing record
  deleteVehicleI(id: number): Observable<ResponseModel> {
    return this.httpClient.
      delete<ResponseModel>(environment.apiUrl + 'VehicleInsurance/' + id);
  }

  // get
  getLatestRecordForVehicle(vehicleId: number): Observable<VehicleInsuranceModel> {
    return this.httpClient.
      get<VehicleInsuranceModel>
      (environment.apiUrl + 'VehicleInsurance/getLatestRecordForVehicle/' + vehicleId);
  };
}
