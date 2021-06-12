import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'environments/environment';
import { VehicleResponseModel } from 'app/models/vehicle/vehicle-response.model';
import { VehicleRevenueLicenceModel } from 'app/models/vehicle/vehicle-revenue-licence.model';
import { ResponseModel } from 'app/models/common/response.model';
import { upload, Upload } from 'app/models/common/upload';

@Injectable({
  providedIn: 'root'
})
export class VehicleRevenueLicenceService {
  constructor(private httpClient: HttpClient) { }

  // get
  getAllVehicleRevenueLicence(vehicleId: number): Observable<VehicleRevenueLicenceModel[]> {
    return this.httpClient.
      get<VehicleRevenueLicenceModel[]>
      (environment.apiUrl + 'VehicleRevenueLicence/getAllVehicleRevenueLicence/' + vehicleId);
  }

  // add new
  saveVehicleRevenueLicence(model: VehicleRevenueLicenceModel): Observable<VehicleResponseModel> {
    return this.httpClient.
      post<VehicleResponseModel>
      (environment.apiUrl + 'VehicleRevenueLicence/saveVehicleRevenueLicence', model);
  }

  // get by id
  getVehicleRevenueLicenceById(id: number): Observable<VehicleRevenueLicenceModel> {
    return this.httpClient.
      get<VehicleRevenueLicenceModel>
      (environment.apiUrl + 'VehicleRevenueLicence/getVehicleRevenueLicenceById/' + id);
  }


  // delete existing record
  deleteVehicleRevenueLicence(id: number): Observable<ResponseModel> {
    return this.httpClient.
      delete<ResponseModel>(environment.apiUrl + 'VehicleRevenueLicence/deleteVehicleRevenueLicence/' + id);
  }

  // get
  getLatestRecordForVehicle(vehicleId: number): Observable<VehicleRevenueLicenceModel> {
    return this.httpClient.
      get<VehicleRevenueLicenceModel>
      (environment.apiUrl + 'VehicleRevenueLicence/getLatestRecordForVehicle/' + vehicleId);
  };

  
  uploadRevenueLicenceImage(data: FormData): Observable<Upload> {
    return this.httpClient.post(environment.apiUrl + 'VehicleRevenueLicence/uploadRevenueLicenceImage', data,{reportProgress: true,observe: 'events'}).pipe(upload());;
  }

  downloadRevenueLicenceImage(id: number): Observable<any> {
    return this.httpClient.get<any>(environment.apiUrl +'VehicleRevenueLicence/downloadRevenueLicenceImage/'+id,{headers:{'filedownload':''}, observe: 'events',reportProgress:true });
  }
}
