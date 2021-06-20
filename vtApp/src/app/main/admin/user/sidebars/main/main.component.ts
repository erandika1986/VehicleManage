import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { DropDownModel } from 'app/models/common/drop-down.modal';
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
  status:DropDownModel[]=[];
  roles:DropDownModel[]=[];
  filterForm:FormGroup;


  // Private
  private _unsubscribeAll: Subject<any>;
  
  constructor(private _usersService: UserService) { 
    this._unsubscribeAll = new Subject();

    let allStaus:DropDownModel = new DropDownModel();
    allStaus.id=0;
    allStaus.name="All";

    let activeStaus:DropDownModel = new DropDownModel();
    activeStaus.id=1;
    activeStaus.name="Active";

    let inactiveStaus:DropDownModel = new DropDownModel();
    inactiveStaus.id=2;
    inactiveStaus.name="Inactive";

    this.status.push(allStaus);
    this.status.push(activeStaus);
    this.status.push(inactiveStaus);

    this.roles.push(allStaus);

    this.filterForm = this.createFilterForm();
  }

  ngOnInit(): void {

    this.filterBy = this._usersService.filterBy || 'all';
    this.getAllMasterData();
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
    
    getAllMasterData()
    {
      this._usersService.getUserMasterData()
        .subscribe(response=>{
          response.roles.forEach(element => {
            this.roles.push(element);
          });
        },error=>{

        });
    }

    createFilterForm(): FormGroup {
      return new FormGroup({
     
        selectedRoleId: new FormControl(0),
        selectdStatusId: new FormControl(0),
      });
    }

    dropdownFilterChanged()
    {
      this._usersService.onFilterChanged.next(this.filterForm.getRawValue());
    }

}
