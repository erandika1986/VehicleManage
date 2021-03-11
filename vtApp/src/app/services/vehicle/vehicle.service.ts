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

  // Master data Related method
  getVehicleTypeMasterData(): Observable<VehicleTypeMasterDataModel> {
    return this.httpClient.
      get<VehicleTypeMasterDataModel>(environment.apiUrl + 'Vehicle/getVehicleTypeMasterData');
  }

  // Get Vehicle Types
  getAllVehicleTypes(pageSize: number, currentPage: number): Observable<VehicleTypePaginatedItemsModel> {
    return this.httpClient.
      get<VehicleTypePaginatedItemsModel>(environment.apiUrl + 'Vehicle/getAllVehicleTypes/' + pageSize + '/' + currentPage);
  }

  // Get Vehicle Master Data
  getVehicleMasterData(): Observable<VehicleMasterDataModel> {
    return this.httpClient.
      get<VehicleMasterDataModel>(environment.apiUrl + 'Vehicle/getVehicleMasterData');
  }

  // Add new Vehicle Type
  addNewVehicleType(vehicleTypeModel: VehicleTypeModel): Observable<ResponseModel> {
    return this.httpClient.
      post<ResponseModel>(environment.apiUrl + 'Vehicle/addNewVehicleType', vehicleTypeModel);
  }

  // Get Vehicle Type By Id
  getVehicleTypeById(id: number): Observable<VehicleTypeModel> {
    return this.httpClient.
      get<VehicleTypeModel>(environment.apiUrl + 'Vehicle/getVehicleTypeById/' + id);
  }

  // Update Existing Vehicle Type
  updateVehicleType(vehicleTypeModel: VehicleTypeModel): Observable<ResponseModel> {
    return this.httpClient.
      put<ResponseModel>(environment.apiUrl + 'Vehicle/updateVehicleType', vehicleTypeModel);
  }

  // Delete Existing Vehicle Type
  deleteVehicleType(id: number): Observable<ResponseModel> {
    return this.httpClient.
      delete<ResponseModel>(environment.apiUrl + 'Vehicle/deleteVehicleType/' + id);
  }

  // get vehicles
  getAllVehicles(pageSize: number, currentPage: number): Observable<VehiclePaginatedItemsModel> {
    return this.httpClient.
      get<VehiclePaginatedItemsModel>(environment.apiUrl + 'Vehicle/getAllVehicles/' + pageSize + '/' + currentPage);
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
