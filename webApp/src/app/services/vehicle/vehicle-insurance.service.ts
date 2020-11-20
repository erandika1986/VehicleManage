import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { VehicleIPaginatedItemsModel } from 'src/app/models/vehicle/vehicle-i-paginated.items.model';
import { VehicleInsuranceModel } from 'src/app/models/vehicle/vehicle-insurance.model';
import { Observable } from 'rxjs';
import { VehicleResponseModel } from 'src/app/models/vehicle/vehicle-response.model';
import { ResponseModel } from 'src/app/models/common/response.model';

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
