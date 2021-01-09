import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { VehicleDifferentialOilChangeMilageModel } from 'src/app/models/vehicle/vehicle-differential-oil-change-milage.model';
import { ResponseModel } from 'src/app/models/common/response.model';
import { Observable } from 'rxjs';
import { VehicleResponseModel } from 'src/app/models/vehicle/vehicle-response.model';
import { VehicleDOCMPaginatedItemsModel } from 'src/app/models/vehicle/vehicle-d-o-c-m-paginated.items.model';

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
