import { Component, Inject, OnInit, ViewEncapsulation } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FuseProgressBarService } from '@fuse/components/progress-bar/progress-bar.service';
import { MatColors } from '@fuse/mat-colors';
import { DropDownModel } from 'app/models/common/drop-down.modal';
import { CustomerModel } from 'app/models/customer/customer.model';
import { CustomerService } from 'app/services/customer/customer.service';

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
  priorities:DropDownModel[]=[];


constructor(public matDialogRef: MatDialogRef<ClientDetailComponent>,
  private _customerService:CustomerService,
  private _fuseProgressBarService: FuseProgressBarService,
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



}

  ngOnInit(): void {
    this.getMasterData();
    this.clientForm = this.createClientForm();
  }


  createClientForm() {
    return new FormGroup({
      id: new FormControl({ value: this.customer.id, disabled: true }),
      name: new FormControl(this.customer.name, Validators.required),
      contactNo1: new FormControl(this.customer.contactNo1),
      contactNo2: new FormControl(this.customer.contactNo2),
      email: new FormControl(this.customer.email),
      address: new FormControl(this.customer.address, Validators.required),
      latitude: new FormControl({ value: this.customer.latitude, disabled: true }),
      longitude: new FormControl({ value: this.customer.longitude, disabled: true }),
      priority: new FormControl(this.customer.priority, Validators.required),
      routeId: new FormControl(this.customer.routeId, Validators.required),
      description: new FormControl(this.customer.description),
      isActive: new FormControl(true),
    });
  }

  getMasterData()
  {
    this._fuseProgressBarService.show();
    this._customerService.getCustomerMasterData().subscribe(response=>
      {
        this._fuseProgressBarService.hide();
        this.priorities = response.priorities;
        this.routes =response.routes;

      },error=>{
      this._fuseProgressBarService.hide();
    })
  }

}

