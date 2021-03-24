import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DropDownModel } from 'app/models/common/drop-down.modal';
import { ResponseModel } from 'app/models/common/response.model';
import { CodeModel } from 'app/models/vehicle/code.model';
import { environment } from 'environments/environment';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MasterDataCodeService {

  onFilterChanged: Subject<any>;

  constructor(private httpClient: HttpClient) {
    this.onFilterChanged = new Subject();
  }

  getAllCodeTypes(): Observable<DropDownModel[]> {
    return this.httpClient.
      get<DropDownModel[]>(environment.apiUrl + 'MasterData/getAllCodeTypes');
  }

  getAllCodesForSelectedCodeType(type: number): Observable<CodeModel[]> {
    return this.httpClient.
      get<CodeModel[]>(environment.apiUrl + 'MasterData/getAllCodesForSelectedCodeType' + "/" + type);
  }

  saveCode(vm: CodeModel): Observable<ResponseModel> {
    return this.httpClient.
      post<ResponseModel>(environment.apiUrl + 'MasterData/saveCode', vm);
  }

  deleteCode(vm: CodeModel): Observable<ResponseModel> {
    return this.httpClient.
      post<ResponseModel>(environment.apiUrl + 'MasterData/deleteCode', vm);
  }
}
