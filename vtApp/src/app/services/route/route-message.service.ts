import { Injectable } from '@angular/core';
import { Subject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RouteMessageService {

  constructor() { }

  private subject = new Subject<any>();

  sendModelSaveMessasge(isReloadRequired: boolean) {
    this.subject.next({ isReload: isReloadRequired });
  }

  getModelSaveMessage(): Observable<any> {
    return this.subject.asObservable();
  }
}
