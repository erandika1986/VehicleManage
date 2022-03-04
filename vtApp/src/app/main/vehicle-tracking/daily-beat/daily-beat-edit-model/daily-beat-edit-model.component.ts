import { Component, Inject, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
import { FuseProgressBarService } from '@fuse/components/progress-bar/progress-bar.service';
import { DailyVehicleBeatModel } from 'app/models/dialy-beat/daily-vehicle-beat.model';
import { VehicleBeatMasterDataModel } from 'app/models/dialy-beat/vehicle-beat-master-data.model';
import { DailyBeatService } from 'app/services/daily-beats/daily-beat.service';

@Component({
  selector: 'daily-beat-edit-model',
  templateUrl: './daily-beat-edit-model.component.html',
  styleUrls: ['./daily-beat-edit-model.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class DailyBeatEditModelComponent implements OnInit {

  horizontalPosition: MatSnackBarHorizontalPosition = 'right';
  verticalPosition: MatSnackBarVerticalPosition = 'top';
  
  dailyBeatId:number;
  action: string;
  masterData:VehicleBeatMasterDataModel;
  dailyBeat: DailyVehicleBeatModel;
  dailyBeatForm: FormGroup;
  dialogTitle: string;



  constructor(public matDialogRef: MatDialogRef<DailyBeatEditModelComponent>,
    private _fuseProgressBarService: FuseProgressBarService,
    private _dailyBeatService:DailyBeatService,
    private _snackBar: MatSnackBar,
    @Inject(MAT_DIALOG_DATA) private _data: any,
    private _formBuilder: FormBuilder) { 
      this.action = _data.action;
      this.masterData = _data.masterData;

      if ( this.action == 'edit' )
      {
          this.dialogTitle = 'Edit Daily Beat';
          this.dailyBeat = _data.data;
          this.dailyBeatId = this.dailyBeat.id;
          this.dailyBeatForm = this.createExistingDailyBeatForm(this.dailyBeat);
      }
      else
      {
        console.log(this.masterData);
        
          this.dialogTitle = 'New Daily Beat';
          this.dailyBeatId=0;
          this.dailyBeat = new DailyVehicleBeatModel();
          this.dailyBeatForm = this.createNewDailyBeatForm();
      }
    }

    ngOnInit(): void {

/*       if(this.dailyBeatId!=0)
      {
        this._dailyBeatService.getVehicleBeatRecordById(this.dailyBeatId)
        .subscribe(response=>{

          this.dailyBeatForm = this.createExistingDailyBeatForm(response);

        },error=>{

        });
      }
      else
      {
        this.dailyBeatForm = this.createNewDailyBeatForm();
      } */
    }
  
    createExistingDailyBeatForm(model:DailyVehicleBeatModel):FormGroup
    {
      console.log(model);

        return this._formBuilder.group({
          id: [model.id],
           vehicleId: [model.vehicleId,Validators.required],
          routeId: [model.routeId,Validators.required],
          salesRepId: [model.salesRepId, Validators.required],
          driverId: [model.driverId],
          date: [new Date(model.date),Validators.required],
          startingMilage: [model.startingMilage,Validators.required],
          endMilage:[model.endMilage,Validators.required],
          status:[model.status,Validators.required],
          remarks:[model.remarks],
          isActive: [true]
        });

    }
  
    createNewDailyBeatForm(): FormGroup
    {
      return this._formBuilder.group({
        id: [0],
        vehicleId: [null,Validators.required],
        routeId: [null,Validators.required],
        salesRepId: [null, Validators.required],
        driverId: [null],
        date: [new Date(),Validators.required],
        startingMilage: [0,Validators.required],
        endMilage:[0,Validators.required],
        status:[null,Validators.required],
        remarks:[''],
        isActive: [true],
      });
    }
  

  

  


}
