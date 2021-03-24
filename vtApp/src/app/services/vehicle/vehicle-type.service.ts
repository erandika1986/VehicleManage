import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ResponseModel } from 'app/models/common/response.model';
import { VehicleTypeMasterDataModel } from 'app/models/vehicle/vehicle-type-master-data.model';
import { VehicleTypePaginatedItemsModel } from 'app/models/vehicle/vehicle-type-paginated.items.model';
import { VehicleTypeModel } from 'app/models/vehicle/vehicle-type.model';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class VehicleTypeService {

  constructor(private httpClient: HttpClient) { }

  // Master data Related method
  getVehicleTypeMasterData(): Observable<VehicleTypeMasterDataModel> {
    return this.httpClient.
      get<VehicleTypeMasterDataModel>(environment.apiUrl + 'VehicleType/getVehicleTypeMasterData');
  }

  // Get Vehicle Types
  getAllVehicleTypes(): Observable<VehicleTypeModel[]> {
    return this.httpClient.
      get<VehicleTypeModel[]>(environment.apiUrl + 'VehicleType/getAllVehicleTypes');
  }


  // Save Vehicle Type
  saveVehicleType(vehicleTypeModel: VehicleTypeModel): Observable<ResponseModel> {
    return this.httpClient.
      post<ResponseModel>(environment.apiUrl + 'VehicleType/saveVehicleType', vehicleTypeModel);
  }

  // Get Vehicle Type By Id
  getVehicleTypeById(id: number): Observable<VehicleTypeModel> {
    return this.httpClient.
      get<VehicleTypeModel>(environment.apiUrl + 'VehicleType/getVehicleTypeById/' + id);
  }


  // Delete Existing Vehicle Type
  deleteVehicleType(id: number): Observable<ResponseModel> {
    return this.httpClient.
      delete<ResponseModel>(environment.apiUrl + 'VehicleType/deleteVehicleType/' + id);
  }
}
