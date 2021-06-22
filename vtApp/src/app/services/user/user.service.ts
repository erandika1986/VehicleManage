import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'environments/environment';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { upload, Upload } from 'app/models/common/upload';
import { ResponseModel } from 'app/models/common/response.model';
import { User } from 'app/models/user/user.model';
import { UserMasterDataModel } from 'app/models/user/user.master.data.model';
import { ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  onUsersChanged: BehaviorSubject<any>;
  onSelectedUsersChanged: BehaviorSubject<any>;
  onUserDataChanged: BehaviorSubject<any>;
  onSearchTextChanged: Subject<any>;
  onFilterChanged: Subject<any>;
  onMasterDataRecieved:Subject<any>;

  users: User[];
  user: any;
  selectedUsers: string[] = [];

  searchText: string;
  filterBy: string;
  
  constructor(private httpClient: HttpClient) {
    this.onUsersChanged = new BehaviorSubject([]);
    this.onSelectedUsersChanged = new BehaviorSubject([]); 
    this.onUserDataChanged = new BehaviorSubject([]);
    this.onSearchTextChanged = new Subject();
    this.onFilterChanged = new Subject();
    this.onMasterDataRecieved = new Subject();
   }



  saveVehicle(model: User): Observable<ResponseModel> {
    return this.httpClient.
      post<ResponseModel>
      (environment.apiUrl + 'User/saveUser', model);
  }

    // get
  getAllUsers(roleId: number,status:number): Observable<User[]> {
      return this.httpClient.
        get<User[]>
        (environment.apiUrl + 'User/getAllUsers/' + roleId+"/"+status);
  }

      // get
      getAllUsers1(roleId: number,status:number): Promise<User[]> {
        return new Promise((resolve,reject) =>{
          this.httpClient.get(environment.apiUrl + 'User/getAllUsers/' + roleId+"/"+status)
            .subscribe((response:User[])=>{
                this.users = response;
                this.onUsersChanged.next(this.users);
                resolve(response);
            },reject);
        });
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
