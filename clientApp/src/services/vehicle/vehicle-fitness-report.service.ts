import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { VehicleFitnessReportModel } from 'src/app/models/vehicle/vehicle-fitness-report.model';
import { VehicleResponseModel } from 'src/app/models/vehicle/vehicle-response.model';
import { ResponseModel } from 'src/app/models/common/response.model';
import { VehicleFRPaginatedItemsModel } from 'src/app/models/vehicle/vehicle-f-r-paginated.items.model';

@Injectable({
  providedIn: 'root'
})
export class VehicleFitnessReportService {

  constructor(private httpClient: HttpClient) { }

  // get
  getAllVehicleFR(vehicleId: number, pageSize: number, currentPage: number): Observable<VehicleFRPaginatedItemsModel> {
    return this.httpClient.
      get<VehicleFRPaginatedItemsModel>
      (environment.apiUrl + 'VehicleFitnessReport/' + vehicleId + '/' + pageSize + '/' + currentPage);
  }

  // add new
  addNewVehicleFR(model: VehicleFitnessReportModel): Observable<VehicleResponseModel> {
    return this.httpClient.
      post<VehicleResponseModel>
      (environment.apiUrl + 'VehicleFitnessReport', model);
  }

  // get by id
  getVehicleFRById(id: number): Observable<VehicleFitnessReportModel> {
    return this.httpClient.
      get<VehicleFitnessReportModel>
      (environment.apiUrl + 'VehicleFitnessReport/' + id);
  }


  // delete existing record
  deleteVehicleFR(id: number): Observable<ResponseModel> {
    return this.httpClient.
      delete<ResponseModel>(environment.apiUrl + 'VehicleFitnessReport/' + id);
  }


  // get
  getLatestRecordForVehicle(vehicleId: number): Observable<VehicleFitnessReportModel> {
    return this.httpClient.
      get<VehicleFitnessReportModel>
      (environment.apiUrl + 'VehicleFitnessReport/getLatestRecordForVehicle/' + vehicleId);
  };
}
