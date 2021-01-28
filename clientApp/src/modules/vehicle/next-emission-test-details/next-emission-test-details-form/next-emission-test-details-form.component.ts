import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { VehicleEmissionTestModel } from 'models/vehicle/vehicle-emission-test.model';
import { VehicleModel } from 'models/vehicle/vehicle.model';
import { VehicleTypeModel } from 'models/vehicle/vehicle-type.model';
import { VehicleEmissionTestService } from 'services/vehicle/vehicle-emission-test.service';
import { VehicleMessageService } from 'services/vehicle/vehicle-message.service';
import { VehicleService } from 'services/vehicle/vehicle.service';

@Component({
  selector: 'app-next-emission-test-details-form',
  templateUrl: './next-emission-test-details-form.component.html',
  styleUrls: ['./next-emission-test-details-form.component.scss']
})
export class NextEmissionTestDetailsFormComponent implements OnInit {

  recordId = 0;
  vehicleId = 0;
  totalRecordCount = 0;
  isReadOnly: boolean = false;

  isSuccess: boolean;
  message: string;

  vehicleET: VehicleEmissionTestModel;

  form: FormGroup;

  vehicle: VehicleModel;
  vehicleType: VehicleTypeModel;

  etd = 0;

  @Input() public data:any;

  constructor(private vehicleETServices: VehicleEmissionTestService,
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
    this.vehicleETServices.getVehicleETById(id).subscribe(res => {
      this.vehicleET = res;
      this.form.get('id').setValue(this.vehicleET.id);
      this.form.get('actualEmissiontTestDate').setValue(this.vehicleET.actualEmissiontTestDate);
      this.form.get('nextEmissiontTestDate').setValue(this.vehicleET.nextEmissiontTestDate);
      this.form.get('createdOn').setValue(this.vehicleET.createdOn);
      this.form.get('updatedOn').setValue(this.vehicleET.updatedOn);
      this.form.get('createdBy').setValue(this.vehicleET.createdBy);
      this.form.get('updatedBy').setValue(this.vehicleET.updatedBy);
      this.form.get('isActive').setValue(this.vehicleET.isActive);
      this.form.get('vehicleId').setValue(this.vehicleET.vehicleId);

      if (this.isReadOnly) {
        this.form.get('actualEmissiontTestDate').disable();
        this.form.get('nextEmissiontTestDate').disable();
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
      this.etd = this.vehicleType.emitionTestAge;
      this.getLatestRecord();
    });
  }

  getLatestRecord() {
    this.vehicleETServices.getLatestRecordForVehicle(this.vehicleId)
      .subscribe(response => {

        if (response.id > 0) {
          let adate = new Date(response.nextEmissiontTestDate);
          let currentDate = new Date(response.nextEmissiontTestDate);
          let ndate = new Date(currentDate.setMonth(currentDate.getMonth() + this.etd));


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

          this.form.get('actualEmissiontTestDate').setValue(actualDate);
          this.form.get('nextEmissiontTestDate').setValue(nextDate);
        }
      }, error => {

      });
  }

  onChangeNetd() {

    const getDate = this.form.controls['actualEmissiontTestDate'].value;
    const date = new Date(getDate.year, getDate.month - 1, getDate.day);
    this.form.get('nextEmissiontTestDate').setValue(this.form.get('actualEmissiontTestDate').value + this.etd);

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
      actualEmissiontTestDate: [null],
      nextEmissiontTestDate: [null],
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

    const aDate = this.form.controls['actualEmissiontTestDate'].value;
    if (aDate !== null) {
      const ad = new Date(aDate.year, aDate.month - 1, aDate.day).toLocaleDateString();
      this.form.get('actualEmissiontTestDate').setValue(ad);
    }

    const nDate = this.form.controls['nextEmissiontTestDate'].value;
    const nd = new Date(nDate.year, nDate.month - 1, nDate.day).toLocaleDateString();
    this.form.get('nextEmissiontTestDate').setValue(nd);

    if (this.form.value.id === 0) {
      this.vehicleETServices.addNewVehicleET(this.form.value).
        subscribe(res => {
          this.isSuccess = res.isSuccess;
          this.message = res.message;
          if (this.isSuccess = true) {
            this.toastrService.success(this.message);
            this.vehicleMessageService.sendMessageToReloadVehicleET(true);
            this.modal.close('Ok Close');
          } else {
            this.toastrService.error(this.message);
          }
        });
    }
  }

}
