import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { fuseAnimations } from '@fuse/animations';
import { FuseSidebarService } from '@fuse/components/sidebar/sidebar.service';
import { UserService } from 'app/services/user/user.service';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, takeUntil } from 'rxjs/operators';
import { UserDetailComponent } from './user-detail/user-detail.component';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations   : fuseAnimations
})
export class UsersComponent  implements OnInit, OnDestroy {

  dialogRef: any;
  hasSelectedContacts: boolean;
  searchInput: FormControl;

      // Private
      private _unsubscribeAll: Subject<any>;
  
  constructor(        private _userService: UserService,
    private _fuseSidebarService: FuseSidebarService,
    private _matDialog: MatDialog) { 
              // Set the defaults
      this.searchInput = new FormControl('');
      // Set the private defaults
      this._unsubscribeAll = new Subject();
    }

  ngOnInit(): void {

this.searchInput.valueChanges
    .pipe(
        takeUntil(this._unsubscribeAll),
        debounceTime(300),
        distinctUntilChanged()
    )
    .subscribe(searchText => {
        this._userService.onSearchTextChanged.next(searchText);
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

    newContact(): void
    {
        this.dialogRef = this._matDialog.open(UserDetailComponent, {
            panelClass: 'contact-form-dialog',
            data      : {
                action: 'new'
            }
        });

        this.dialogRef.afterClosed()
            .subscribe((response: FormGroup) => {
                if ( !response )
                {
                    return;
                }

                this._userService.saveVehicle(response.getRawValue());
            });
    }

        /**
     * Toggle the sidebar
     *
     * @param name
     */
    toggleSidebar(name): void
    {
        this._fuseSidebarService.getSidebar(name).toggleOpen();
    }


}


