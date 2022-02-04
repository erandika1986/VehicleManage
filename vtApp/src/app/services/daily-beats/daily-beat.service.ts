import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from 'environments/environment';
import { VehicleBeatMasterDataModel } from 'app/models/dialy-beat/vehicle-beat-master-data.model';
import { VehicleBeatFilterModel } from 'app/models/dialy-beat/vehicle-beat-filter.model';
import { DailyVehicleBeatModel } from 'app/models/dialy-beat/daily-vehicle-beat.model';
import { ResponseModel } from 'app/models/common/response.model';
import { VehicleDailyBeatPaginatedItemsModel } from 'app/models/dialy-beat/vehicle-daily-beat-paginated-items.model';

@Injectable({
  providedIn: 'root'
})
export class DailyBeatService {

  onFilterChanged: Subject<VehicleBeatFilterModel>;
  onSearchTextChanged : Subject<string>;
  onMasterDataRecieved:Subject<VehicleBeatMasterDataModel>;
  onDailyBeatSaved:Subject<any>;

  constructor(private httpClient: HttpClient) {
    this.onFilterChanged = new Subject();
    this.onSearchTextChanged =  new Subject();
    this.onMasterDataRecieved = new Subject();
    this.onDailyBeatSaved = new Subject();
   }

  getMasterData(): Observable<VehicleBeatMasterDataModel> {
    return this.httpClient.
      get<VehicleBeatMasterDataModel>(environment.apiUrl + 'VehicleDailyBeat' + "/getMasterData");
  }

  getAllVehicleBeatRecord(filter: VehicleBeatFilterModel): Observable<VehicleDailyBeatPaginatedItemsModel> {
    return this.httpClient.post<VehicleDailyBeatPaginatedItemsModel>(environment.apiUrl + 'VehicleDailyBeat/getAllVehicleBeatRecord', filter);
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
