import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NextAirCleanerDetailsFormComponent } from './next-air-cleaner-details-form/next-air-cleaner-details-form.component';
import { VehicleService } from 'src/app/services/vehicle/vehicle.service';
import { ToastrService } from 'ngx-toastr';
import { VehicleMessageService } from 'src/app/services/vehicle/vehicle-message.service';
import { Subscription } from 'rxjs';
import { VehicleAirCleanerModel } from 'src/app/models/vehicle/vehicle-air-cleaner.model';
import { VehicleAirCleanerReplaceMilageService } from 'src/app/services/vehicle/vehicle-air-cleaner-replace-milage.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-next-air-cleaner-details',
  templateUrl: './next-air-cleaner-details.component.html',
  styleUrls: ['./next-air-cleaner-details.component.scss']
})
export class NextAirCleanerDetailsComponent implements OnInit {

  vehicleAC: VehicleAirCleanerModel[];

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
    private vehicleACService: VehicleAirCleanerReplaceMilageService,
    private toastrService: ToastrService,
    private vehicleMessageService: VehicleMessageService,
    private router: Router, private route: ActivatedRoute) {

    this.subscription = this.vehicleMessageService.getMessageToReloadVehicleAC().
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
    this.vehicleACService.getAllVehicleACRM(this.vehicleId, this.pageSize, this.currentPage).subscribe(response => {
      this.vehicleAC = response.data;
      this.totalRecordCount = response.totalRecordCount;
    });
  }

  onPageChange() {
    this.get();
  }

  addOrEdit(id: number, isReadOnly: boolean) {
    const ngbModalRef = this.ngbModal.
      open(NextAirCleanerDetailsFormComponent, { centered: true, size: 'lg', backdrop: 'static' });
    ngbModalRef.componentInstance.data = { id: id, vehicleId: this.vehicleId, totalRecordCount: this.totalRecordCount, isReadOnly: isReadOnly };
  }

  delete(id: number, index: number) {

    this.vehicleACService.deleteVehicleACRM(id).subscribe(res => {
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
