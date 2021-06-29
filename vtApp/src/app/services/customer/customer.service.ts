import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CustomerMasterDataModel } from 'app/models/customer/customer.master.data.model';
import { CustomerModel } from 'app/models/customer/customer.model';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  constructor(private httpClient: HttpClient) { }


  getAllCustomers(): Observable<CustomerModel[]> {
    return this.httpClient.
      get<CustomerModel[]>(environment.apiUrl + 'Customer');
  }
  
  getCustomerById(id: number): Observable<CustomerModel> {
    return this.httpClient.
      get<CustomerModel>(environment.apiUrl + 'Customer' + "/" + id);
  }

  saveCustomer(vm: CustomerModel): Observable<CustomerModel> {
    return this.httpClient.
      post<CustomerModel>(environment.apiUrl + 'Customer', vm);
  }

  delete(id: number): Observable<CustomerModel> {
    return this.httpClient.
      delete<CustomerModel>(environment.apiUrl + 'Customer' + "/" + id);
  }

  getCustomerMasterData(): Observable<CustomerMasterDataModel> {
    return this.httpClient.
      get<CustomerMasterDataModel>(environment.apiUrl + 'Customer' + "/" + this.getCustomerMasterData);
  }
  
}
