import { Component, Inject, OnInit, ViewEncapsulation } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatColors } from '@fuse/mat-colors';
import { DropDownModel } from 'app/models/common/drop-down.modal';
import { CustomerModel } from 'app/models/customer/customer.model';

@Component({
  selector: 'app-client-detail',
  templateUrl: './client-detail.component.html',
  styleUrls: ['./client-detail.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class ClientDetailComponent implements OnInit {
  action: string;
  customer: CustomerModel;
  clientForm: FormGroup;
  dialogTitle: string;
  presetColors = MatColors.presets;
  routes: DropDownModel[]=[];


constructor(public matDialogRef: MatDialogRef<ClientDetailComponent>,
  @Inject(MAT_DIALOG_DATA) private _data: any) {

  this.action = _data.action;
  this.customer = _data.customer;
  
  if (this.action === 'edit') {
    this.dialogTitle = "Edit Client";

  }
  else {
    this.dialogTitle = 'New Client';
    /*         this.event = new CalendarEventModel({
                start: _data.date,
                end  : _data.date
            }); */
  }

  this.clientForm = this.createClientForm();

}

  ngOnInit(): void {
  }


  createClientForm() {
    return new FormGroup({
      id: new FormControl({ value: this.customer.id, disabled: true }),
      name: new FormControl(this.customer.name, Validators.required),
      address: new FormControl(this.customer.address, Validators.required),
      contactNo1: new FormControl(this.customer.contactNo1, Validators.required),
      email: new FormControl(this.customer.email, Validators.required),
      isActive: new FormControl(true),
    });
  }

}

