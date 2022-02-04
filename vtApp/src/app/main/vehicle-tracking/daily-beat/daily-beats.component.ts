import { Component, ElementRef, OnDestroy, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { FuseProgressBarService } from '@fuse/components/progress-bar/progress-bar.service';
import { FuseSidebarService } from '@fuse/components/sidebar/sidebar.service';
import { VehicleBeatMasterDataModel } from 'app/models/dialy-beat/vehicle-beat-master-data.model';
import { DailyBeatService } from 'app/services/daily-beats/daily-beat.service';
import { fromEvent, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, takeUntil } from 'rxjs/operators';
import { DailyBeatEditModelComponent } from './daily-beat-edit-model/daily-beat-edit-model.component';

@Component({
  selector: 'app-daily-beats',
  templateUrl: './daily-beats.component.html',
  styleUrls: ['./daily-beats.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations   : fuseAnimations
})
export class DailyBeatsComponent  implements OnInit, OnDestroy {

  @ViewChild('filter', {static: true})
  filter: ElementRef;

  dialogRef: any;
  hasSelectedContacts: boolean;

  private _unsubscribeAll: Subject<any>;
  masterData:VehicleBeatMasterDataModel;

  constructor(
    private _dailyBeatService:DailyBeatService,
    private _fuseSidebarService: FuseSidebarService,
    private _fuseProgressBarService: FuseProgressBarService,
    public _router: Router,
    private _matDialog: MatDialog) {
      // Set the private defaults
      this._unsubscribeAll = new Subject();
     }

  ngOnInit(): void {

    fromEvent(this.filter.nativeElement, 'keyup')
    .pipe(
        takeUntil(this._unsubscribeAll),
        debounceTime(150),
        distinctUntilChanged()
    )
    .subscribe(() => {

      this._dailyBeatService.onSearchTextChanged.next(this.filter.nativeElement.value)
    });

    this._dailyBeatService.onMasterDataRecieved.subscribe(response=>{
      this.masterData = response;
    })
  }

  ngOnDestroy(): void
  {
      // Unsubscribe from all subscriptions
      this._unsubscribeAll.next();
      this._unsubscribeAll.complete();
  }


  newDailyBeat(): void
  {

  }

  toggleSidebar(name): void
  {
      this._fuseSidebarService.getSidebar(name).toggleOpen();
  }
  

  saveUser()
  {

  }

  addNew()
  {
    console.log(this.masterData);
    
    this.dialogRef = this._matDialog.open(DailyBeatEditModelComponent, {
      panelClass: 'daily-beat-edit-form-dialog',
      data      : {
          masterData:this.masterData,
          action: 'new'
      }
  });

  this.dialogRef.afterClosed()
      .subscribe((response: FormGroup) => {
          if ( !response )
          {
              return;
          }
          this._dailyBeatService.onDailyBeatSaved.next(response.getRawValue());
      });
  }
}
