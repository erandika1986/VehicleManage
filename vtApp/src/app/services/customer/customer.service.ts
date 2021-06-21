import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CustomerModel } from 'app/models/customer/customer.model';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  constructor(private httpClient: HttpClient) { }

  getCustomerById(id: number): Observable<CustomerModel> {
    return this.httpClient.
      get<CustomerModel>(environment.apiUrl + 'Customer' + "/" + id);
  }
}
