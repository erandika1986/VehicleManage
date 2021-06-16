import { Component, Inject, OnInit, ViewEncapsulation } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatColors } from '@fuse/mat-colors';
import { VehicleDifferentialOilChangeMilageModel } from 'app/models/vehicle/vehicle-differential-oil-change-milage.model';

@Component({
  selector: 'differential-oil-detail',
  templateUrl: './differential-oil-detail.component.html',
  styleUrls: ['./differential-oil-detail.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class DifferentialOilDetailComponent implements OnInit {

  action: string;
  differentialOil: VehicleDifferentialOilChangeMilageModel;
  totalNoOfRecords:number;
  differentialOilForm: FormGroup;
  dialogTitle: string;
  isReadOnly:boolean;
  presetColors = MatColors.presets;

  constructor(public matDialogRef: MatDialogRef<DifferentialOilDetailComponent>,
    @Inject(MAT_DIALOG_DATA) private _data: any) { 
      this.differentialOil = _data.differentialOil;
      this.totalNoOfRecords=_data.totalNoOfRecords;
      this.action = _data.action;
      this.isReadOnly = _data.isReadOnly;
  
      if (this.action === 'edit') {
        this.dialogTitle = "Edit  Differential Oil Record";
      }
      else {
        this.dialogTitle = 'New  Differential Oi Record';
      }
  
      this.differentialOilForm = this.createForm();
    }

  ngOnInit(): void {
  }

  createForm() {

    return new FormGroup({
      id: new FormControl({ value: this.differentialOil.id, disabled: true }),
      registrationNo:new FormControl({ value: this.differentialOil.registrationNo, disabled: true }), 
      vehicleId: new FormControl(this.differentialOil.vehicleId, Validators.required),
      differentialOilChangeMilage: new FormControl({ value: this.differentialOil.differentialOilChangeMilage, disabled: this.isReadOnly },[Validators.required]),
      nextDifferentialOilChangeMilage: new FormControl({ value: this.differentialOil.nextDifferentialOilChangeMilage, disabled: this.isReadOnly },[Validators.required]),
      note: new FormControl({ value: this.differentialOil.note, disabled: this.isReadOnly }),
      isActive: new FormControl(this.differentialOil.isActive),
      createdOn: new FormControl({ value: this.differentialOil.createdOn, disabled: true }),
      updatedOn: new FormControl({ value: this.differentialOil.updatedOn, disabled: true }),
    });
  }

}
