import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { VehicleService } from 'src/app/services/vehicle/vehicle.service';
import { ToastrService } from 'ngx-toastr';
import { VehicleMessageService } from 'src/app/services/vehicle/vehicle-message.service';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { VehicleTypeModel } from 'src/app/models/vehicle/vehicle-type.model';
import { VehicleModel } from 'src/app/models/vehicle/vehicle.model';
import { VehicleRevenueLicenceModel } from 'src/app/models/vehicle/vehicle-revenue-licence.model';
import { VehicleRevenueLicenceService } from 'src/app/services/vehicle/vehicle-revenue-licence.service';

@Component({
  selector: 'app-next-revenue-licence-details-form',
  templateUrl: './next-revenue-licence-details-form.component.html',
  styleUrls: ['./next-revenue-licence-details-form.component.scss']
})
export class NextRevenueLicenceDetailsFormComponent implements OnInit {
  recordId = 0;
  vehicleId = 0;
  totalRecordCount = 0;
  isReadOnly: boolean = false;

  isSuccess: boolean;
  message: string;

  vehicleRL: VehicleRevenueLicenceModel;

  form: FormGroup;

  vehicle: VehicleModel;
  vehicleType: VehicleTypeModel;

  rld = 0;

  @Input() public data;

  constructor(private vehicleRLServices: VehicleRevenueLicenceService,
    private formBuilder: FormBuilder,
    private toastrService: ToastrService,
    private vehicleMessageService: VehicleMessageService,
    public modal: NgbActiveModal,
    private vehicleService: VehicleService
  ) { }

  ngOnInit() {
    this.setForm();
    this.vehicleId = this.data.vehicleId;
    this.recordId = this.data.id;
    this.totalRecordCount = this.data.totalRecordCount;
    this.isReadOnly = this.data.isReadOnly;
    this.loadData();
    this.getVehicleDetails();
  }

  getById(id: number) {
    this.vehicleRLServices.getVehicleRLById(id).subscribe(res => {
      this.vehicleRL = res;
      this.form.get('id').setValue(this.vehicleRL.id);
      this.form.get('actualRevenueLicenceDate').setValue(this.vehicleRL.actualRevenueLicenceDate);
      this.form.get('nextRevenueLicenceDate').setValue(this.vehicleRL.nextRevenueLicenceDate);
      this.form.get('createdOn').setValue(this.vehicleRL.createdOn);
      this.form.get('updatedOn').setValue(this.vehicleRL.updatedOn);
      this.form.get('createdBy').setValue(this.vehicleRL.createdBy);
      this.form.get('updatedBy').setValue(this.vehicleRL.updatedBy);
      this.form.get('isActive').setValue(this.vehicleRL.isActive);
      this.form.get('vehicleId').setValue(this.vehicleRL.vehicleId);

      if (this.isReadOnly) {
        this.form.get('actualRevenueLicenceDate').disable();
        this.form.get('nextRevenueLicenceDate').disable();
      }
    });
  }

  getVehicleDetails() {
    this.vehicleService.getVehicleById(this.vehicleId).subscribe(res => {
      this.vehicle = res;
      this.form.get('registrationNo').setValue(this.vehicle.registrationNo);
      this.getVehicleTypeDetails(this.vehicle.vehicelType);
    });
  }

  getVehicleTypeDetails(id: number) {
    this.vehicleService.getVehicleTypeById(id).subscribe(res => {
      this.vehicleType = res;
      this.rld = this.vehicleType.revenueLicenceAge;
      this.getLatestRecord();
    });
  }

  getLatestRecord() {
    this.vehicleRLServices.getLatestRecordForVehicle(this.vehicleId)
      .subscribe(response => {

        if (response.id > 0) {
          let adate = new Date(response.nextRevenueLicenceDate);
          let currentDate = new Date(response.nextRevenueLicenceDate);
          let ndate = new Date(currentDate.setMonth(currentDate.getMonth() + this.rld));


          let actualDate = {
            "year": adate.getFullYear(),
            "month": adate.getMonth() + 1,
            "day": adate.getDate()
          }

          let nextDate = {
            "year": ndate.getFullYear(),
            "month": ndate.getMonth() + 1,
            "day": ndate.getDate()
          }

          this.form.get('actualRevenueLicenceDate').setValue(actualDate);
          this.form.get('nextRevenueLicenceDate').setValue(nextDate);
        }
      }, error => {

      });
  }

  onChangeNRLD() {
    const getDate = this.form.controls['actualRevenueLicenceDate'].value;
    const date = new Date(getDate.year, getDate.month - 1, getDate.day);
    this.form.get('nextRevenueLicenceDate').setValue(this.form.get('actualRevenueLicenceDate').value + this.rld);

  }

  loadData() {
    if (this.recordId === 0) {
      this.setForm();
    } else {
      this.getById(this.recordId);
    }
  }

  setForm() {

    this.form = this.formBuilder.group({
      id: [0],
      vehicleId: [0],
      actualRevenueLicenceDate: [null],
      nextRevenueLicenceDate: [null],
      createdBy: [0],
      updatedBy: [0],
      createdOn: [null],
      updatedOn: [null],
      isActive: [true],
      registrationNo: ['']
    });
  }

  onSubmit() {

    this.form.get('vehicleId').setValue(this.vehicleId);

    const aDate = this.form.controls['actualRevenueLicenceDate'].value;
    if (aDate !== null) {
      const ad = new Date(aDate.year, aDate.month - 1, aDate.day).toLocaleDateString();
      this.form.get('actualRevenueLicenceDate').setValue(ad);
    }

    const nDate = this.form.controls['nextRevenueLicenceDate'].value;
    const nd = new Date(nDate.year, nDate.month - 1, nDate.day).toLocaleDateString();
    this.form.get('nextRevenueLicenceDate').setValue(nd);

    if (this.form.value.id === 0) {
      this.vehicleRLServices.addNewVehicleRL(this.form.value).
        subscribe(res => {
          this.isSuccess = res.isSuccess;
          this.message = res.message;
          if (this.isSuccess = true) {
            this.toastrService.success(this.message);
            this.vehicleMessageService.sendMessageToReloadVehicleRL(true);
            this.modal.close('Ok Close');
          } else {
            this.toastrService.error(this.message);
          }
        });
    }
  }

}
