import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'environments/environment';
import { VehicleEmissionTestModel } from 'app/models/vehicle/vehicle-emission-test.model';
import { VehicleResponseModel } from 'app/models/vehicle/vehicle-response.model';
import { ResponseModel } from 'app/models/common/response.model';
import { upload, Upload } from 'app/models/common/upload';

@Injectable({
  providedIn: 'root'
})
export class VehicleEmissionTestService {

  constructor(private httpClient: HttpClient) { }

  // get
  getAllVehicleEmissionTest(vehicleId: number): Observable<VehicleResponseModel[]> {
    return this.httpClient.
      get<VehicleResponseModel[]>
      (environment.apiUrl + 'VehicleEmissionTest/getAllVehicleEmissionTest/' + vehicleId);
  }

  // add new
  saveVehicleEmissionTest(model: VehicleEmissionTestModel): Observable<VehicleResponseModel> {
    return this.httpClient.
      post<VehicleResponseModel>
      (environment.apiUrl + 'VehicleEmissionTest/saveVehicleEmissionTest', model);
  }

  // get by id
  getVehicleEmissionTestById(id: number): Observable<VehicleEmissionTestModel> {
    return this.httpClient.
      get<VehicleEmissionTestModel>
      (environment.apiUrl + 'VehicleEmissionTest/getVehicleEmissionTestById/' + id);
  }



  // delete existing record
  deleteVehicleEmissionTest(id: number): Observable<ResponseModel> {
    return this.httpClient.
      delete<ResponseModel>(environment.apiUrl + 'VehicleEmissionTest/deleteVehicleEmissionTest/' + id);
  }

  // get
  GetLatestRecordForVehicle(vehicleId: number): Observable<VehicleEmissionTestModel> {
    return this.httpClient.
      get<VehicleEmissionTestModel>
      (environment.apiUrl + 'VehicleEmissionTest/GetLatestRecordForVehicle/' + vehicleId);
  };

  uploadEmissionTestImage(data: FormData): Observable<Upload> {
    return this.httpClient.post(environment.apiUrl + 'VehicleEmissionTest/uploadEmissionTestImage', data,{reportProgress: true,observe: 'events'}).pipe(upload());;
  }

  downloadEmissionTestImage(id: number): Observable<any> {
    return this.httpClient.get<any>(environment.apiUrl +'VehicleEmissionTest/downloadEmissionTestImage/'+id,{headers:{'filedownload':''}, observe: 'events',reportProgress:true });
  }
}
