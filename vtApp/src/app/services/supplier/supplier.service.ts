import { SupplierModel } from './../../models/supplier/supplier.model';
import { ResponseModel } from './../../models/common/response.model';
import { SupplierModule } from './../../main/admin/supplier/supplier.module';
import { environment } from './../../../environments/environment.hmr';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SupplierService {

  constructor(private httpClient: HttpClient) { }

  GetAllSuppliers(): Observable<SupplierModel[]> {
    return this.httpClient.
      get<SupplierModel[]>(environment.apiUrl + 'Supplier');
  }

  GetSupplierById(id: number): Observable<SupplierModel> {
    return this.httpClient.
      get<SupplierModel>(environment.apiUrl + 'Supplier' + "/" + id);
  }

  SaveSupplier(vm: SupplierModel): Observable<ResponseModel> {
    return this.httpClient.
      post<ResponseModel>(environment.apiUrl + 'Supplier', vm);
  }

  DeleteSupplier(id: number): Observable<ResponseModel> {
    return this.httpClient.
      delete<ResponseModel>(environment.apiUrl + 'Supplier' + "/" + id);
  }
}
