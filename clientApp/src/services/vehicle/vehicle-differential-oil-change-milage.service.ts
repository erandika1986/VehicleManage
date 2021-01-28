import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { VehicleDOCMPaginatedItemsModel } from 'models/vehicle/vehicle-d-o-c-m-paginated.items.model';
import { environment } from 'environments/environment';
import { VehicleDifferentialOilChangeMilageModel } from 'models/vehicle/vehicle-differential-oil-change-milage.model';
import { VehicleResponseModel } from 'models/vehicle/vehicle-response.model';
import { ResponseModel } from 'models/common/response.model';

@Injectable({
  providedIn: 'root'
})
export class VehicleDifferentialOilChangeMilageService {

  constructor(private httpClient: HttpClient) { }

  // get
  getAllVehicleDOCM(vehicleId: number, pageSize: number, currentPage: number): Observable<VehicleDOCMPaginatedItemsModel> {
    return this.httpClient.
      get<VehicleDOCMPaginatedItemsModel>
      (environment.apiUrl + 'VehicleDifferentialOilChangeMilage/' + vehicleId + '/' + pageSize + '/' + currentPage);
  };
  // get
  getLatestRecordForVehicle(vehicleId: number): Observable<VehicleDifferentialOilChangeMilageModel> {
    return this.httpClient.
      get<VehicleDifferentialOilChangeMilageModel>
      (environment.apiUrl + 'VehicleDifferentialOilChangeMilage/getLatestRecordForVehicle/' + vehicleId);
  };

  // add new
  addNewVehicleDOCM(model: VehicleDifferentialOilChangeMilageModel): Observable<VehicleResponseModel> {
    return this.httpClient.
      post<VehicleResponseModel>
      (environment.apiUrl + 'VehicleDifferentialOilChangeMilage', model);
  };

  // get by id
  getVehicleDOCMById(id: number): Observable<VehicleDifferentialOilChangeMilageModel> {
    return this.httpClient.
      get<VehicleDifferentialOilChangeMilageModel>
      (environment.apiUrl + 'VehicleDifferentialOilChangeMilage/' + id);
  };



  // delete existing record
  deleteVehicleDOCM(id: number): Observable<ResponseModel> {
    return this.httpClient.
      delete<ResponseModel>(environment.apiUrl + 'VehicleDifferentialOilChangeMilage/' + id);
  };
}
