import { Component, Inject, OnInit, ViewEncapsulation } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatColors } from '@fuse/mat-colors';
import { VehicleFitnessReportModel } from 'app/models/vehicle/vehicle-fitness-report.model';

@Component({
  selector: 'app-fitness-report-detail',
  templateUrl: './fitness-report-detail.component.html',
  styleUrls: ['./fitness-report-detail.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class FitnessReportDetailComponent implements OnInit {

  action: string;
  fitnessReport: VehicleFitnessReportModel;
  totalNoOfRecords:number;
  fitnessReportForm: FormGroup;
  dialogTitle: string;
  isReadOnly:boolean;
  presetColors = MatColors.presets;

  constructor(public matDialogRef: MatDialogRef<FitnessReportDetailComponent>,
    @Inject(MAT_DIALOG_DATA) private _data: any) { 

      this.fitnessReport = _data.fitnessReport;
      this.totalNoOfRecords=_data.totalNoOfRecords;
      this.action = _data.action;
      this.isReadOnly = _data.isReadOnly;
  
      if (this.action === 'edit') {
        this.dialogTitle = "Edit Vehcile Fitness Report Record";
      }
      else {
        this.dialogTitle = 'New Vehcile Fitness Report Record';
      }
  
      this.fitnessReportForm = this.createForm();
    }

  ngOnInit(): void {
  }


  createForm() {

    let fitnessReportDate =new Date();
    let validTillDate= new Date();
    validTillDate.setDate(fitnessReportDate.getDate()+365);

    if(this.fitnessReport.id>0)
    {
      fitnessReportDate = new Date(this.fitnessReport.fitnessReportYear,this.fitnessReport.fitnessReportMonth,this.fitnessReport.fitnessReportDay,0,0,0);

      validTillDate = new Date(this.fitnessReport.validTillYear,this.fitnessReport.validTillMonth,this.fitnessReport.validTillDay,0,0,0);
    }


    

    return new FormGroup({
      id: new FormControl({ value: this.fitnessReport.id, disabled: true }),
      registrationNo:new FormControl({ value: this.fitnessReport.registrationNo, disabled: true }), 
      vehicleId: new FormControl(this.fitnessReport.vehicleId, Validators.required),
      fitnessReportDate: new FormControl({ value: fitnessReportDate, disabled: this.isReadOnly }),
      validTillDate: new FormControl({ value: validTillDate, disabled: this.isReadOnly }),
      note: new FormControl({ value: this.fitnessReport.note, disabled: this.isReadOnly }),
      isActive: new FormControl(this.fitnessReport.isActive),
      createdOn: new FormControl({ value: this.fitnessReport.createdOn, disabled: true }),
      updatedOn: new FormControl({ value: this.fitnessReport.updatedOn, disabled: true }),
    });
  }

}
