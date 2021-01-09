import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NextFitnessReportDetailsFormComponent } from './next-fitness-report-details-form/next-fitness-report-details-form.component';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { Router, ActivatedRoute } from '@angular/router';
import { VehicleFitnessReportModel } from 'models/vehicle/vehicle-fitness-report.model';
import { VehicleFitnessReportService } from 'services/vehicle/vehicle-fitness-report.service';
import { VehicleMessageService } from 'services/vehicle/vehicle-message.service';

@Component({
  selector: 'app-next-fitness-report-details',
  templateUrl: './next-fitness-report-details.component.html',
  styleUrls: ['./next-fitness-report-details.component.scss']
})
export class NextFitnessReportDetailsComponent implements OnInit {

  vehicleFR: VehicleFitnessReportModel[];

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
    private vehicleFRService: VehicleFitnessReportService,
    private toastrService: ToastrService,
    private vehicleMessageService: VehicleMessageService,
    private router: Router, private route: ActivatedRoute) {
    this.subscription = this.vehicleMessageService.getMessageToReloadVehicleFR().
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
    this.vehicleFRService.getAllVehicleFR(this.vehicleId, this.pageSize, this.currentPage).subscribe(response => {
      this.vehicleFR = response.data;
      this.totalRecordCount = response.totalRecordCount;
    });
  }

  addOrEdit(id: number, isReadOnly: boolean) {
    const ngbModalRef = this.ngbModal.open(NextFitnessReportDetailsFormComponent, { centered: true, size: 'lg', backdrop: 'static' });
    ngbModalRef.componentInstance.data = { id: id, vehicleId: this.vehicleId, totalRecordCount: this.totalRecordCount, isReadOnly: isReadOnly };
  }

  delete(id: number, index: number) {
    this.vehicleFRService.deleteVehicleFR(id).subscribe(res => {
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
