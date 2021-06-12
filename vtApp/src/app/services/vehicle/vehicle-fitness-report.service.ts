import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'environments/environment';
import { VehicleFitnessReportModel } from 'app/models/vehicle/vehicle-fitness-report.model';
import { VehicleResponseModel } from 'app/models/vehicle/vehicle-response.model';
import { ResponseModel } from 'app/models/common/response.model';
import { upload, Upload } from 'app/models/common/upload';

@Injectable({
  providedIn: 'root'
})
export class VehicleFitnessReportService {

  constructor(private httpClient: HttpClient) { }

  // get
  getAllVehicleFitnessReport(vehicleId: number, pageSize: number, currentPage: number): Observable<VehicleResponseModel[]> {
    return this.httpClient.
      get<VehicleResponseModel[]>
      (environment.apiUrl + 'VehicleFitnessReport/getAllVehicleFitnessReport/' + vehicleId);
  }

  // add new
  saveVehicleFitnessReport(model: VehicleFitnessReportModel): Observable<VehicleResponseModel> {
    return this.httpClient.
      post<VehicleResponseModel>
      (environment.apiUrl + 'VehicleFitnessReport/saveVehicleFitnessReport', model);
  }

  // get by id
  getVehicleFitnessReportById(id: number): Observable<VehicleFitnessReportModel> {
    return this.httpClient.
      get<VehicleFitnessReportModel>
      (environment.apiUrl + 'VehicleFitnessReport/getVehicleFitnessReportById/' + id);
  }


  // delete existing record
  deleteVehicleFitnessReport(id: number): Observable<ResponseModel> {
    return this.httpClient.
      delete<ResponseModel>(environment.apiUrl + 'VehicleFitnessReport/deleteVehicleFitnessReport/' + id);
  }


  // get
  getLatestRecordForVehicle(vehicleId: number): Observable<VehicleFitnessReportModel> {
    return this.httpClient.
      get<VehicleFitnessReportModel>
      (environment.apiUrl + 'VehicleFitnessReport/getLatestRecordForVehicle/' + vehicleId);
  };

  uploadFitnessReportImage(data: FormData): Observable<Upload> {
    return this.httpClient.post(environment.apiUrl + 'VehicleFitnessReport/uploadFitnessReportImage', data,{reportProgress: true,observe: 'events'}).pipe(upload());;
  }

  downloadFitnessReportImage(id: number): Observable<any> {
    return this.httpClient.get<any>(environment.apiUrl +'VehicleFitnessReport/downloadFitnessReportImage/'+id,{headers:{'filedownload':''}, observe: 'events',reportProgress:true });
  }
}
