import { DropDownModel } from './../../../../models/common/drop-down.modal';
import { WarehouseService } from 'app/services/warehouse/warehouse.service';
import { Component, Inject, OnInit, ViewEncapsulation } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatColors } from '@fuse/mat-colors';
import { WarehouseModel } from 'app/models/warehouse/warehouse.model';

@Component({
  selector: 'app-wherehouse-detail',
  templateUrl: './wherehouse-detail.component.html',
  styleUrls: ['./wherehouse-detail.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class WherehouseDetailComponent implements OnInit {

  action: string;
  warehouse: WarehouseModel;
  warehouseForm: FormGroup;
  dialogTitle: string;
  presetColors = MatColors.presets;
  managers:DropDownModel[]=[];
  /**
* Constructor
*
* @param {MatDialogRef<CalendarEventFormDialogComponent>} matDialogRef
* @param _data
* @param {FormBuilder} _formBuilder
*/
constructor(public matDialogRef: MatDialogRef<WherehouseDetailComponent>,
  private _warehouseService:WarehouseService,
  @Inject(MAT_DIALOG_DATA) private _data: any) {
  this.warehouse = _data.warehouse;
  this.action = _data.action;
console.log(this.warehouse);

  if (this.action === 'edit') {
    this.dialogTitle = "Edit Warehouse";
  }
  else {
    this.dialogTitle = 'New Warehouse';
    /*         this.event = new CalendarEventModel({
                start: _data.date,
                end  : _data.date
            }); */
  }

  this.warehouseForm = this.createWarehouseForm();

}

ngOnInit(): void {
  this.getAllManagers();
}



createWarehouseForm() {
  return new FormGroup({
    id: new FormControl({ value: this.warehouse.id, disabled: true }),
    address: new FormControl(this.warehouse.address, Validators.required),
    phone: new FormControl(this.warehouse.phone,Validators.required),
    selectedManagerId: new FormControl(this.warehouse.selectedManagerId,Validators.required),
    floorSpace: new FormControl(this.warehouse.floorSpace, Validators.required),
    iSActive:new FormControl(this.warehouse.isActive)
  });
}



 
getAllManagers(){
  this._warehouseService.getAllManagers()
   .subscribe(response=>{
       this.managers = response;
   },error=>{

   });
} 

}
