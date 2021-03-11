import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'environments/environment';
import { RoutePaginatedItemsModel } from 'app/models/route/route-paginated-items.model';
import { RouteModel } from 'app/models/route/route.model';
import { ResponseModel } from 'app/models/common/response.model';

@Injectable({
  providedIn: 'root'
})
export class RouteService {

  constructor(private httpClient: HttpClient) { }


  getAllRoutes(pageSize: number, currentPage: number): Observable<RoutePaginatedItemsModel> {
    return this.httpClient.
      get<RoutePaginatedItemsModel>(environment.apiUrl + 'Route' + "/" + pageSize + "/" + currentPage);
  }

  getRouteById(id: number): Observable<RouteModel> {
    return this.httpClient.
      get<RouteModel>(environment.apiUrl + 'Route' + "/" + id);
  }

  addNewRoute(vm: RouteModel): Observable<ResponseModel> {
    return this.httpClient.
      post<ResponseModel>(environment.apiUrl + 'Route', vm);
  }

  updateRoute(vm: RouteModel): Observable<ResponseModel> {
    return this.httpClient.
      put<ResponseModel>(environment.apiUrl + 'Route', vm);
  }

  delete(id: number): Observable<ResponseModel> {
    return this.httpClient.
      delete<ResponseModel>(environment.apiUrl + 'Route' + "/" + id);
  }


}
