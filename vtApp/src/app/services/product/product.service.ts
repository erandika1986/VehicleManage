import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DropDownModel } from 'app/models/common/drop-down.modal';
import { ResponseModel } from 'app/models/common/response.model';
import { upload, Upload } from 'app/models/common/upload';
import { ProductCategoryModel } from 'app/models/product/product.category.model';
import { ProductModel } from 'app/models/product/product.model';
import { ProductSubCategoryModel } from 'app/models/product/product.sub.category.model';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private httpClient: HttpClient) { }

  //For Product Category

  getAllProductCategories(): Observable<ProductCategoryModel[]> {
    return this.httpClient.
      get<ProductCategoryModel[]>(environment.apiUrl + 'ProductCategory');
  }

  getProductCategoryById(id: number): Observable<ProductCategoryModel> {
    return this.httpClient.
      get<ProductCategoryModel>(environment.apiUrl + 'ProductCategory' + "/" + id);
  }

  saveProductCategory(vm: ProductCategoryModel): Observable<ResponseModel> {
    return this.httpClient.
      post<ResponseModel>(environment.apiUrl + 'ProductCategory', vm);
  }

  deleteProductCategory(id: number): Observable<ResponseModel> {
    return this.httpClient.
      delete<ResponseModel>(environment.apiUrl + 'ProductCategory' + "/" + id);
  }

  uploadProductCategoryImage(data: FormData): Observable<Upload> {
    return this.httpClient.post(environment.apiUrl + 'ProductCategory/uploadProductCategoryImage', data,{reportProgress: true,observe: 'events'}).pipe(upload());;
  }

  downloadProductCategoryImage(id: number): Observable<any> {
    return this.httpClient.get<any>(environment.apiUrl +'ProductCategory/downloadProductCategoryImage/'+id,{headers:{'filedownload':''}, observe: 'events',reportProgress:true });
  }

  //For Product Sub Category
  getAllByCategoryId(categoryId:number): Observable<ProductSubCategoryModel[]> {
    return this.httpClient.
      get<ProductSubCategoryModel[]>(environment.apiUrl + 'ProductSubCategory/getAllByCategoryId/'+categoryId);
  }

  getProductSubCategoryById(id: number): Observable<ProductSubCategoryModel> {
    return this.httpClient.
      get<ProductSubCategoryModel>(environment.apiUrl + 'ProductSubCategory' + "/" + id);
  }

  saveProductSubCategory(vm: ProductSubCategoryModel): Observable<ResponseModel> {
    return this.httpClient.
      post<ResponseModel>(environment.apiUrl + 'ProductSubCategory', vm);
  }

  deleteProductSubCategory(id: number): Observable<ResponseModel> {
    return this.httpClient.
      delete<ResponseModel>(environment.apiUrl + 'ProductSubCategory' + "/" + id);
  }

  getProductCategories(): Observable<DropDownModel[]> {
    return this.httpClient.
      get<DropDownModel[]>(environment.apiUrl + 'ProductSubCategory/getProductCategories');
  }

  uploadSubProductCategoryImage(data: FormData): Observable<Upload> {
    return this.httpClient.post(environment.apiUrl + 'ProductSubCategory/uploadSubProductCategoryImage', data,{reportProgress: true,observe: 'events'}).pipe(upload());;
  }

  downloadProductSubCategoryImage(id: number): Observable<any> {
    return this.httpClient.get<any>(environment.apiUrl +'ProductSubCategory/downloadProductSubCategoryImage/'+id,{headers:{'filedownload':''}, observe: 'events',reportProgress:true });
  }

  //For Product
  getAllProductSubCategories(subCategoryId:number): Observable<ProductModel[]> {
    return this.httpClient.
      get<ProductModel[]>(environment.apiUrl + 'Product/getAllProductSubCategories/'+subCategoryId);
  }

  getProductById(id: number): Observable<ProductModel> {
    return this.httpClient.
      get<ProductModel>(environment.apiUrl + 'Product' + "/" + id);
  }

  saveProduct(vm: ProductModel): Observable<ResponseModel> {
    return this.httpClient.
      post<ResponseModel>(environment.apiUrl + 'Product', vm);
  }

  deleteProduct(id: number): Observable<ResponseModel> {
    return this.httpClient.
      delete<ResponseModel>(environment.apiUrl + 'Product' + "/" + id);
  }

  getProductSubCategories(subCategoryId:number): Observable<DropDownModel[]> {
    return this.httpClient.
      get<DropDownModel[]>(environment.apiUrl + 'Product/getProductSubCategories/'+subCategoryId);
  }


}
