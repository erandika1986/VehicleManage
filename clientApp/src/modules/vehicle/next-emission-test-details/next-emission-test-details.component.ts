import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NextEmissionTestDetailsFormComponent } from './next-emission-test-details-form/next-emission-test-details-form.component';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { Router, ActivatedRoute } from '@angular/router';
import { VehicleEmissionTestModel } from 'models/vehicle/vehicle-emission-test.model';
import { VehicleEmissionTestService } from 'services/vehicle/vehicle-emission-test.service';
import { VehicleMessageService } from 'services/vehicle/vehicle-message.service';

@Component({
  selector: 'app-next-emission-test-details',
  templateUrl: './next-emission-test-details.component.html',
  styleUrls: ['./next-emission-test-details.component.scss']
})
export class NextEmissionTestDetailsComponent implements OnInit {

  vehicleET: VehicleEmissionTestModel[];

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
    private vehicleETService: VehicleEmissionTestService,
    private toastrService: ToastrService,
    private vehicleMessageService: VehicleMessageService,
    private router: Router, private route: ActivatedRoute) {
    this.subscription = this.vehicleMessageService.getMessageToReloadVehicleET().
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
    this.vehicleETService.getAllVehicleET(this.vehicleId, this.pageSize, this.currentPage).subscribe(response => {
      this.vehicleET = response.data;
      this.totalRecordCount = response.totalRecordCount;
    });
  }

  addOrEdit(id: number, isReadOnly: boolean) {

    const ngbModalRef = this.ngbModal.
      open(NextEmissionTestDetailsFormComponent, { centered: true, size: 'lg', backdrop: 'static' });
    ngbModalRef.componentInstance.data = { id: id, vehicleId: this.vehicleId, totalRecordCount: this.totalRecordCount, isReadOnly: isReadOnly };
  }

  delete(id: number, index: number) {
    this.vehicleETService.deleteVehicleET(id).subscribe(res => {
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
