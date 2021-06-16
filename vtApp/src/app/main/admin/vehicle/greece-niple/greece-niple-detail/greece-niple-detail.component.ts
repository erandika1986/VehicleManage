import { Component, Inject, OnInit, ViewEncapsulation } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatColors } from '@fuse/mat-colors';
import { VehicleGreeceNipleModel } from 'app/models/vehicle/vehicle-greece-niple';

@Component({
  selector: 'app-greece-niple-detail',
  templateUrl: './greece-niple-detail.component.html',
  styleUrls: ['./greece-niple-detail.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class GreeceNipleDetailComponent implements OnInit {

  action: string;
  greeceNiple: VehicleGreeceNipleModel;
  totalNoOfRecords:number;
  greeceNipleForm: FormGroup;
  dialogTitle: string;
  isReadOnly:boolean;
  presetColors = MatColors.presets;
  
  constructor(public matDialogRef: MatDialogRef<GreeceNipleDetailComponent>,
    @Inject(MAT_DIALOG_DATA) private _data: any) { 
      this.greeceNiple = _data.greeceNiple;
      this.totalNoOfRecords=_data.totalNoOfRecords;
      this.action = _data.action;
      this.isReadOnly = _data.isReadOnly;
  
      if (this.action === 'edit') {
        this.dialogTitle = "Edit Vehcile Greece Niple Record";
      }
      else {
        this.dialogTitle = 'New Vehcile Greece Niple Record';
      }
  
      this.greeceNipleForm = this.createForm();
    }

  ngOnInit(): void {
  }


  createForm() {

    let greeceNipleReplaceDate =new Date();
    let nextGreeceNipleReplaceDate= new Date();
    nextGreeceNipleReplaceDate.setDate(greeceNipleReplaceDate.getDate()+365);

    if(this.greeceNiple.id>0)
    {
      greeceNipleReplaceDate = new Date(this.greeceNiple.greeceNipleReplacYear,this.greeceNiple.greeceNipleReplacMonth,this.greeceNiple.greeceNipleReplacDay,0,0,0);

      nextGreeceNipleReplaceDate = new Date(this.greeceNiple.nextGreeceNipleReplaceYear,this.greeceNiple.nextGreeceNipleReplaceMonth,this.greeceNiple.nextGreeceNipleReplaceDay,0,0,0);
    }


    

    return new FormGroup({
      id: new FormControl({ value: this.greeceNiple.id, disabled: true }),
      registrationNo:new FormControl({ value: this.greeceNiple.registrationNo, disabled: true }), 
      vehicleId: new FormControl(this.greeceNiple.vehicleId, Validators.required),
      greeceNipleReplaceDate: new FormControl({ value: greeceNipleReplaceDate, disabled: this.isReadOnly }),
      nextGreeceNipleReplaceDate: new FormControl({ value: nextGreeceNipleReplaceDate, disabled: this.isReadOnly }),
      note: new FormControl({ value: this.greeceNiple.note, disabled: this.isReadOnly }),
      isActive: new FormControl(this.greeceNiple.isActive),
      createdOn: new FormControl({ value: this.greeceNiple.createdOn, disabled: true }),
      updatedOn: new FormControl({ value: this.greeceNiple.updatedOn, disabled: true }),
    });
  }
}
