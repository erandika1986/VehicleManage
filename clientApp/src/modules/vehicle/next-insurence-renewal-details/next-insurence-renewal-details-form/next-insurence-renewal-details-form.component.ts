import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormGroup } from '@angular/forms';
import { VehicleInsuranceModel } from 'models/vehicle/vehicle-insurance.model';
import { VehicleModel } from 'models/vehicle/vehicle.model';
import { VehicleTypeModel } from 'models/vehicle/vehicle-type.model';
import { VehicleInsuranceService } from 'services/vehicle/vehicle-insurance.service';
import { VehicleMessageService } from 'services/vehicle/vehicle-message.service';
import { VehicleService } from 'services/vehicle/vehicle.service';

@Component({
  selector: 'app-next-insurence-renewal-details-form',
  templateUrl: './next-insurence-renewal-details-form.component.html',
  styleUrls: ['./next-insurence-renewal-details-form.component.scss']
})
export class NextInsurenceRenewalDetailsFormComponent implements OnInit {

  recordId = 0;
  vehicleId = 0;
  totalRecordCount = 0;
  isReadOnly: boolean = false;

  isSuccess: boolean;
  message: string;

  vehicleI: VehicleInsuranceModel;

  form: FormGroup;

  vehicle: VehicleModel;
  vehicleType: VehicleTypeModel;

  vid = 0;

  @Input() public data:any;

  constructor(private vehicleIServices: VehicleInsuranceService,
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
    this.vehicleIServices.getVehicleIById(id).subscribe(res => {
      this.vehicleI = res;
      this.form.get('id').setValue(this.vehicleI.id);
      this.form.get('actualInsuranceDate').setValue(this.vehicleI.actualInsuranceDate);
      this.form.get('nextInsuranceDate').setValue(this.vehicleI.nextInsuranceDate);
      this.form.get('createdOn').setValue(this.vehicleI.createdOn);
      this.form.get('updatedOn').setValue(this.vehicleI.updatedOn);
      this.form.get('createdBy').setValue(this.vehicleI.createdBy);
      this.form.get('updatedBy').setValue(this.vehicleI.updatedBy);
      this.form.get('isActive').setValue(this.vehicleI.isActive);
      this.form.get('vehicleId').setValue(this.vehicleI.vehicleId);

      if (this.isReadOnly) {
        this.form.get('actualInsuranceDate').disable();
        this.form.get('nextInsuranceDate').disable();
      }
    });
  }

  getVehicleDetails() {
    this.vehicleService.getVehicleById(this.vehicleId).subscribe(res => {
      this.vehicle = res;
      this.form.get('registrationNo').setValue(this.vehicle.registrationNo);
      this.getVehicleTypeDetails(this.vehicle.vehicelType);
      this.getLatestRecord();
    });
  }

  getVehicleTypeDetails(id: number) {
    this.vehicleService.getVehicleTypeById(id).subscribe(res => {
      this.vehicleType = res;
      this.vid = this.vehicleType.insuranceAge;
    });
  }

  getLatestRecord() {
    this.vehicleIServices.getLatestRecordForVehicle(this.vehicleId)
      .subscribe(response => {

        if (response.id > 0) {
          let adate = new Date(response.nextInsuranceDate);
          let currentDate = new Date(response.nextInsuranceDate);
          let ndate = new Date(currentDate.setMonth(currentDate.getMonth() + this.vid));


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

          this.form.get('actualInsuranceDate').setValue(actualDate);
          this.form.get('nextInsuranceDate').setValue(nextDate);
        }
      }, error => {

      });
  }

  onChangeNID() {
    const getDate = this.form.controls['actualInsuranceDate'].value;
    const date = new Date(getDate.year, getDate.month - 1, getDate.day);
    this.form.get('nextInsuranceDate').setValue(this.form.get('actualInsuranceDate').value + this.vid);

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
      actualInsuranceDate: [null],
      nextInsuranceDate: [null],
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

    const aDate = this.form.controls['actualInsuranceDate'].value;
    if (aDate !== null) {
      const ad = new Date(aDate.year, aDate.month - 1, aDate.day).toLocaleDateString();
      this.form.get('actualInsuranceDate').setValue(ad);
    }

    const nDate = this.form.controls['nextInsuranceDate'].value;
    const nd = new Date(nDate.year, nDate.month - 1, nDate.day).toLocaleDateString();
    this.form.get('nextInsuranceDate').setValue(nd);

    if (this.form.value.id === 0) {
      this.vehicleIServices.addNewVehicleI(this.form.value).
        subscribe(res => {
          this.isSuccess = res.isSuccess;
          this.message = res.message;
          if (this.isSuccess = true) {
            this.toastrService.success(this.message);
            this.vehicleMessageService.sendMessageToReloadVehicleI(true);
            this.modal.close('Ok Close');
          } else {
            this.toastrService.error(this.message);
          }
        });
    }

  }

}
