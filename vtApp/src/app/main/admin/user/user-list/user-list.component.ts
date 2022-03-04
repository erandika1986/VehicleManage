import { Component, OnDestroy, OnInit, TemplateRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { fuseAnimations } from '@fuse/animations';
import { FuseConfirmDialogComponent } from '@fuse/components/confirm-dialog/confirm-dialog.component';
import { FuseProgressBarService } from '@fuse/components/progress-bar/progress-bar.service';
import { UserDataSource } from 'app/models/user/user.datasource';
import { UserFilter } from 'app/models/user/user.filter.model';
import { UserMasterDataModel } from 'app/models/user/user.master.data.model';
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

  @ViewChild(MatPaginator, {static: true})
  paginator: MatPaginator;

  @ViewChild(MatSort, {static: true})
  sort: MatSort;

  filterValue:string;
  dataSource = new MatTableDataSource([]);
  displayedColumns = ['image', 'firstName','roleText', 'email', 'mobileNo', 'buttons'];

  masterData:UserMasterDataModel;

  dialogRef: any;
  confirmDialogRef: MatDialogRef<FuseConfirmDialogComponent>;

  userFilter:UserFilter;
  
  // Private
  private _unsubscribeAll: Subject<any>;
      
  constructor
  (
    private _fuseProgressBarService: FuseProgressBarService,
      private _userService: UserService,
        public _matDialog: MatDialog) {
      this._unsubscribeAll = new Subject();
     }

  ngOnInit(): void {

    this._userService.onFilterChanged.subscribe((response:UserFilter)=>{
        this.userFilter=response;
        this.loadUsers(response.selectedRoleId,response.selectdStatusId);
    });

    this._userService.onUserUpdated.subscribe(()=>{
        this.loadUsers(this.userFilter.selectedRoleId,this.userFilter.selectdStatusId);
    });

    this._userService.onNewUserAdded.subscribe(response=>{
        this.saveUser(response);
    });

    this._userService.onSearchTextChanged.subscribe(response=>{
        this.filterValue = response.trim(); // Remove whitespace
        this.filterValue = this.filterValue.toLowerCase(); // Datasource defaults to lowercase matches
        this.dataSource.filter = this.filterValue;
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
        
    });

    this._userService.onMasterDataRecieved.subscribe(response=>{
        this.masterData = response;
    })

    this.loadUsers(0,0);
  }

  ngOnDestroy(): void
  {
      // Unsubscribe from all subscriptions
      this._unsubscribeAll.next();
      this._unsubscribeAll.complete();
  }

  loadUsers(roleId:number,status:number)
  {
    this._userService.getAllUsers(roleId,status)
    .subscribe(response=>{
        console.log(response);
        
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
            panelClass: 'user-form-dialog',
            data      : {
              user: user,
              masterData:this.masterData,
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

                        this.saveUser(formData.getRawValue());

                        break;
                    /**
                     * Delete
                     */
                    case 'delete':

                        this.deleteUser(user);

                        break;
                }
            });
    }

    /**
     * Delete Contact
     */
    deleteUser(user:User): void
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

    saveUser(user:User)
    {
        this._fuseProgressBarService.show();
        console.log(user);
        
        this._userService.saveVehicle(user)
            .subscribe(response=>{
                this._fuseProgressBarService.hide();
                this.loadUsers(0,0);
            },error=>{
                this._fuseProgressBarService.hide();
            });
    }

}
