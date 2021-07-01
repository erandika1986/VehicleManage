import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DropDownModel } from 'app/models/common/drop-down.modal';
import { ResponseModel } from 'app/models/common/response.model';
import { upload, Upload } from 'app/models/common/upload';
import { ProductCategoryModel } from 'app/models/product/product.category.model';
import { ProductImageModel } from 'app/models/product/product.image.model';
import { ProductModel } from 'app/models/product/product.model';
import { ProductSubCategoryModel } from 'app/models/product/product.sub.category.model';
import { environment } from 'environments/environment';
import { BehaviorSubject, Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  selectedCategoryId:number=0;
  selectedSubCategoryId:number=0;
  selectedProductId:number=0;

  onProductImageUploaded: Subject<any>;
  
  constructor(private httpClient: HttpClient) { 
    this.onProductImageUploaded = new Subject();
  }

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
    this.selectedCategoryId = categoryId;
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
  getAllProducts(subCategoryId:number): Observable<ProductModel[]> {
    this.selectedSubCategoryId = subCategoryId;
    return this.httpClient.
      get<ProductModel[]>(environment.apiUrl + 'Product/getAllProducts/'+subCategoryId);
  }

  getAllProductImages(productId:number): Observable<ProductImageModel[]> {

    return this.httpClient.
      get<ProductImageModel[]>(environment.apiUrl + 'Product/getAllProductImages/'+productId);
  }

  makeDefaultImage(photo: ProductImageModel): Observable<ResponseModel> {
    return this.httpClient.post<ResponseModel>(environment.apiUrl + 'Product/makeDefaultImage', photo);
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

  getProductSubCategories(categoryId:number): Observable<DropDownModel[]> {
    this.selectedCategoryId=categoryId;
    return this.httpClient.
      get<DropDownModel[]>(environment.apiUrl + 'Product/getProductSubCategories/'+categoryId);
  }

  getSuppliers(): Observable<DropDownModel[]> {

    return this.httpClient.
      get<DropDownModel[]>(environment.apiUrl + 'Product/getSuppliers');
  }

  uploadProductImage(data: FormData): Observable<Upload> {
    return this.httpClient.post(environment.apiUrl + 'Product/uploadProductImage', data,{reportProgress: true,observe: 'events'}).pipe(upload());;
  }

  downloadProductImage(id: number): Observable<any> {
    return this.httpClient.get<any>(environment.apiUrl +'Product/downloadProductImage/'+id,{headers:{'filedownload':''}, observe: 'events',reportProgress:true });
  }


}
