import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { VehicleGBOCMPaginatedItemsModel } from 'app/models/vehicle/vehicle-g-b-o-c-m-paginated.items.model';
import { environment } from 'environments/environment';
import { VehicleGearBoxOilMilageModel } from 'app/models/vehicle/vehicle-gear-box-oil-milage.model';
import { VehicleResponseModel } from 'app/models/vehicle/vehicle-response.model';
import { ResponseModel } from 'app/models/common/response.model';

@Injectable({
  providedIn: 'root'
})
export class VehicleGearBoxOilChangeMilageService {

  constructor(private httpClient: HttpClient) { }

  // get
  getAllVehicleGBOCM(vehicleId: number, pageSize: number, currentPage: number): Observable<VehicleGBOCMPaginatedItemsModel> {
    return this.httpClient.
      get<VehicleGBOCMPaginatedItemsModel>
      (environment.apiUrl + 'VehicleGearBoxOilMilage/' + vehicleId + '/' + pageSize + '/' + currentPage);
  }

  // add new
  addNewVehicleGBOCM(model: VehicleGearBoxOilMilageModel): Observable<VehicleResponseModel> {
    return this.httpClient.
      post<VehicleResponseModel>
      (environment.apiUrl + 'VehicleGearBoxOilMilage', model);
  }

  // get by id
  getVehicleRLGBOCMId(id: number): Observable<VehicleGearBoxOilMilageModel> {
    return this.httpClient.
      get<VehicleGearBoxOilMilageModel>
      (environment.apiUrl + 'VehicleGearBoxOilMilage/' + id);
  }


  // delete existing record
  deleteVehicleGBOCM(id: number): Observable<ResponseModel> {
    return this.httpClient.
      delete<ResponseModel>(environment.apiUrl + 'VehicleGearBoxOilMilage/' + id);
  }

  // get
  getLatestRecordForVehicle(vehicleId: number): Observable<VehicleGearBoxOilMilageModel> {
    return this.httpClient.
      get<VehicleGearBoxOilMilageModel>
      (environment.apiUrl + 'VehicleGearBoxOilMilage/getLatestRecordForVehicle/' + vehicleId);
  };
}
