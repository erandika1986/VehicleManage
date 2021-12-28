import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DropDownModel } from 'app/models/common/drop-down.modal';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DropdownService {

  constructor(private httpClient: HttpClient) { }

  getProductCategories(): Observable<DropDownModel[]> {
    return this.httpClient.
      get<DropDownModel[]>(environment.apiUrl + 'DropDownService/getProductCategories');
  }

  getProductSubCategories(categoryId:number): Observable<DropDownModel[]> {
    return this.httpClient.
      get<DropDownModel[]>(environment.apiUrl + 'DropDownService/getProductSubCategories/'+categoryId);
  }

  getProducts(subCategoryId:number): Observable<DropDownModel[]> {
    return this.httpClient.
      get<DropDownModel[]>(environment.apiUrl + 'DropDownService/getProducts/'+subCategoryId);
  }

  getProductsForSupplier(subCategoryId:number,supplierId:number): Observable<DropDownModel[]> {
    return this.httpClient.
      get<DropDownModel[]>(environment.apiUrl + 'DropDownService/getProducts/'+subCategoryId+'/'+supplierId);
  }
}
