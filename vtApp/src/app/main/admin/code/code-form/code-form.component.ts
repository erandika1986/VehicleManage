import { Component, Inject, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatColors } from '@fuse/mat-colors';
import { CodeModel } from 'app/models/vehicle/code.model';

@Component({
  selector: 'app-code-form',
  templateUrl: './code-form.component.html',
  styleUrls: ['./code-form.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class CodeFormComponent implements OnInit {

  action: string;
  code: CodeModel;
  codeForm: FormGroup;
  dialogTitle: string;
  presetColors = MatColors.presets;

  /**
 * Constructor
 *
 * @param {MatDialogRef<CalendarEventFormDialogComponent>} matDialogRef
 * @param _data
 * @param {FormBuilder} _formBuilder
 */


  constructor(public matDialogRef: MatDialogRef<CodeFormComponent>,
    @Inject(MAT_DIALOG_DATA) private _data: any,
    private _formBuilder: FormBuilder) {
    this.code = _data.code;
    this.action = _data.action;

    if (this.action === 'edit') {
      this.dialogTitle = "Edit Code";
    }
    else {
      this.dialogTitle = 'New Code';
      /*         this.event = new CalendarEventModel({
                  start: _data.date,
                  end  : _data.date
              }); */
    }

    this.codeForm = this.createCodeForm();
  }

  ngOnInit(): void {
  }

  createCodeForm() {
    return new FormGroup({
      selectedCodeType: new FormControl({ value: this.code.selectedCodeType, disabled: false }),
      selectedCode: new FormControl({ value: this.code.selectedCode, disabled: true }),
      id: new FormControl({ value: this.code.id, disabled: true }),
      code: new FormControl(this.code.code, Validators.required),

    });
  }

}
