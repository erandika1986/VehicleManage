import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { VehicleResponseModel } from 'src/app/models/vehicle/vehicle-response.model';
import { ResponseModel } from 'src/app/models/common/response.model';
import { VehicleFFMPaginatedItemsModel } from 'src/app/models/vehicle/vehicle-f-f-m-paginated.items.model';
import { VehicleFuelFilterMilageModel } from 'src/app/models/vehicle/vehicle-fuel-filter-milage.model';

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
