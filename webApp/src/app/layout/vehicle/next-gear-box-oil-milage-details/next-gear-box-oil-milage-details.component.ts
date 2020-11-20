import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import {
  NextGearBoxOilMilageDetailsFormComponent
} from './next-gear-box-oil-milage-details-form/next-gear-box-oil-milage-details-form.component';
import { ToastrService } from 'ngx-toastr';
import { VehicleMessageService } from 'src/app/services/vehicle/vehicle-message.service';
import { Subscription } from 'rxjs';
import { VehicleGearBoxOilMilageModel } from 'src/app/models/vehicle/vehicle-gear-box-oil-milage.model';
import { VehicleGearBoxOilChangeMilageService } from 'src/app/services/vehicle/vehicle-gear-box-oil-change-milage.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-next-gear-box-oil-milage-details',
  templateUrl: './next-gear-box-oil-milage-details.component.html',
  styleUrls: ['./next-gear-box-oil-milage-details.component.scss']
})
export class NextGearBoxOilMilageDetailsComponent implements OnInit {

  vehicleGBOM: VehicleGearBoxOilMilageModel[];

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
    private vehicleGBOMService: VehicleGearBoxOilChangeMilageService,
    private toastrService: ToastrService,
    private vehicleMessageService: VehicleMessageService,
    private router: Router, private route: ActivatedRoute) {

    this.subscription = this.vehicleMessageService.getMessageToReloadVehicleGBOM().
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
    this.vehicleGBOMService.getAllVehicleGBOCM(this.vehicleId, this.pageSize, this.currentPage).subscribe(response => {
      this.vehicleGBOM = response.data;
      this.totalRecordCount = response.totalRecordCount;
    });
  }

  onPageChange() {
    this.get();
  }

  addOrEdit(id: number, isReadOnly: boolean) {
    const ngbModalRef = this.ngbModal.
      open(NextGearBoxOilMilageDetailsFormComponent, { centered: true, size: 'lg', backdrop: 'static' });
    ngbModalRef.componentInstance.data = { id: id, vehicleId: this.vehicleId, totalRecordCount: this.totalRecordCount, isReadOnly: isReadOnly };
  }

  delete(id: number, index: number) {
    this.vehicleGBOMService.deleteVehicleGBOCM(id).subscribe(res => {
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
