import { Component, OnInit } from '@angular/core';
import { NextGreeceNipleDetailsFormComponent } from './next-greece-niple-details-form/next-greece-niple-details-form.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { Router, ActivatedRoute } from '@angular/router';
import { VehicleGreeceNipleModel } from 'models/vehicle/vehicle-greece-niple';
import { VehicleGreeceNipleService } from 'services/vehicle/vehicle-greece-niple.service';
import { VehicleMessageService } from 'services/vehicle/vehicle-message.service';

@Component({
  selector: 'app-next-greece-niple-details',
  templateUrl: './next-greece-niple-details.component.html',
  styleUrls: ['./next-greece-niple-details.component.scss']
})
export class NextGreeceNipleDetailsComponent implements OnInit {

  vehicleGN: VehicleGreeceNipleModel[];

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
    private vehicleGNervice: VehicleGreeceNipleService,
    private toastrService: ToastrService,
    private vehicleMessageService: VehicleMessageService,
    private router: Router, private route: ActivatedRoute) {

    this.subscription = this.vehicleMessageService.getMessageToReloadVehicleGN().
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
    this.vehicleGNervice.getAllVehicleGN(this.vehicleId, this.pageSize, this.currentPage).subscribe(response => {
      this.vehicleGN = response.data;
      this.totalRecordCount = response.totalRecordCount;
    });
  }

  onPageChange() {
    this.get();
  }

  addOrEdit(id: number, isReadOnly: boolean) {
    const ngbModalRef = this.ngbModal.
      open(NextGreeceNipleDetailsFormComponent, { centered: true, size: 'lg', backdrop: 'static' });
    ngbModalRef.componentInstance.data = { id: id, vehicleId: this.vehicleId, totalRecordCount: this.totalRecordCount, isReadOnly: isReadOnly };
  }

  delete(id: number, index: number) {
    this.vehicleGNervice.deleteVehicleGN(id).subscribe(res => {
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
