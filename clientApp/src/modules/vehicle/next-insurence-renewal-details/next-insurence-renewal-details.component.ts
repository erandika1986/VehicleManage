import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import {
  NextInsurenceRenewalDetailsFormComponent
} from './next-insurence-renewal-details-form/next-insurence-renewal-details-form.component';
import { ToastrService } from 'ngx-toastr';
import { VehicleMessageService } from 'src/app/services/vehicle/vehicle-message.service';
import { VehicleInsuranceModel } from 'src/app/models/vehicle/vehicle-insurance.model';
import { Subscription } from 'rxjs';
import { Router, ActivatedRoute } from '@angular/router';
import { VehicleInsuranceService } from 'src/app/services/vehicle/vehicle-insurance.service';

@Component({
  selector: 'app-next-insurence-renewal-details',
  templateUrl: './next-insurence-renewal-details.component.html',
  styleUrls: ['./next-insurence-renewal-details.component.scss']
})
export class NextInsurenceRenewalDetailsComponent implements OnInit {

  vehicleI: VehicleInsuranceModel[];

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
    private vehicleIService: VehicleInsuranceService,
    private toastrService: ToastrService,
    private vehicleMessageService: VehicleMessageService,
    private router: Router, private route: ActivatedRoute) {
    this.subscription = this.vehicleMessageService.getMessageToReloadVehicleI().
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

  onPageChange() {
    this.get();
  }

  get() {
    this.vehicleIService.getAllVehicleI(this.vehicleId, this.pageSize, this.currentPage).subscribe(response => {
      this.vehicleI = response.data;
      this.totalRecordCount = response.totalRecordCount;
    });
  }

  addOrEdit(id: number, isReadOnly: boolean) {
    const ngbModalRef = this.ngbModal.
      open(NextInsurenceRenewalDetailsFormComponent, { centered: true, size: 'lg', backdrop: 'static' });
    ngbModalRef.componentInstance.data = { id: id, vehicleId: this.vehicleId, totalRecordCount: this.totalRecordCount, isReadOnly: isReadOnly };
  }

  delete(id: number, index: number) {
    this.vehicleIService.deleteVehicleI(id).subscribe(res => {
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
