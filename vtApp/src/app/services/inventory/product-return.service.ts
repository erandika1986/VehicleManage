import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DropDownModel } from 'app/models/common/drop-down.modal';
import { ResponseModel } from 'app/models/common/response.model';
import { ProductPaginatedItemsModel } from 'app/models/product-return/product.paginated.items.model';
import { ProductReturnFilterModel } from 'app/models/product-return/product.return.filter.model';
import { ProductReturnMasterDataModel } from 'app/models/product-return/product.return.master.data.model';
import { ProductReturnModel } from 'app/models/product-return/product.return.model';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductReturnService {

  constructor(private httpClient: HttpClient) { }

  getProductReturnMasterData(): Observable<ProductReturnMasterDataModel> {
    return this.httpClient.
      get<ProductReturnMasterDataModel>(environment.apiUrl + "ProductReturn/getProductReturnMasterData");
  }

  getAllVehicleReturnRecord(filter: ProductReturnFilterModel): Observable<ProductPaginatedItemsModel> {
    return this.httpClient.post<ProductPaginatedItemsModel>(environment.apiUrl + 'ProductReturn/getAllVehicleReturnRecord', filter);
  }

  SaveProductReturn(vm: ProductReturnModel): Observable<ResponseModel> {
    return this.httpClient.
      post<ResponseModel>(environment.apiUrl + 'ProductReturn/saveProductReturn', vm);
  }

  deleteProductReturn(id: number): Observable<ResponseModel> {
    return this.httpClient.
      delete<ResponseModel>(environment.apiUrl + "ProductReturn/deleteProductReturn/" + id);
  }

  getSalesOrderListForSelectedClient(clientId: number): Observable<DropDownModel[]> {
    return this.httpClient.
    get<DropDownModel[]>(environment.apiUrl + "ProductReturn/getSalesOrderListForSelectedClient/" + clientId);
  }

  getProductReturn(id: number): Observable<ProductReturnModel> {
    return this.httpClient.
      get<ProductReturnModel>(environment.apiUrl + "ProductReturn/getProductReturn/" + id);
  }

}
