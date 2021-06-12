import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from 'environments/environment';
import { VehicleInsuranceModel } from 'app/models/vehicle/vehicle-insurance.model';
import { VehicleResponseModel } from 'app/models/vehicle/vehicle-response.model';
import { ResponseModel } from 'app/models/common/response.model';
import { upload, Upload } from 'app/models/common/upload';

@Injectable({
  providedIn: 'root'
})
export class VehicleInsuranceService {

  constructor(private httpClient: HttpClient) { }

  // get
  getAllVehicleInsuranceDetails(vehicleId: number): Observable<VehicleInsuranceModel[]> {
    return this.httpClient.
      get<VehicleInsuranceModel[]>
      (environment.apiUrl + 'VehicleInsurance/getAllVehicleInsurance/' + vehicleId);
  }

  // add new
  saveVehicleInsurance(model: VehicleInsuranceModel): Observable<VehicleResponseModel> {
    return this.httpClient.
      post<VehicleResponseModel>
      (environment.apiUrl + 'VehicleInsurance/saveVehicleInsurance', model);
  }

  // get by id
  getVehicleInsuranceRecordById(id: number): Observable<VehicleInsuranceModel> {
    return this.httpClient.
      get<VehicleInsuranceModel>
      (environment.apiUrl + 'VehicleInsurance/getVehicleInsuranceById/' + id);
  }


  // delete existing record
  deleteVehicleInsuranceRecord(id: number): Observable<ResponseModel> {
    return this.httpClient.
      delete<ResponseModel>(environment.apiUrl + 'VehicleInsurance/deleteVehicleInsurance/' + id);
  }

  // get
  getLatestRecordForVehicleInsurance(vehicleId: number): Observable<VehicleInsuranceModel> {
    return this.httpClient.
      get<VehicleInsuranceModel>
      (environment.apiUrl + 'VehicleInsurance/getLatestRecordForVehicle/' + vehicleId);
  };

  uploadInsuranceImage(data: FormData): Observable<Upload> {
    return this.httpClient.post(environment.apiUrl + 'VehicleInsurance/uploadInsuranceImage', data,{reportProgress: true,observe: 'events'}).pipe(upload());;
  }

  downloadInsuranceImage(id: number): Observable<any> {
    return this.httpClient.get<any>(environment.apiUrl +'VehicleInsurance/downloadInsuranceImage/'+id,{headers:{'filedownload':''}, observe: 'events',reportProgress:true });
  }
}
