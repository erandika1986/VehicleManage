import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { VehicleTypeFormComponent } from '../vehicle-type-form/vehicle-type-form.component';
import { NgbModal, ModalDismissReasons, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { VehicleService } from 'src/app/services/vehicle/vehicle.service';
import { VehicleTypeModel } from 'src/app/models/vehicle/vehicle-type.model';
import { VehicleMessageService } from 'src/app/services/vehicle/vehicle-message.service';
import { Subscription } from 'rxjs';
import { NgxSpinnerService } from 'ngx-spinner';
import { DialogData } from 'src/app/models/common/dialog-data';
import { ConfirmationDialog } from 'src/app/shared/confirmation-dialog/confirmation-dialog';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-vehicle-types',
  templateUrl: './vehicle-types.component.html',
  styleUrls: ['./vehicle-types.component.scss']
})
export class VehicleTypesComponent implements OnInit {

  vehicleTypes: VehicleTypeModel[];

  currentPage = 1;
  pageSize = 20;
  totalPageCount = 0;
  totalRecordCount = 0;
  currentPageItemCount = 0;

  subscription: Subscription;


  constructor(private vehicleService: VehicleService,
    private toastrService: ToastrService,
    private spinner: NgxSpinnerService,
    private vehicleMessageService: VehicleMessageService,
    public dialog: MatDialog,
    private ngbModal: NgbModal) {

    this.subscription = this.vehicleMessageService.getMessageToReloadVehicleTypesTable().
      subscribe(response => {
        if (response.isReload) {
          this.getAllVehicleTypes();
        }
      });
  }

  ngOnInit() {
    this.getAllVehicleTypes();
  }

  onPageChange() {
    this.getAllVehicleTypes();
  }

  getAllVehicleTypes() {
    this.vehicleService.getAllVehicleTypes(this.pageSize, this.currentPage).subscribe(res => {
      this.vehicleTypes = res.data;
      this.totalRecordCount = res.totalRecordCount;
    });
  }

  addOrEditVehicleType(id: number, index?: number) {
    const ngbModalRef = this.ngbModal.open(VehicleTypeFormComponent, { centered: true, size: 'xl', backdrop: 'static' });
    ngbModalRef.componentInstance.data = id;
  }

  deleteVehicleType(id: number, index: number) {

    let data: DialogData = new DialogData();
    data.header = "Please confirm.";
    data.message = "Do you really want to delete this selected record ?";
    const dialogRef = this.dialog.open(ConfirmationDialog, {
      width: '250px',
      data: data
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.spinner.show();
        this.vehicleService.deleteVehicleType(id).subscribe(response => {
          this.spinner.hide();
          if (response.isSuccess === true) {
            this.toastrService.success(response.message);
            this.getAllVehicleTypes();
          } else {
            this.toastrService.error(response.message);
          }
        }, error => {
          this.spinner.hide();
        });
      }
    });


  }

}
