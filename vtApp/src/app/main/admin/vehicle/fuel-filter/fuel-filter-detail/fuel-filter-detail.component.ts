import { Component, Inject, OnInit, ViewEncapsulation } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatColors } from '@fuse/mat-colors';
import { VehicleFuelFilterMilageModel } from 'app/models/vehicle/vehicle-fuel-filter-milage.model';

@Component({
  selector: 'app-fuel-filter-detail',
  templateUrl: './fuel-filter-detail.component.html',
  styleUrls: ['./fuel-filter-detail.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class FuelFilterDetailComponent implements OnInit {

  action: string;
  fuelFilter: VehicleFuelFilterMilageModel;
  totalNoOfRecords:number;
  fuelFilterForm: FormGroup;
  dialogTitle: string;
  isReadOnly:boolean;
  presetColors = MatColors.presets;
  constructor(public matDialogRef: MatDialogRef<FuelFilterDetailComponent>,
    @Inject(MAT_DIALOG_DATA) private _data: any) {
      this.fuelFilter = _data.fuelFilter;
      this.totalNoOfRecords=_data.totalNoOfRecords;
      this.action = _data.action;
      this.isReadOnly = _data.isReadOnly;
  
      if (this.action === 'edit') {
        this.dialogTitle = "Edit Fuel Filter Record";
      }
      else {
        this.dialogTitle = 'New Fuel Filter Record';
      }
  
      this.fuelFilterForm = this.createForm();
     }

  ngOnInit(): void {

  }


  createForm() {

    return new FormGroup({
      id: new FormControl({ value: this.fuelFilter.id, disabled: true }),
      registrationNo:new FormControl({ value: this.fuelFilter.registrationNo, disabled: true }), 
      vehicleId: new FormControl(this.fuelFilter.vehicleId, Validators.required),
      fuelFilterChangeMilage: new FormControl({ value: this.fuelFilter.fuelFilterChangeMilage, disabled: this.isReadOnly },[Validators.required]),
      nextFuelFilterChangeMilage: new FormControl({ value: this.fuelFilter.nextFuelFilterChangeMilage, disabled: this.isReadOnly },[Validators.required]),
      isActive: new FormControl(this.fuelFilter.isActive),
      createdOn: new FormControl({ value: this.fuelFilter.createdOn, disabled: true }),
      updatedOn: new FormControl({ value: this.fuelFilter.updatedOn, disabled: true }),
    });
  }

}
