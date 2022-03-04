import { Component, ElementRef, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { FuseConfirmDialogComponent } from '@fuse/components/confirm-dialog/confirm-dialog.component';
import { FuseProgressBarService } from '@fuse/components/progress-bar/progress-bar.service';
import { RouteModel } from 'app/models/route/route.model';
import { RouteService } from 'app/services/route/route.service';
import { RouteDetailComponent } from '../route-detail/route-detail.component';

@Component({
  selector: 'route-list',
  templateUrl: './route-list.component.html',
  styleUrls: ['./route-list.component.scss'],
  animations: fuseAnimations,
  encapsulation: ViewEncapsulation.None
})
export class RouteListComponent implements OnInit {

  horizontalPosition: MatSnackBarHorizontalPosition = 'right';
  verticalPosition: MatSnackBarVerticalPosition = 'top';

  dataSource = new MatTableDataSource([]);


  dialogRef: any;
  confirmDialogRef: MatDialogRef<FuseConfirmDialogComponent>;

  displayedColumns = ["buttons", "id", "routeCode","name", "startFrom", "endFrom", "totalDistance"];

  @ViewChild(MatPaginator) paginator: MatPaginator;

  @ViewChild(MatSort) sort: MatSort;

  @ViewChild('input') input: ElementRef;

  constructor(private _route: ActivatedRoute,
    private _routeService: RouteService,
    private _fuseProgressBarService: FuseProgressBarService,
    private _matDialog: MatDialog,
    private _snackBar: MatSnackBar,
    public _router: Router) { }

  ngOnInit(): void {

    this.loadRoutes();
  }


  editRoute(route: RouteModel): void {
    this.dialogRef = this._matDialog.open(RouteDetailComponent, {
      panelClass: 'route-form-dialog',
      data: {
        route: route,
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
            this.saveRoute(formData.getRawValue());


            break;
          /**
           * Delete
           */
          case 'delete':

            this.deleteRoute(formData.getRawValue());

            break;
        }
      });

  }

  addNewRoute(): void {

    let route: RouteModel = new RouteModel();
    route.id = 0;
    route.endFrom = "";
    route.name="";
    route.isActive = true;
    route.routeCode = "";
    route.startFrom = "";
    route.totalDistance = 0;

    this.dialogRef = this._matDialog.open(RouteDetailComponent, {
      panelClass: 'route-form-dialog',
      data: {
        route: route,
        action: "add"
      }
    });

    this.dialogRef.afterClosed()
      .subscribe(response => {
        if (!response) {
          return;
        }

        const formData: FormGroup = response;
        this.saveRoute(formData.getRawValue());

      });
  }


  loadRoutes() {
    this._fuseProgressBarService.show();
    this._routeService.getAllRoutes()
      .subscribe(response => {
        this._fuseProgressBarService.hide();
        this.dataSource = new MatTableDataSource(response);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
      }, error => {
        this._fuseProgressBarService.hide();
      })
  }


  applyFilter(filterValue: string) {
    filterValue = filterValue.trim(); // Remove whitespace
    filterValue = filterValue.toLowerCase(); // Datasource defaults to lowercase matches
    this.dataSource.filter = filterValue;
  }


  saveRoute(vm: RouteModel) {
    this._fuseProgressBarService.show();
    this._routeService.saveRoute(vm)
      .subscribe(response => {

        this._fuseProgressBarService.hide();
        if (response.isSuccess) {
          this._snackBar.open(response.message, 'Success', {
            duration: 2500,
            horizontalPosition: this.horizontalPosition,
            verticalPosition: this.verticalPosition,
          });

          this.loadRoutes();;
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


  deleteRoute(route: RouteModel) {
    this.confirmDialogRef = this._matDialog.open(FuseConfirmDialogComponent, {
      disableClose: false
    });

    this.confirmDialogRef.componentInstance.confirmMessage = 'Are you sure you want to delete this record?';

    this.confirmDialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._fuseProgressBarService.show();
        this._routeService.delete(route.id)
          .subscribe(response => {

            this._fuseProgressBarService.hide();
            if (response.isSuccess) {
              this._snackBar.open(response.message, 'Success', {
                duration: 2500,
                horizontalPosition: this.horizontalPosition,
                verticalPosition: this.verticalPosition,
              });

              this.loadRoutes();
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

}
