import { Component, Inject, OnInit, ViewEncapsulation } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatColors } from '@fuse/mat-colors';
import { VehicleEngineOilMilageModel } from 'app/models/vehicle/vehicle-engine-oil-milage.model';

@Component({
  selector: 'app-engine-oil-detail',
  templateUrl: './engine-oil-detail.component.html',
  styleUrls: ['./engine-oil-detail.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class EngineOilDetailComponent implements OnInit {

  action: string;
  engineOil: VehicleEngineOilMilageModel;
  totalNoOfRecords:number;
  engineOilForm: FormGroup;
  dialogTitle: string;
  isReadOnly:boolean;
  presetColors = MatColors.presets;

  constructor(public matDialogRef: MatDialogRef<EngineOilDetailComponent>,
    @Inject(MAT_DIALOG_DATA) private _data: any) { 
      this.engineOil = _data.engineOil;
      this.totalNoOfRecords=_data.totalNoOfRecords;
      this.action = _data.action;
      this.isReadOnly = _data.isReadOnly;
  
      if (this.action === 'edit') {
        this.dialogTitle = "Edit Engine Oil Record";
      }
      else {
        this.dialogTitle = 'New Engine Oil Record';
      }
  
      this.engineOilForm = this.createForm();
    }

  ngOnInit(): void {
  }

  createForm() {

    return new FormGroup({
      id: new FormControl({ value: this.engineOil.id, disabled: true }),
      registrationNo:new FormControl({ value: this.engineOil.registrationNo, disabled: true }), 
      vehicleId: new FormControl(this.engineOil.vehicleId, Validators.required),
      oilChangeMilage: new FormControl({ value: this.engineOil.oilChangeMilage, disabled: this.isReadOnly },[Validators.required]),
      nextOilChangeMilage: new FormControl({ value: this.engineOil.nextOilChangeMilage, disabled: this.isReadOnly },[Validators.required]),
      note: new FormControl({ value: this.engineOil.note, disabled: this.isReadOnly }),
      isActive: new FormControl(this.engineOil.isActive),
      createdOn: new FormControl({ value: this.engineOil.createdOn, disabled: true }),
      updatedOn: new FormControl({ value: this.engineOil.updatedOn, disabled: true }),
    });
  }

}
