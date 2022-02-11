import { Component, OnInit } from '@angular/core';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';
import { DropDownModel } from './../../../../../models/common/drop-down.modal';
import { FormGroup } from '@angular/forms';
import { FormControl } from '@angular/forms';
import { Subject } from 'rxjs';
import { DailyBeatService } from './../../../../../services/daily-beats/daily-beat.service';


@Component({
  selector: 'expenses-main-sidebar',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit {

  user: any;
  filterBy: string;
  status:DropDownModel[]=[];
  routes:DropDownModel[]=[];
  vehicles:DropDownModel[]=[];
  drivers:DropDownModel[]=[];
  salesReps:DropDownModel[]=[];
  filterForm:FormGroup;

  private _unsubscribeAll: Subject<any>;

  constructor(private _dailyBeatService:DailyBeatService) {
    this._unsubscribeAll = new Subject();
   }

  ngOnInit(): void {
    this.getAllMasterData();
    this.filterForm = this.createFilterForm();
  }

  ngOnDestroy(): void
  {
      this._unsubscribeAll.next();
      this._unsubscribeAll.complete();
  }

  createFilterForm(): FormGroup {
    return new FormGroup({
   
      selectExpenseCategory: new FormControl(0),
      fromDate: new FormControl(new Date()),
      toDate: new FormControl(new Date())
    });
  }

  getAllMasterData()
  {
    this._dailyBeatService.getMasterData()
      .subscribe(response=>{

        


        let firstItem = new DropDownModel();
        firstItem.id=0;
        firstItem.name="--All--";



        this.status = response.status;
        this.status.unshift(firstItem);

        this.salesReps = response.salesReps;
        this.salesReps.unshift(firstItem);

        this.drivers = response.drivers;
        this.drivers.unshift(firstItem);

        this.routes = response.routes;
        this.routes.unshift(firstItem);

        this.vehicles = response.vehicles;
        this.vehicles.unshift(firstItem);


        this._dailyBeatService.onMasterDataRecieved.next(response);

      },error=>{

      });
  }

  dropdownFilterChanged()
  {
    this._dailyBeatService.onFilterChanged.next(this.filterForm.getRawValue());
  }

  addEvent(type: string, event: MatDatepickerInputEvent<Date>) {
    this._dailyBeatService.onFilterChanged.next(this.filterForm.getRawValue());
  }

}
