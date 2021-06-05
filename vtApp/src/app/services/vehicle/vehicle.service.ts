import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { VehicleTypeMasterDataModel } from 'app/models/vehicle/vehicle-type-master-data.model';
import { environment } from 'environments/environment';
import { VehicleTypePaginatedItemsModel } from 'app/models/vehicle/vehicle-type-paginated.items.model';
import { VehicleMasterDataModel } from 'app/models/vehicle/vehicle-master-data.model';
import { VehicleTypeModel } from 'app/models/vehicle/vehicle-type.model';
import { ResponseModel } from 'app/models/common/response.model';
import { VehiclePaginatedItemsModel } from 'app/models/vehicle/vehicle-paginated.items.model';
import { VehicleModel } from 'app/models/vehicle/vehicle.model';
import { VehicleResponseModel } from 'app/models/vehicle/vehicle-response.model';

@Injectable({
  providedIn: 'root'
})
export class VehicleService {

  constructor(private httpClient: HttpClient) { }

  // Get Vehicle Master Data
  getVehicleMasterData(): Observable<VehicleMasterDataModel> {
    return this.httpClient.
      get<VehicleMasterDataModel>(environment.apiUrl + 'Vehicle/getVehicleMasterData');
  }

  // get vehicles
  getAllVehicles(pageSize: number, currentPage: number,sortBy:string,sortDirection:string,searchText:string): Observable<VehiclePaginatedItemsModel> {
    return this.httpClient.
      get<VehiclePaginatedItemsModel>(environment.apiUrl + 'Vehicle/getAllVehicles/' + pageSize + '/' + currentPage +'/'+sortBy+'/'+sortDirection+'/'+searchText);
  }

  // add new vehicle
  addNewVehicle(vehicleModel: VehicleModel): Observable<VehicleResponseModel> {
    return this.httpClient.
      post<VehicleResponseModel>(environment.apiUrl + 'Vehicle/addNewVehicle', vehicleModel);
  }

  // get vehicle by id
  getVehicleById(id: number): Observable<VehicleModel> {
    return this.httpClient.
      get<VehicleModel>(environment.apiUrl + 'Vehicle/getVehicleById/' + id);
  }

  // update existing vehicle
  updateVehicle(vehicleModel: VehicleModel): Observable<ResponseModel> {
    return this.httpClient.
      put<ResponseModel>(environment.apiUrl + 'Vehicle/updateVehicle', vehicleModel);
  }

  // delete existing vehicle
  deleteVehicle(id: number): Observable<ResponseModel> {
    return this.httpClient.
      delete<ResponseModel>(environment.apiUrl + 'Vehicle/deleteVehicle/' + id);
  }

  // Check vehicle exists
  isVehicleAlreadyExists(regNo: string): Observable<ResponseModel> {
    return this.httpClient.
      get<ResponseModel>(environment.apiUrl + 'Vehicle/isVehicleAlreadyExists/' + regNo);
  }

}
