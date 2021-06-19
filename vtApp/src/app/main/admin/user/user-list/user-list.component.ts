import { Component, OnDestroy, OnInit, TemplateRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { fuseAnimations } from '@fuse/animations';
import { FuseConfirmDialogComponent } from '@fuse/components/confirm-dialog/confirm-dialog.component';
import { UserDataSource } from 'app/models/user/user.datasource';
import { User } from 'app/models/user/user.model';
import { UserService } from 'app/services/user/user.service';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { UserDetailComponent } from '../user-detail/user-detail.component';

@Component({
  selector: 'users-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations   : fuseAnimations
})
export class UserListComponent implements OnInit, OnDestroy {

  @ViewChild('dialogContent')
  dialogContent: TemplateRef<any>;

  users: any;
  user: any;
  //dataSource: UserDataSource | null;
  dataSource = new MatTableDataSource([]);
  displayedColumns = ['image', 'firstName', 'email', 'mobileNo', 'buttons'];
  selectedUsers: any[];
  checkboxes: {};
  dialogRef: any;
  confirmDialogRef: MatDialogRef<FuseConfirmDialogComponent>;
  
      // Private
      private _unsubscribeAll: Subject<any>;
      
  constructor(        private _userService: UserService,
    public _matDialog: MatDialog) {
      this._unsubscribeAll = new Subject();
     }

  ngOnInit(): void {

    //this.dataSource = new UserDataSource(this._userService);

    this._userService.getAllUsers(0,true)
    .subscribe(response=>{
        this.dataSource = new MatTableDataSource(response);
        this.dataSource.connect();
    });

    this._userService.onContactsChanged
    .pipe(takeUntil(this._unsubscribeAll))
    .subscribe(contacts => {
        this.users = contacts;

/*         this.checkboxes = {};
        contacts.map(contact => {
            this.checkboxes[contact.id] = false;
        }); */
    });

    this._userService.onSelectedContactsChanged
    .pipe(takeUntil(this._unsubscribeAll))
    .subscribe(selectedContacts => {
/*         for ( const id in this.checkboxes )
        {
            if ( !this.checkboxes.hasOwnProperty(id) )
            {
                continue;
            }

            this.checkboxes[id] = selectedContacts.includes(id);
        } */
        this.selectedUsers = selectedContacts;
    });

    this._userService.onUserDataChanged
    .pipe(takeUntil(this._unsubscribeAll))
    .subscribe(user => {
        this.user = user;
    });

this._userService.onFilterChanged
    .pipe(takeUntil(this._unsubscribeAll))
    .subscribe(() => {
        //this._userService.deselectContacts();
    });
  }

  ngOnDestroy(): void
  {
      // Unsubscribe from all subscriptions
      this._unsubscribeAll.next();
      this._unsubscribeAll.complete();
  }

      /**
     * Edit contact
     *
     * @param contact
     */
    editUser(user:User): void
    {
        this.dialogRef = this._matDialog.open(UserDetailComponent, {
            panelClass: 'contact-form-dialog',
            data      : {
              user: user,
                action : 'edit'
            }
        });

        this.dialogRef.afterClosed()
            .subscribe(response => {
                if ( !response )
                {
                    return;
                }
                const actionType: string = response[0];
                const formData: FormGroup = response[1];
                switch ( actionType )
                {
                    /**
                     * Save
                     */
                    case 'save':

                        this._userService.saveVehicle(formData.getRawValue());

                        break;
                    /**
                     * Delete
                     */
                    case 'delete':

                        this.deleteContact(user);

                        break;
                }
            });
    }

    /**
     * Delete Contact
     */
    deleteContact(user:User): void
    {
        this.confirmDialogRef = this._matDialog.open(FuseConfirmDialogComponent, {
            disableClose: false
        });

        this.confirmDialogRef.componentInstance.confirmMessage = 'Are you sure you want to delete?';

        this.confirmDialogRef.afterClosed().subscribe(result => {
            if ( result )
            {
                this._userService.deleteVehicleRevenueLicence(user.id);
            }
            this.confirmDialogRef = null;
        });

    }

}
