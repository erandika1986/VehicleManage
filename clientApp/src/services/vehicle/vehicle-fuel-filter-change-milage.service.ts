import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { VehicleFFMPaginatedItemsModel } from 'models/vehicle/vehicle-f-f-m-paginated.items.model';
import { environment } from 'environments/environment';
import { VehicleResponseModel } from 'models/vehicle/vehicle-response.model';
import { VehicleFuelFilterMilageModel } from 'models/vehicle/vehicle-fuel-filter-milage.model';
import { ResponseModel } from 'models/common/response.model';

@Injectable({
  providedIn: 'root'
})
export class VehicleFuelFilterChangeMilageService {

  constructor(private httpClient: HttpClient) { }

  // get
  getAllVehicleFFM(vehicleId: number, pageSize: number, currentPage: number): Observable<VehicleFFMPaginatedItemsModel> {
    return this.httpClient.
      get<VehicleFFMPaginatedItemsModel>
      (environment.apiUrl + 'VehicleFuelFilterMilage/' + vehicleId + '/' + pageSize + '/' + currentPage);
  }

  // add new
  addNewVehicleFFM(model: VehicleFuelFilterMilageModel): Observable<VehicleResponseModel> {
    return this.httpClient.
      post<VehicleResponseModel>
      (environment.apiUrl + 'VehicleFuelFilterMilage', model);
  }

  // get by id
  getVehicleFFMById(id: number): Observable<VehicleFuelFilterMilageModel> {
    return this.httpClient.
      get<VehicleFuelFilterMilageModel>
      (environment.apiUrl + 'VehicleFuelFilterMilage/' + id);
  }

  // delete existing record
  deleteVehicleFFM(id: number): Observable<ResponseModel> {
    return this.httpClient.
      delete<ResponseModel>(environment.apiUrl + 'VehicleFuelFilterMilage/' + id);
  }

  // get
  getLatestRecordForVehicle(vehicleId: number): Observable<VehicleFuelFilterMilageModel> {
    return this.httpClient.
      get<VehicleFuelFilterMilageModel>
      (environment.apiUrl + 'VehicleFuelFilterMilage/getLatestRecordForVehicle/' + vehicleId);
  };
}
