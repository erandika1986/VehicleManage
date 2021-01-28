import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { VehicleTypeFormComponent } from '../vehicle-type-form/vehicle-type-form.component';
import { NgbModal, ModalDismissReasons, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { Subscription } from 'rxjs';
import { NgxSpinnerService } from 'ngx-spinner';
import { MatDialog } from '@angular/material/dialog';
import { VehicleTypeModel } from 'models/vehicle/vehicle-type.model';
import { VehicleService } from 'services/vehicle/vehicle.service';
import { VehicleMessageService } from 'services/vehicle/vehicle-message.service';
import { DialogData } from 'models/common/dialog-data';
import { ConfirmationDialog } from 'shared/confirmation-dialog/confirmation-dialog';

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
