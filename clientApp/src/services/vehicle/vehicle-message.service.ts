import { Injectable } from '@angular/core';
import { Subject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class VehicleMessageService {

  constructor() { }

  private subject = new Subject<any>();

  // vehicle type
  sendMessageToReloadVehicleTypesTable(isReloadRequired: boolean) {
    this.subject.next({ isReload: isReloadRequired });
  }
  getMessageToReloadVehicleTypesTable(): Observable<any> {
    return this.subject.asObservable();
  }

  // vehicle DOCM
  sendMessageToReloadVehicleDOCM(isReloadRequired: boolean) {
    this.subject.next({ isReload: isReloadRequired });
  }
  getMessageToReloadVehicleDOCM(): Observable<any> {
    return this.subject.asObservable();
  }

  // vehicle FR
  sendMessageToReloadVehicleFR(isReloadRequired: boolean) {
    this.subject.next({ isReload: isReloadRequired });
  }
  getMessageToReloadVehicleFR(): Observable<any> {
    return this.subject.asObservable();
  }

    // vehicle GN
    sendMessageToReloadVehicleGN(isReloadRequired: boolean) {
      this.subject.next({ isReload: isReloadRequired });
    }
    getMessageToReloadVehicleGN(): Observable<any> {
      return this.subject.asObservable();
    }

        // vehicle I
        sendMessageToReloadVehicleI(isReloadRequired: boolean) {
          this.subject.next({ isReload: isReloadRequired });
        }
        getMessageToReloadVehicleI(): Observable<any> {
          return this.subject.asObservable();
        }

        // vehicle RL
        sendMessageToReloadVehicleRL(isReloadRequired: boolean) {
          this.subject.next({ isReload: isReloadRequired });
        }
        getMessageToReloadVehicleRL(): Observable<any> {
          return this.subject.asObservable();
        }

        // vehicle ET
        sendMessageToReloadVehicleET(isReloadRequired: boolean) {
          this.subject.next({ isReload: isReloadRequired });
        }
        getMessageToReloadVehicleET(): Observable<any> {
          return this.subject.asObservable();
        }

        // vehicle AC
        sendMessageToReloadVehicleAC(isReloadRequired: boolean) {
          this.subject.next({ isReload: isReloadRequired });
        }
        getMessageToReloadVehicleAC(): Observable<any> {
          return this.subject.asObservable();
        }

        // vehicle EOM
        sendMessageToReloadVehicleEOM(isReloadRequired: boolean) {
          this.subject.next({ isReload: isReloadRequired });
        }
        getMessageToReloadVehicleEOM(): Observable<any> {
          return this.subject.asObservable();
        }

        // vehicle FFM
        sendMessageToReloadVehicleFFM(isReloadRequired: boolean) {
          this.subject.next({ isReload: isReloadRequired });
        }
        getMessageToReloadVehicleFFM(): Observable<any> {
          return this.subject.asObservable();
        }

        // vehicle GBOM
        sendMessageToReloadVehicleGBOM(isReloadRequired: boolean) {
          this.subject.next({ isReload: isReloadRequired });
        }
        getMessageToReloadVehicleGBOM(): Observable<any> {
          return this.subject.asObservable();
        }
}
