import { Component, Inject } from '@angular/core';
import { DialogData } from 'src/app/models/common/dialog-data';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
    selector: 'dialog-overview-example-dialog',
    templateUrl: 'confirmation-dialog.html',
})
export class ConfirmationDialog {

    constructor(
        public dialogRef: MatDialogRef<ConfirmationDialog>,
        @Inject(MAT_DIALOG_DATA) public data: DialogData) { }

    onYesClick(): void {
        this.dialogRef.close(true);
    }

    onNoClick(): void {
        this.dialogRef.close(false);
    }
}