import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import {
  NextFuelFilterMilageDetailsFormComponent
} from './next-fuel-filter-milage-details-form/next-fuel-filter-milage-details-form.component';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { VehicleFuelFilterMilageModel } from 'models/vehicle/vehicle-fuel-filter-milage.model';
import { VehicleFuelFilterChangeMilageService } from 'services/vehicle/vehicle-fuel-filter-change-milage.service';
import { VehicleMessageService } from 'services/vehicle/vehicle-message.service';

@Component({
  selector: 'app-next-fuel-filter-milage-details',
  templateUrl: './next-fuel-filter-milage-details.component.html',
  styleUrls: ['./next-fuel-filter-milage-details.component.scss']
})
export class NextFuelFilterMilageDetailsComponent implements OnInit {

  vehicleFFM: VehicleFuelFilterMilageModel[];

  vehicleId = 0;

  currentPage = 1;
  pageSize = 2;
  totalPageCount = 0;
  totalRecordCount = 0;

  currentPageItemCount = 0;

  isSuccess: boolean;
  message: string;

  subscription: Subscription;

  constructor(private ngbModal: NgbModal,
    private vehicleFFMService: VehicleFuelFilterChangeMilageService,
    private toastrService: ToastrService,
    private vehicleMessageService: VehicleMessageService,
    private router: Router, private route: ActivatedRoute) {

    this.subscription = this.vehicleMessageService.getMessageToReloadVehicleFFM().
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
    this.vehicleFFMService.getAllVehicleFFM(this.vehicleId, this.pageSize, this.currentPage).subscribe(response => {
      console.log(response);
      this.vehicleFFM = response.data;
      this.totalRecordCount = response.totalRecordCount;
    });
  }

  onPageChange() {
    this.get();
  }

  addOrEdit(id: number, isReadOnly: boolean) {
    const ngbModalRef = this.ngbModal.
      open(NextFuelFilterMilageDetailsFormComponent, { centered: true, size: 'lg', backdrop: 'static' });
    ngbModalRef.componentInstance.data = { id: id, vehicleId: this.vehicleId, totalRecordCount: this.totalRecordCount, isReadOnly: isReadOnly };
  }

  delete(id: number, index: number) {
    this.vehicleFFMService.deleteVehicleFFM(id).subscribe(res => {
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
