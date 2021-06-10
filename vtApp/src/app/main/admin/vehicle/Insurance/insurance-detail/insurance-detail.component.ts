import { Component, Inject, OnInit, ViewEncapsulation } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatColors } from '@fuse/mat-colors';
import { VehicleInsuranceModel, VehicleInsuranceReactiveForm } from 'app/models/vehicle/vehicle-insurance.model';

@Component({
  selector: 'app-insurance-detail',
  templateUrl: './insurance-detail.component.html',
  styleUrls: ['./insurance-detail.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class InsuranceDetailComponent implements OnInit {

  action: string;
  insurance: VehicleInsuranceModel;
  totalNoOfRecords:number;
  insuranceForm: FormGroup;
  dialogTitle: string;
  isReadOnly:boolean;
  presetColors = MatColors.presets;

  constructor(public matDialogRef: MatDialogRef<InsuranceDetailComponent>,
    @Inject(MAT_DIALOG_DATA) private _data: any) {
      this.insurance = _data.insurance;
      this.totalNoOfRecords=_data.totalNoOfRecords;
      this.action = _data.action;
      this.isReadOnly = _data.isReadOnly;
  
      if (this.action === 'edit') {
        this.dialogTitle = "Edit Vehcile Insurance Record";
      }
      else {
        this.dialogTitle = 'New Vehcile Insurance Record';
      }
  
      this.insuranceForm = this.createForm();
 
     }

  ngOnInit(): void {
  }

  createForm() {

    let insuranceDate =new Date();
    let validTillDate= new Date();
    validTillDate.setDate(insuranceDate.getDate()+365);

    if(this.insurance.id>0)
    {
      insuranceDate = new Date(this.insurance.insuranceYear,this.insurance.insuranceMonth,this.insurance.insuranceDay,0,0,0);

      validTillDate = new Date(this.insurance.validTillYear,this.insurance.validTillMonth,this.insurance.validTillDay,0,0,0);
    }


    

    return new FormGroup({
      id: new FormControl({ value: this.insurance.id, disabled: true }),
      registrationNo:new FormControl({ value: this.insurance.registrationNo, disabled: true }), 
      vehicleId: new FormControl(this.insurance.vehicleId, Validators.required),
      insuranceDate: new FormControl({ value: insuranceDate, disabled: this.isReadOnly }),
      validTillDate: new FormControl({ value: validTillDate, disabled: this.isReadOnly }),
      isActive: new FormControl(this.insurance.isActive),
      createdOn: new FormControl({ value: this.insurance.createdOn, disabled: true }),
      updatedOn: new FormControl({ value: this.insurance.updatedOn, disabled: true }),
    });
  }

}
