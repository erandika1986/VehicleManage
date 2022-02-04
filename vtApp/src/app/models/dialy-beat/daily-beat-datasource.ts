import { DataSource } from "@angular/cdk/collections";
import { MatPaginator } from "@angular/material/paginator";
import { MatSort } from "@angular/material/sort";
import { DailyBeatService } from "app/services/daily-beats/daily-beat.service";
import { BehaviorSubject, merge, Observable, of as observableOf} from "rxjs";
import { catchError, map, startWith, switchMap } from "rxjs/operators";
import { DailyVehicleBeatModel } from "./daily-vehicle-beat.model";
import { VehicleBeatFilterModel } from "./vehicle-beat-filter.model";

export class DailyBeatDataSource extends DataSource<any>
{
    public _filterChange = new BehaviorSubject(true);
    public _saveRecord = new BehaviorSubject(true);
    private _searchTextChange = new BehaviorSubject('');
    private _pageSize = new BehaviorSubject(25);
    private _status = new BehaviorSubject(1);
    private _totalRecordNumber= new BehaviorSubject(0);
    vehicleBeatFilter:VehicleBeatFilterModel;

    constructor(private _dailyBeatService: DailyBeatService,
        private _matPaginator: MatPaginator,
        private _matSort: MatSort)
    {
        super();
        let date = new Date();
        this.vehicleBeatFilter = new VehicleBeatFilterModel();
        this.vehicleBeatFilter.date = date;
        this.vehicleBeatFilter.dateDay = date.getDate();
        this.vehicleBeatFilter.dateMonth = date.getMonth()+1;
        this.vehicleBeatFilter.dateYear = date.getFullYear();
        this.vehicleBeatFilter.selectedDriverId = 0;
        this.vehicleBeatFilter.selectedRouteId = 0;
        this.vehicleBeatFilter.selectedSalesRepId = 0;
        this.vehicleBeatFilter.selectedStatus = 0;
        this.vehicleBeatFilter.selectedVehicleId = 0;

        this._dailyBeatService.onFilterChanged.subscribe(response=>{

            this.vehicleBeatFilter.date = response.date;
            this.vehicleBeatFilter.dateDay = response.date.getDate();
            this.vehicleBeatFilter.dateMonth = response.date.getMonth()+1;
            this.vehicleBeatFilter.dateYear = response.date.getFullYear();
            this.vehicleBeatFilter.selectedDriverId = response.selectedDriverId;
            this.vehicleBeatFilter.selectedRouteId = response.selectedRouteId;
            this.vehicleBeatFilter.selectedSalesRepId = response.selectedSalesRepId;
            this.vehicleBeatFilter.selectedStatus = response.selectedStatus;
            this.vehicleBeatFilter.selectedVehicleId = response.selectedVehicleId;
            this._matPaginator.pageIndex = 0;
            this._filterChange.next(true);
        });

        this._matSort.sortChange.subscribe(() => this._matPaginator.pageIndex = 0);
    }

    connect(): Observable<DailyVehicleBeatModel[]>
    {
        console.log('Connecting....');

        const displayDataChanges = [
            this._matPaginator.page,
            this._searchTextChange,
            this._matSort.sortChange,
            this._filterChange,
            this._saveRecord
        ];

        return merge(...displayDataChanges)
        .pipe(
          startWith({}),
          switchMap(() => {
              this.vehicleBeatFilter.currentPage = this._matPaginator.pageIndex+1;
              this.vehicleBeatFilter.pageSize = this.pageSize;

            return this._dailyBeatService!
            .getAllVehicleBeatRecord(this.vehicleBeatFilter)
            .pipe(catchError(() => observableOf(null)));
          }),
          map(data => {
  
            if (data === null) {
              return [];
            }
            this.totalRecord =data.totalRecordCount;
            return data.data;
          }),
        )
    }

    disconnect(): void
    {

    }


    get filter(): string
    {
        return this._searchTextChange.value;
    }

    set filter(filter: string)
    {
        this._searchTextChange.next(filter);
    }

    get pageSize():number
    {
        return this._pageSize.value;
    }

    set pageSize(value:number)
    {
        this._pageSize.next(value);
    }

    get status():number
    {
        return this._status.value;
    }

    set status(value:number)
    {
        this._status.next(value);
    }

    get totalRecord():number
    {
        return this._totalRecordNumber.value;
    }

    set totalRecord(value:number)
    {
        this._totalRecordNumber.next(value);
    }
}