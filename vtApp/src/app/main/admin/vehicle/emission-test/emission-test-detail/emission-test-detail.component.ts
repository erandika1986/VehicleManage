import { Component, Inject, OnInit, ViewEncapsulation } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatColors } from '@fuse/mat-colors';
import { VehicleEmissionTestModel } from 'app/models/vehicle/vehicle-emission-test.model';

@Component({
  selector: 'app-emission-test-detail',
  templateUrl: './emission-test-detail.component.html',
  styleUrls: ['./emission-test-detail.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class EmissionTestDetailComponent implements OnInit {

  action: string;
  emissionTest: VehicleEmissionTestModel;
  totalNoOfRecords:number;
  emissionTestForm: FormGroup;
  dialogTitle: string;
  isReadOnly:boolean;
  presetColors = MatColors.presets;

  constructor(public matDialogRef: MatDialogRef<EmissionTestDetailComponent>,
    @Inject(MAT_DIALOG_DATA) private _data: any) {
      this.emissionTest = _data.emissionTest;
      this.totalNoOfRecords=_data.totalNoOfRecords;
      this.action = _data.action;
      this.isReadOnly = _data.isReadOnly;
  
      if (this.action === 'edit') {
        this.dialogTitle = "Edit Vehcile Emission Test Record";
      }
      else {
        this.dialogTitle = 'New Vehcile Emission Test Record';
      }
  
      this.emissionTestForm = this.createForm();
     }

  ngOnInit(): void {

  }


  createForm() {

    let emssionTestDate =new Date();
    let validTillDate= new Date();
    validTillDate.setDate(emssionTestDate.getDate()+365);

    if(this.emissionTest.id>0)
    {
      emssionTestDate = new Date(this.emissionTest.emissionTestYear,this.emissionTest.emissionTestMonth,this.emissionTest.emissionTestDay,0,0,0);

      validTillDate = new Date(this.emissionTest.validTillYear,this.emissionTest.validTillMonth,this.emissionTest.validTillDay,0,0,0);
    }


    

    return new FormGroup({
      id: new FormControl({ value: this.emissionTest.id, disabled: true }),
      registrationNo:new FormControl({ value: this.emissionTest.registrationNo, disabled: true }), 
      vehicleId: new FormControl(this.emissionTest.vehicleId, Validators.required),
      emissiontTestDate: new FormControl({ value: emssionTestDate, disabled: this.isReadOnly }),
      validTillDate: new FormControl({ value: validTillDate, disabled: this.isReadOnly }),
      isActive: new FormControl(this.emissionTest.isActive),
      createdOn: new FormControl({ value: this.emissionTest.createdOn, disabled: true }),
      updatedOn: new FormControl({ value: this.emissionTest.updatedOn, disabled: true }),
    });
  }

}
