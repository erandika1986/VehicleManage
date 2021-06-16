import { Component, Inject, OnInit, ViewEncapsulation } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatColors } from '@fuse/mat-colors';
import { VehicleAirCleanerModel } from 'app/models/vehicle/vehicle-air-cleaner.model';

@Component({
  selector: 'app-air-cleaner-detail',
  templateUrl: './air-cleaner-detail.component.html',
  styleUrls: ['./air-cleaner-detail.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class AirCleanerDetailComponent implements OnInit {

  action: string;
  airCleaner: VehicleAirCleanerModel;
  totalNoOfRecords:number;
  airCleanerForm: FormGroup;
  dialogTitle: string;
  isReadOnly:boolean;
  presetColors = MatColors.presets;

  constructor(public matDialogRef: MatDialogRef<AirCleanerDetailComponent>,
    @Inject(MAT_DIALOG_DATA) private _data: any) {

      this.airCleaner = _data.airCleaner;
      this.totalNoOfRecords=_data.totalNoOfRecords;
      this.action = _data.action;
      this.isReadOnly = _data.isReadOnly;
  
      if (this.action === 'edit') {
        this.dialogTitle = "Edit Air Cleaner Record";
      }
      else {
        this.dialogTitle = 'New Air Cleaner Record';
      }
  
      this.airCleanerForm = this.createForm();
     }

  ngOnInit(): void {
  }

  createForm() {

    return new FormGroup({
      id: new FormControl({ value: this.airCleaner.id, disabled: true }),
      registrationNo:new FormControl({ value: this.airCleaner.registrationNo, disabled: true }), 
      vehicleId: new FormControl(this.airCleaner.vehicleId, Validators.required),
      airCleanerReplaceMilage: new FormControl({ value: this.airCleaner.airCleanerReplaceMilage, disabled: this.isReadOnly }),
      nextAirCleanerReplaceMilage: new FormControl({ value: this.airCleaner.nextAirCleanerReplaceMilage, disabled: this.isReadOnly }),
      isActive: new FormControl(this.airCleaner.isActive),
      createdOn: new FormControl({ value: this.airCleaner.createdOn, disabled: true }),
      updatedOn: new FormControl({ value: this.airCleaner.updatedOn, disabled: true }),
    });
  }

}
