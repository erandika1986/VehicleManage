import { Component, OnDestroy, OnInit, TemplateRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { fuseAnimations } from '@fuse/animations';
import { FuseConfirmDialogComponent } from '@fuse/components/confirm-dialog/confirm-dialog.component';
import { UserDataSource } from 'app/models/user/user.datasource';
import { UserFilter } from 'app/models/user/user.filter.model';
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
  filterValue:string;
  dataSource = new MatTableDataSource([]);
  displayedColumns = ['image', 'firstName', 'email', 'mobileNo', 'buttons'];
  selectedUsers: any[];
  checkboxes: {};
  dialogRef: any;
  confirmDialogRef: MatDialogRef<FuseConfirmDialogComponent>;
  
      // Private
      private _unsubscribeAll: Subject<any>;
      
  constructor
  (
      private _userService: UserService,
        public _matDialog: MatDialog) {
      this._unsubscribeAll = new Subject();
     }

  ngOnInit(): void {

    this._userService.onFilterChanged.subscribe((response:UserFilter)=>{
        this.loadUsers(response.selectedRoleId,response.selectdStatus==1?true:false);
    });

    this._userService.onSearchTextChanged.subscribe(response=>{
        this.filterValue = response.trim(); // Remove whitespace
        this.filterValue = this.filterValue.toLowerCase(); // Datasource defaults to lowercase matches
        this.dataSource.filter = this.filterValue;
        
    });

    this.loadUsers(0,true);
  }

  ngOnDestroy(): void
  {
      // Unsubscribe from all subscriptions
      this._unsubscribeAll.next();
      this._unsubscribeAll.complete();
  }

  loadUsers(roleId:number,status:boolean)
  {
    this._userService.getAllUsers(roleId,status)
    .subscribe(response=>{
        this.dataSource = new MatTableDataSource(response);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;

    });
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
