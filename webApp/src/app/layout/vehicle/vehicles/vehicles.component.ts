import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { VehicleModel } from 'src/app/models/vehicle/vehicle.model';
import { VehiclePaginatedItemsModel } from 'src/app/models/vehicle/vehicle-paginated.items.model';
import { VehicleService } from 'src/app/services/vehicle/vehicle.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { DialogData } from 'src/app/models/common/dialog-data';
import { ConfirmationDialog } from 'src/app/shared/confirmation-dialog/confirmation-dialog';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-vehicles',
  templateUrl: './vehicles.component.html',
  styleUrls: ['./vehicles.component.scss']
})
export class VehiclesComponent implements OnInit {

  currentPage = 1;
  pageSize = 20;
  totalPageCount = 0;
  totalRecordCount = 0;
  currentPageItemCount = 0;
  dataContainer: VehiclePaginatedItemsModel;
  rowData: VehicleModel[];

  isSuccess: boolean;
  message: string;


  constructor(private router: Router,
    private vehicleService: VehicleService,
    private spinner: NgxSpinnerService,
    private toastrService: ToastrService,
    public dialog: MatDialog) { }

  ngOnInit() {

    this.getVehicleList();
  }

  edit(id: number) {
    this.router.navigate(['vehicle/vehicle-detail/' + id]);
  }

  getVehicleList() {
    this.spinner.show();
    this.vehicleService.getAllVehicles(this.pageSize, this.currentPage)
      .subscribe(response => {

        this.spinner.hide();
        this.rowData = response.data;
        this.dataContainer = response;
        this.totalRecordCount = response.totalRecordCount;
      }, error => {
        this.spinner.hide();
      });
  }

  delete(id: number, index: number) {

    let data1: DialogData = new DialogData();
    data1.header = "Please confirm.";
    data1.message = "Do you really want to delete this selected record ?";
    const dialogRef = this.dialog.open(ConfirmationDialog, {
      width: '250px',
      data: data1
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.spinner.show();
        this.vehicleService.deleteVehicle(id).subscribe(response => {
          this.spinner.hide();
          if (response.isSuccess === true) {
            this.toastrService.success(response.message);
            this.getVehicleList();
          } else {
            this.toastrService.error(response.message);
          }
        }, error => {
          this.spinner.hide();
        });
      }
    });

  }

  onPageChange() {
    this.getVehicleList();
  }


}
