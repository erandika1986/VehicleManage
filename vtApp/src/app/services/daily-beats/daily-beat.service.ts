import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from 'environments/environment';
import { VehicleBeatMasterDataModel } from 'app/models/dialy-beat/vehicle-beat-master-data.model';
import { VehicleBeatFilterModel } from 'app/models/dialy-beat/vehicle-beat-filter.model';
import { DailyVehicleBeatModel } from 'app/models/dialy-beat/daily-vehicle-beat.model';
import { ResponseModel } from 'app/models/common/response.model';

@Injectable({
  providedIn: 'root'
})
export class DailyBeatService {

  constructor(private httpClient: HttpClient) { }

  getMasterData(): Observable<VehicleBeatMasterDataModel> {
    return this.httpClient.
      get<VehicleBeatMasterDataModel>(environment.apiUrl + 'VehicleDailyBeat' + "/getMasterData");
  }

  getAllVehicleBeatRecord(filter: VehicleBeatFilterModel): Observable<DailyVehicleBeatModel[]> {
    return this.httpClient.post<DailyVehicleBeatModel[]>(environment.apiUrl + 'VehicleDailyBeat/getAllVehicleBeatRecord', filter);
  }

  getVehicleBeatRecordById(id: number): Observable<DailyVehicleBeatModel> {
    return this.httpClient.
      get<DailyVehicleBeatModel>(environment.apiUrl + 'VehicleDailyBeat' + "/" + id);
  }

  saveDailyVehicleBeatRecord(vm: DailyVehicleBeatModel): Observable<ResponseModel> {
    return this.httpClient.
      post<ResponseModel>(environment.apiUrl + 'VehicleDailyBeat', vm);
  }

  delete(id: number): Observable<ResponseModel> {
    return this.httpClient.
      delete<ResponseModel>(environment.apiUrl + 'VehicleDailyBeat' + "/" + id);
  }
}
