import { Component, ElementRef, OnDestroy, OnInit, TemplateRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { fuseAnimations } from '@fuse/animations';
import { FuseProgressBarService } from '@fuse/components/progress-bar/progress-bar.service';
import { DailyBeatDataSource } from 'app/models/dialy-beat/daily-beat-datasource';
import { DailyVehicleBeatModel } from 'app/models/dialy-beat/daily-vehicle-beat.model';
import { VehicleBeatMasterDataModel } from 'app/models/dialy-beat/vehicle-beat-master-data.model';
import { DailyBeatService } from 'app/services/daily-beats/daily-beat.service';
import { fromEvent, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, takeUntil } from 'rxjs/operators';
import { DailyBeatEditModelComponent } from '../daily-beat-edit-model/daily-beat-edit-model.component';
import { DailyBeatOrderDetailComponent } from '../daily-beat-order-detail/daily-beat-order-detail.component';

@Component({
  selector: 'daily-beat-list',
  templateUrl: './daily-beat-list.component.html',
  styleUrls: ['./daily-beat-list.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations   : fuseAnimations
})
export class DailyBeatListComponent implements OnInit, OnDestroy {

    masterData:VehicleBeatMasterDataModel;
    // Private
    private _unsubscribeAll: Subject<any>;

    @ViewChild(MatPaginator,{static:true}) 
    paginator: MatPaginator;
  
    @ViewChild(MatSort, {static:true}) 
    sort: MatSort;
  
    dialogRef: any;

    dataSource: DailyBeatDataSource;

    pageSizes:number[] =[25,50,75,100,200,500];

    displayedColumns = ["buttons",'routeName', 'vehicleNumber','driverName', 'date', 'statusInText'];

  constructor(private _dailyBeatService:DailyBeatService,private _fuseProgressBarService: FuseProgressBarService,private _matDialog: MatDialog) {
    this._unsubscribeAll = new Subject();
   }

  ngOnInit(): void {

    this.dataSource = new DailyBeatDataSource(this._dailyBeatService, this.paginator, this.sort);

    this._dailyBeatService.onSearchTextChanged.subscribe(searchValue=>{

      if ( !this.dataSource )
      {
          return;
      }

      this.dataSource.filter = searchValue;

    });

    this._dailyBeatService.onMasterDataRecieved.subscribe(response=>{
      this.masterData = response;
    });

    this._dailyBeatService.onDailyBeatSaved.subscribe(response=>{

      this.saveDailyBeat(response);
    });
    
  }

  onChangePage(pe:PageEvent) {
    this.dataSource.pageSize = pe.pageSize;
  }

  editDailyBeat(item:DailyVehicleBeatModel)
  {
    console.log(item);
    
    this.dialogRef = this._matDialog.open(DailyBeatEditModelComponent, {
      panelClass: 'daily-beat-edit-form-dialog',
      data      : {
          masterData:this.masterData,
          data:item,
          action: 'edit'
      }
  });

  this.dialogRef.afterClosed()
      .subscribe((response: FormGroup) => {
          if ( !response )
          {
              return;
          }

          console.log(response);
          
          this.saveDailyBeat(response[1].getRawValue())
      });
  }

  deleteDailyBeat(item:DailyVehicleBeatModel)
  {

  }

  view(item:DailyVehicleBeatModel)
  {

  }

  manageSalesOrder(item:DailyVehicleBeatModel)
  {
    this.dialogRef = this._matDialog.open(DailyBeatOrderDetailComponent, {
      panelClass: 'daily-beat-order-detail',
      data      : {
        model:item,
      }
  });
  }

  ngOnDestroy(): void
  {
      // Unsubscribe from all subscriptions
      this._unsubscribeAll.next();
      this._unsubscribeAll.complete();
  }

  saveDailyBeat(item:DailyVehicleBeatModel)
  {
      this._fuseProgressBarService.show();
      
      item.dateYear = item.date.getFullYear();
      item.dateMonth = item.date.getMonth()+1;
      item.dateDay = item.date.getDate();

      this._dailyBeatService.saveDailyVehicleBeatRecord(item)
        .subscribe(response=>
        {
          this.dataSource._saveRecord.next(true);
          this._fuseProgressBarService.hide();
        },error=>{
          this._fuseProgressBarService.hide();
        });
  }
}
