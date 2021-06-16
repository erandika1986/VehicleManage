import { Component, Inject, OnInit, ViewEncapsulation } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatColors } from '@fuse/mat-colors';
import { VehicleGearBoxOilMilageModel } from 'app/models/vehicle/vehicle-gear-box-oil-milage.model';

@Component({
  selector: 'app-gear-box-detail',
  templateUrl: './gear-box-detail.component.html',
  styleUrls: ['./gear-box-detail.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class GearBoxDetailComponent implements OnInit {

  action: string;
  gearBoxOil: VehicleGearBoxOilMilageModel;
  totalNoOfRecords:number;
  gearBoxOilForm: FormGroup;
  dialogTitle: string;
  isReadOnly:boolean;
  presetColors = MatColors.presets;

  constructor(public matDialogRef: MatDialogRef<GearBoxDetailComponent>,
    @Inject(MAT_DIALOG_DATA) private _data: any) {
      this.gearBoxOil = _data.gearBoxOil;
      this.totalNoOfRecords=_data.totalNoOfRecords;
      this.action = _data.action;
      this.isReadOnly = _data.isReadOnly;
  
      if (this.action === 'edit') {
        this.dialogTitle = "Edit  Gear Box Oil Record";
      }
      else {
        this.dialogTitle = 'New  Gear Box Oil Record';
      }
  
      this.gearBoxOilForm = this.createForm();
     }

  ngOnInit(): void {
  }

  createForm() {

    return new FormGroup({
      id: new FormControl({ value: this.gearBoxOil.id, disabled: true }),
      registrationNo:new FormControl({ value: this.gearBoxOil.registrationNo, disabled: true }), 
      vehicleId: new FormControl(this.gearBoxOil.vehicleId, Validators.required),
      gearBoxOilChangeMilage: new FormControl({ value: this.gearBoxOil.gearBoxOilChangeMilage, disabled: this.isReadOnly },[Validators.required]),
      nextGearBoxOilChangeMilage: new FormControl({ value: this.gearBoxOil.nextGearBoxOilChangeMilage, disabled: this.isReadOnly },[Validators.required]),
      isActive: new FormControl(this.gearBoxOil.isActive),
      createdOn: new FormControl({ value: this.gearBoxOil.createdOn, disabled: true }),
      updatedOn: new FormControl({ value: this.gearBoxOil.updatedOn, disabled: true }),
    });
  }

}
