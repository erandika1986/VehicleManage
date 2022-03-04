import { Component, Inject, OnInit, ViewEncapsulation } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatColors } from '@fuse/mat-colors';
import { RouteModel } from 'app/models/route/route.model';

@Component({
  selector: 'app-route-detail',
  templateUrl: './route-detail.component.html',
  styleUrls: ['./route-detail.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class RouteDetailComponent implements OnInit {

  action: string;
  route: RouteModel;
  routeForm: FormGroup;
  dialogTitle: string;
  presetColors = MatColors.presets;

  /**
* Constructor
*
* @param {MatDialogRef<CalendarEventFormDialogComponent>} matDialogRef
* @param _data
* @param {FormBuilder} _formBuilder
*/

  constructor(public matDialogRef: MatDialogRef<RouteDetailComponent>,
    @Inject(MAT_DIALOG_DATA) private _data: any) {
    this.route = _data.route;
    this.action = _data.action;

    if (this.action === 'edit') {
      this.dialogTitle = "Edit Route";
    }
    else {
      this.dialogTitle = 'New Route';
      /*         this.event = new CalendarEventModel({
                  start: _data.date,
                  end  : _data.date
              }); */
    }

    this.routeForm = this.createRouteForm();

  }

  ngOnInit(): void {
  }



  createRouteForm() {
    return new FormGroup({
      id: new FormControl({ value: this.route.id, disabled: true }),
      routeCode: new FormControl(this.route.routeCode, Validators.required),
      name:new FormControl(this.route.name, Validators.required),
      startFrom: new FormControl(this.route.startFrom, Validators.required),
      endFrom: new FormControl(this.route.endFrom, Validators.required),
      totalDistance: new FormControl(this.route.totalDistance, Validators.required),
      isActive: new FormControl(true),
    });
  }

}
