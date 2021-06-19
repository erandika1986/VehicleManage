import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'environments/environment';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { upload, Upload } from 'app/models/common/upload';
import { ResponseModel } from 'app/models/common/response.model';
import { User } from 'app/models/user/user.model';
import { UserMasterDataModel } from 'app/models/user/user.master.data.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  onContactsChanged: BehaviorSubject<any>;
  onSelectedContactsChanged: BehaviorSubject<any>;
  onUserDataChanged: BehaviorSubject<any>;
  onSearchTextChanged: Subject<any>;
  onFilterChanged: Subject<any>;

  users: User[];
  user: any;
  selectedContacts: string[] = [];

  searchText: string;
  filterBy: string;
  
  constructor(private httpClient: HttpClient) {
    this.onContactsChanged = new BehaviorSubject([]);
    this.onSelectedContactsChanged = new BehaviorSubject([]);
    this.onUserDataChanged = new BehaviorSubject([]);
    this.onSearchTextChanged = new Subject();
    this.onFilterChanged = new Subject();
   }

  saveVehicle(model: User): Observable<ResponseModel> {
    return this.httpClient.
      post<ResponseModel>
      (environment.apiUrl + 'User/saveVehicle', model);
  }

    // get
  getAllUsers(roleId: number,status:boolean): Observable<User[]> {
      return this.httpClient.
        get<User[]>
        (environment.apiUrl + 'User/getAllUsers/' + roleId+"/"+status);
  }

        // get
  getUserMasterData(): Observable<UserMasterDataModel> {
          return this.httpClient.
            get<UserMasterDataModel>
            (environment.apiUrl + 'User/getUserMasterData' );
  }

  // delete existing record
  deleteVehicleRevenueLicence(id: number): Observable<ResponseModel> {
    return this.httpClient.
      delete<ResponseModel>(environment.apiUrl + 'User/deleteUser/' + id);
  }

  uploadUserImage(data: FormData): Observable<Upload> {
    return this.httpClient.post(environment.apiUrl + 'User/uploadUserImage', data,{reportProgress: true,observe: 'events'}).pipe(upload());;
  }

  downloadUserImage(id: number,type:number): Observable<any> {
    return this.httpClient.get<any>(environment.apiUrl +'User/downloadUserImage/'+id+"/"+type,{headers:{'filedownload':''}, observe: 'events',reportProgress:true });
  }
}
