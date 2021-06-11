import { Component, Inject, OnInit, ViewEncapsulation } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatColors } from '@fuse/mat-colors';
import { VehicleRevenueLicenceModel } from 'app/models/vehicle/vehicle-revenue-licence.model';

@Component({
  selector: 'app-revenue-licence-detail',
  templateUrl: './revenue-licence-detail.component.html',
  styleUrls: ['./revenue-licence-detail.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class RevenueLicenceDetailComponent implements OnInit {

  action: string;
  licence: VehicleRevenueLicenceModel;
  totalNoOfRecords:number;
  revenueLicenceForm: FormGroup;
  dialogTitle: string;
  isReadOnly:boolean;
  presetColors = MatColors.presets;

  constructor(public matDialogRef: MatDialogRef<RevenueLicenceDetailComponent>,
    @Inject(MAT_DIALOG_DATA) private _data: any) { 

      this.licence = _data.licence;
      this.totalNoOfRecords=_data.totalNoOfRecords;
      this.action = _data.action;
      this.isReadOnly = _data.isReadOnly;
  
      if (this.action === 'edit') {
        this.dialogTitle = "Edit Vehcile Revenue Licence Record";
      }
      else {
        this.dialogTitle = 'New Vehcile Revenue Licence Record';
      }
  
      this.revenueLicenceForm = this.createForm();
    }

  ngOnInit(): void {
  }

  createForm() {

    let revenueDate =new Date();
    let validTillDate= new Date();
    validTillDate.setDate(revenueDate.getDate()+365);

    if(this.licence.id>0)
    {
      revenueDate = new Date(this.licence.revenueLicenceYear,this.licence.revenueLicenceMonth,this.licence.revenueLicenceDay,0,0,0);

      validTillDate = new Date(this.licence.validTillYear,this.licence.validTillMonth,this.licence.validTillDay,0,0,0);
    }


    

    return new FormGroup({
      id: new FormControl({ value: this.licence.id, disabled: true }),
      registrationNo:new FormControl({ value: this.licence.registrationNo, disabled: true }), 
      vehicleId: new FormControl(this.licence.vehicleId, Validators.required),
      revenueLicenceDate: new FormControl({ value: revenueDate, disabled: this.isReadOnly }),
      validTillDate: new FormControl({ value: validTillDate, disabled: this.isReadOnly }),
      isActive: new FormControl(this.licence.isActive),
      createdOn: new FormControl({ value: this.licence.createdOn, disabled: true }),
      updatedOn: new FormControl({ value: this.licence.updatedOn, disabled: true }),
    });
  }

}
