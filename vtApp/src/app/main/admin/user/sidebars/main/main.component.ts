import { Component, OnDestroy, OnInit } from '@angular/core';
import { UserService } from 'app/services/user/user.service';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

@Component({
  selector: 'users-main-sidebar',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit, OnDestroy {

  user: any;
  filterBy: string;

  // Private
  private _unsubscribeAll: Subject<any>;
  
  constructor(private _usersService: UserService) { 
    this._unsubscribeAll = new Subject();
  }

  ngOnInit(): void {

    this.filterBy = this._usersService.filterBy || 'all';
 
    this._usersService.onUserDataChanged
        .pipe(takeUntil(this._unsubscribeAll))
        .subscribe(user => {
            this.user = user;
        }); 
  }

      /**
     * On destroy
     */
    ngOnDestroy(): void
    {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }

    changeFilter(filter): void
    {
        this.filterBy = filter;
        this._usersService.onFilterChanged.next(this.filterBy);
    }

}
