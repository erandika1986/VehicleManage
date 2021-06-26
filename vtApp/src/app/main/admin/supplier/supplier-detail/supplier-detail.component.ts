import { Component, Inject, OnInit, ViewEncapsulation } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatColors } from '@fuse/mat-colors';
import { SupplierModel } from 'app/models/supplier/supplier.model';
import { SupplierService } from 'app/services/supplier/supplier.service';

@Component({
  selector: 'app-supplier-detail',
  templateUrl: './supplier-detail.component.html',
  styleUrls: ['./supplier-detail.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class SupplierDetailComponent implements OnInit {

  action: string;
  supplier: SupplierModel;
  supplierForm: FormGroup;
  dialogTitle: string;
  presetColors = MatColors.presets;
  //managers:DropDownModel[]=[];
  /**
* Constructor
*
* @param {MatDialogRef<CalendarEventFormDialogComponent>} matDialogRef
* @param _data
* @param {FormBuilder} _formBuilder
*/
constructor(public matDialogRef: MatDialogRef<SupplierDetailComponent>,
  private _supplierService:SupplierService,
  @Inject(MAT_DIALOG_DATA) private _data: any) {
  this.supplier = _data.supplier;
  this.action = _data.action;
console.log(this.supplier);

  if (this.action === 'edit') {
    this.dialogTitle = "Edit Supplier";
  }
  else {
    this.dialogTitle = 'New Supplier';
    /*         this.event = new CalendarEventModel({
                start: _data.date,
                end  : _data.date
            }); */
  }

  this.supplierForm = this.createSupplierForm();

}
ngOnInit(): void {
}

createSupplierForm() {
  return new FormGroup({
    id: new FormControl({ value: this.supplier.id, disabled: true }),
    name: new FormControl(this.supplier.name, Validators.required),
    description: new FormControl(this.supplier.description,Validators.required),
    address: new FormControl(this.supplier.address,Validators.required),
    phone1: new FormControl(this.supplier.phone1,Validators.required),
    phone2: new FormControl(this.supplier.phone2,Validators.required),
    email1: new FormControl(this.supplier.email1,Validators.required),
    email2: new FormControl(this.supplier.email2,Validators.required),
    iSActive:new FormControl(this.supplier.isActive)
  });
} 

}
