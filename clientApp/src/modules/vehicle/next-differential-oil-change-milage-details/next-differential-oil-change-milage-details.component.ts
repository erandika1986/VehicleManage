import { Component, OnInit } from '@angular/core';
import { NgbModal, ModalDismissReasons, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import {
  NextDifferentialOilChangeMilageDetailsFormComponent
} from './next-differential-oil-change-milage-details-form/next-differential-oil-change-milage-details-form.component';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { Router, ActivatedRoute } from '@angular/router';
import { VehicleDifferentialOilChangeMilageModel } from 'models/vehicle/vehicle-differential-oil-change-milage.model';
import { VehicleDifferentialOilChangeMilageService } from 'services/vehicle/vehicle-differential-oil-change-milage.service';
import { VehicleMessageService } from 'services/vehicle/vehicle-message.service';

@Component({
  selector: 'app-next-differential-oil-change-milage-details',
  templateUrl: './next-differential-oil-change-milage-details.component.html',
  styleUrls: ['./next-differential-oil-change-milage-details.component.scss']
})
export class NextDifferentialOilChangeMilageDetailsComponent implements OnInit {

  vehicleDOCM: VehicleDifferentialOilChangeMilageModel[];

  vehicleId = 0;

  currentPage = 1;
  pageSize = 15;
  totalPageCount = 0;
  totalRecordCount = 0;

  currentPageItemCount = 0;

  isSuccess: boolean;
  message: string;

  subscription: Subscription;

  constructor(private ngbModal: NgbModal,
    private vehicleDOCMService: VehicleDifferentialOilChangeMilageService,
    private toastrService: ToastrService,
    private vehicleMessageService: VehicleMessageService,
    private router: Router, private route: ActivatedRoute) {

    this.subscription = this.vehicleMessageService.getMessageToReloadVehicleDOCM().
      subscribe(response => {
        if (response.isReload) {
          this.get();
        }
      });
  }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.vehicleId = + params.get('id');
      this.get();
    });
  }

  get() {
    this.vehicleDOCMService.getAllVehicleDOCM(this.vehicleId, this.pageSize, this.currentPage).subscribe(response => {
      this.vehicleDOCM = response.data;
      this.totalRecordCount = response.totalRecordCount;
    });
  }

  onPageChange() {
    this.get();
  }

  addOrEdit(id: number, isReadOnly: boolean) {
    const ngbModalRef = this.ngbModal.
      open(NextDifferentialOilChangeMilageDetailsFormComponent, { centered: true, size: 'lg', backdrop: 'static' });
    ngbModalRef.componentInstance.data = { id: id, vehicleId: this.vehicleId, totalRecordCount: this.totalRecordCount, isReadOnly: isReadOnly };
  }

  delete(id: number, index: number) {
    this.vehicleDOCMService.deleteVehicleDOCM(id).subscribe(res => {
      this.isSuccess = res.isSuccess;
      this.message = res.message;
      if (this.isSuccess === true) {
        this.toastrService.success(this.message);
        this.get();
      } else {
        this.toastrService.error(this.message);
      }
    });
  }
}
