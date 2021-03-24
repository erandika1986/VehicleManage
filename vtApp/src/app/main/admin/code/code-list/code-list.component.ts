import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { FuseConfirmDialogComponent } from '@fuse/components/confirm-dialog/confirm-dialog.component';
import { FuseProgressBarService } from '@fuse/components/progress-bar/progress-bar.service';
import { DropDownModel } from 'app/models/common/drop-down.modal';
import { CodeModel } from 'app/models/vehicle/code.model';
import { MasterDataCodeService } from 'app/services/vehicle/master-data-code.service';
import { CodeFormComponent } from '../code-form/code-form.component';

@Component({
  selector: 'code-list',
  templateUrl: './code-list.component.html',
  styleUrls: ['./code-list.component.scss'],
  animations: fuseAnimations,
  encapsulation: ViewEncapsulation.None
})
export class CodeListComponent implements OnInit {
  horizontalPosition: MatSnackBarHorizontalPosition = 'right';
  verticalPosition: MatSnackBarVerticalPosition = 'top';

  selectedCodeType: DropDownModel;
  dialogRef: any;
  confirmDialogRef: MatDialogRef<FuseConfirmDialogComponent>;

  totalNumberOfRecords: number;

  dataSource = new MatTableDataSource([]);
  displayedColumns = ["editbuttons", "deletebuttons", "id", "code"];

  @ViewChild(MatPaginator) paginator: MatPaginator;

  @ViewChild(MatSort) sort: MatSort;

  constructor(private _fuseProgressBarService: FuseProgressBarService,
    private _masterDataService: MasterDataCodeService,
    private _matDialog: MatDialog,
    private _snackBar: MatSnackBar,
    public _router: ActivatedRoute) { }

  ngOnInit(): void {

    this._masterDataService.onFilterChanged.subscribe(value => {
      this.selectedCodeType = value;
      this.getCodeListForSelectedType();
    });
  }

  getCodeListForSelectedType() {
    this._fuseProgressBarService.show();
    this._masterDataService.getAllCodesForSelectedCodeType(this.selectedCodeType.id)
      .subscribe(response => {
        this._fuseProgressBarService.hide();
        this.dataSource = new MatTableDataSource(response);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;

      }, error => {
        this._fuseProgressBarService.hide();
      });
  }

  addCode() {
    let code: CodeModel = new CodeModel();
    code.id = 0;
    code.code = "";
    code.selectedCodeType = this.selectedCodeType.id;
    code.selectedCode = this.selectedCodeType.name;

    this.dialogRef = this._matDialog.open(CodeFormComponent, {
      panelClass: 'code-form-dialog',
      data: {
        code: code,
        action: "add"
      }
    });

    this.dialogRef.afterClosed()
      .subscribe(response => {
        if (!response) {
          return;
        }

        const formData: FormGroup = response;
        this.saveCode(formData.getRawValue());

      });
  }

  editCode(item: CodeModel) {
    item.selectedCode = this.selectedCodeType.name;
    this.dialogRef = this._matDialog.open(CodeFormComponent, {
      panelClass: 'code-form-dialog',
      data: {
        code: item,
        action: "edit"
      }
    });

    this.dialogRef.afterClosed()
      .subscribe(response => {
        if (!response) {
          return;
        }
        const actionType: string = response[0];
        const formData: FormGroup = response[1];
        switch (actionType) {
          /**
           * Save
           */
          case 'save':
            this.saveCode(formData.getRawValue());
            //this.events[eventIndex] = Object.assign(this.events[eventIndex], formData.getRawValue());
            //this.refresh.next(true);

            break;
          /**
           * Delete
           */
          case 'delete':

            this.deleteCode(formData.getRawValue());

            break;
        }
      });
  }

  deleteCode(item: CodeModel) {
    this.confirmDialogRef = this._matDialog.open(FuseConfirmDialogComponent, {
      disableClose: false
    });

    this.confirmDialogRef.componentInstance.confirmMessage = 'Are you sure you want to delete this record?';

    this.confirmDialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._fuseProgressBarService.show();
        this._masterDataService.deleteCode(item)
          .subscribe(response => {

            this._fuseProgressBarService.hide();
            if (response.isSuccess) {
              this._snackBar.open(response.message, 'Success', {
                duration: 2500,
                horizontalPosition: this.horizontalPosition,
                verticalPosition: this.verticalPosition,
              });

              this.getCodeListForSelectedType();
            }
            else {
              this._snackBar.open(response.message, 'Error', {
                duration: 2500,
                horizontalPosition: this.horizontalPosition,
                verticalPosition: this.verticalPosition,
              });
            }
          }, error => {
            this._fuseProgressBarService.hide();
            this._snackBar.open("Network error has been occured. Please try again.", 'Error', {
              duration: 2500,
              horizontalPosition: this.horizontalPosition,
              verticalPosition: this.verticalPosition,
            });
          });
      }
      this.confirmDialogRef = null;
    });
  }

  saveCode(vm: CodeModel) {
    this._fuseProgressBarService.show();
    this._masterDataService.saveCode(vm)
      .subscribe(response => {

        this._fuseProgressBarService.hide();
        if (response.isSuccess) {
          this._snackBar.open(response.message, 'Success', {
            duration: 2500,
            horizontalPosition: this.horizontalPosition,
            verticalPosition: this.verticalPosition,
          });

          this.getCodeListForSelectedType();
        }
        else {
          this._snackBar.open(response.message, 'Error', {
            duration: 2500,
            horizontalPosition: this.horizontalPosition,
            verticalPosition: this.verticalPosition,
          });
        }
      }, error => {
        this._fuseProgressBarService.hide();
        this._snackBar.open("Network error has been occured. Please try again.", 'Error', {
          duration: 2500,
          horizontalPosition: this.horizontalPosition,
          verticalPosition: this.verticalPosition,
        });
      });
  }

}
