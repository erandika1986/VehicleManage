import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import {
  NextEngineOilMilageDetailsFormComponent
} from './next-engine-oil-milage-details-form/next-engine-oil-milage-details-form.component';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { Router, ActivatedRoute } from '@angular/router';
import { VehicleEngineOilMilageModel } from 'models/vehicle/vehicle-engine-oil-milage.model';
import { VehicleEngineOilChangeMilageService } from 'services/vehicle/vehicle-engine-oil-change-milage.service';
import { VehicleMessageService } from 'services/vehicle/vehicle-message.service';

@Component({
  selector: 'app-next-engine-oil-milage-details',
  templateUrl: './next-engine-oil-milage-details.component.html',
  styleUrls: ['./next-engine-oil-milage-details.component.scss']
})
export class NextEngineOilMilageDetailsComponent implements OnInit {

  vehicleEOM: VehicleEngineOilMilageModel[];

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
    private vehicleEOMService: VehicleEngineOilChangeMilageService,
    private toastrService: ToastrService,
    private vehicleMessageService: VehicleMessageService,
    private router: Router, private route: ActivatedRoute) {

    this.subscription = this.vehicleMessageService.getMessageToReloadVehicleEOM().
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
    this.vehicleEOMService.getAllVehicleEOCM(this.vehicleId, this.pageSize, this.currentPage).subscribe(response => {
      this.vehicleEOM = response.data;
      this.totalRecordCount = response.totalRecordCount;
    });
  }

  onPageChange() {
    this.get();
  }

  addOrEdit(id: number, isReadOnly: boolean) {
    const ngbModalRef = this.ngbModal.
      open(NextEngineOilMilageDetailsFormComponent, { centered: true, size: 'lg', backdrop: 'static' });
    ngbModalRef.componentInstance.data = { id: id, vehicleId: this.vehicleId, totalRecordCount: this.totalRecordCount, isReadOnly: isReadOnly };
  }

  delete(id: number, index: number) {
    this.vehicleEOMService.deleteVehicleEOCM(id).subscribe(res => {
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
