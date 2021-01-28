import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NextRevenueLicenceDetailsFormComponent } from './next-revenue-licence-details-form/next-revenue-licence-details-form.component';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { VehicleRevenueLicenceModel } from 'models/vehicle/vehicle-revenue-licence.model';
import { VehicleRevenueLicenceService } from 'services/vehicle/vehicle-revenue-licence.service';
import { VehicleMessageService } from 'services/vehicle/vehicle-message.service';

@Component({
  selector: 'app-next-revenue-licence-details',
  templateUrl: './next-revenue-licence-details.component.html',
  styleUrls: ['./next-revenue-licence-details.component.scss']
})
export class NextRevenueLicenceDetailsComponent implements OnInit {

  vehicleRL: VehicleRevenueLicenceModel[];

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
    private vehicleRLService: VehicleRevenueLicenceService,
    private toastrService: ToastrService,
    private vehicleMessageService: VehicleMessageService,
    private router: Router, private route: ActivatedRoute) {
    this.subscription = this.vehicleMessageService.getMessageToReloadVehicleRL().
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
    this.vehicleRLService.getAllVehicleRL(this.vehicleId, this.pageSize, this.currentPage).subscribe(response => {
      this.vehicleRL = response.data;
      this.totalRecordCount = response.totalRecordCount;
    });
  }

  addOrEdit(id: number, isReadOnly: boolean) {
    const ngbModalRef = this.ngbModal.
      open(NextRevenueLicenceDetailsFormComponent, { centered: true, size: 'lg', backdrop: 'static' });
    ngbModalRef.componentInstance.data = { id: id, vehicleId: this.vehicleId, totalRecordCount: this.totalRecordCount, isReadOnly: isReadOnly };
  }

  delete(id: number, index: number) {
    this.vehicleRLService.deleteVehicleRL(id).subscribe(res => {
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
