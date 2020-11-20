import { Component, OnInit, Input } from '@angular/core';
import { VehicleService } from 'src/app/services/vehicle/vehicle.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { VehicleMessageService } from 'src/app/services/vehicle/vehicle-message.service';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { VehicleAirCleanerModel } from 'src/app/models/vehicle/vehicle-air-cleaner.model';
import { VehicleModel } from 'src/app/models/vehicle/vehicle.model';
import { VehicleTypeModel } from 'src/app/models/vehicle/vehicle-type.model';
import { VehicleAirCleanerReplaceMilageService } from 'src/app/services/vehicle/vehicle-air-cleaner-replace-milage.service';

@Component({
  selector: 'app-next-air-cleaner-details-form',
  templateUrl: './next-air-cleaner-details-form.component.html',
  styleUrls: ['./next-air-cleaner-details-form.component.scss']
})
export class NextAirCleanerDetailsFormComponent implements OnInit {

  recordId = 0;
  vehicleId = 0;
  totalRecordCount = 0;
  isReadOnly: boolean = false;

  isSuccess: boolean;
  message: string;

  vehicleAC: VehicleAirCleanerModel;

  form: FormGroup;

  vehicle: VehicleModel;
  vehicleType: VehicleTypeModel;

  acrm = 0;

  @Input() public data;

  constructor(private vehicleACRMService: VehicleAirCleanerReplaceMilageService,
    private formBuilder: FormBuilder,
    private toastrService: ToastrService,
    private vehicleMessageService: VehicleMessageService,
    public modal: NgbActiveModal,
    private vehicleService: VehicleService
  ) { }

  ngOnInit() {
    this.setForm();
    this.recordId = this.data.id;
    this.vehicleId = this.data.vehicleId;
    this.isReadOnly = this.data.isReadOnly;
    this.totalRecordCount = this.data.totalRecordCount;
    this.loadData();
    this.getVehicleDetails();
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
      this.acrm = this.vehicleType.airCleanerAge;
      this.getLatestRecord();
    });
  }

  onChangeACRM(event) {

    if (event.key === "0" || event.key === "1" || event.key === "2" || event.key === "3" ||
      event.key === "4" || event.key === "5" || event.key === "6" || event.key === "7" ||
      event.key === "8" || event.key === "9" || event.key === "Backspace" || event.key === "Delete") {

      let nextMilage = this.form.get('actualAirCleanerReplaceMilage').value + this.acrm;
      this.form.get('nextAirCleanerReplaceMilage').setValue(nextMilage);
    }
  }

  getLatestRecord() {
    this.vehicleACRMService.getLatestRecordForVehicle(this.vehicleId).subscribe(response => {
      if (response.nextAirCleanerReplaceMilage != 0) {
        this.form.get('actualAirCleanerReplaceMilage').setValue(response.nextAirCleanerReplaceMilage);
        let nextMilage = response.nextAirCleanerReplaceMilage + this.acrm;
        this.form.get('nextAirCleanerReplaceMilage').setValue(nextMilage);
      }
    }, error => {
      this.toastrService.error("Unknown error has been occured. Please try again.");
    });
  }

  getById(id: number) {
    this.vehicleACRMService.getVehicleACRMById(id).subscribe(res => {
      this.vehicleAC = res;
      this.form.get('id').setValue(this.vehicleAC.id);
      this.form.get('actualAirCleanerReplaceMilage').setValue(this.vehicleAC.actualAirCleanerReplaceMilage);
      this.form.get('nextAirCleanerReplaceMilage').setValue(this.vehicleAC.nextAirCleanerReplaceMilage);
      this.form.get('createdOn').setValue(this.vehicleAC.createdOn);
      this.form.get('updatedOn').setValue(this.vehicleAC.updatedOn);
      this.form.get('createdBy').setValue(this.vehicleAC.createdBy);
      this.form.get('updatedBy').setValue(this.vehicleAC.updatedBy);
      this.form.get('isActive').setValue(this.vehicleAC.isActive);
      this.form.get('vehicleId').setValue(this.vehicleAC.vehicleId);

      if (this.isReadOnly) {
        this.form.get('actualAirCleanerReplaceMilage').disable();
        this.form.get('nextAirCleanerReplaceMilage').disable();
      }

    });
  }

  loadData() {
    console.log('loadData');
    if (this.recordId === 0) {
      this.setForm();
    } else {
      this.getById(this.recordId);
    }
  }

  setForm() {
    console.log('setForm');
    this.form = this.formBuilder.group({
      id: [0],
      vehicleId: [0],
      actualAirCleanerReplaceMilage: [null],
      nextAirCleanerReplaceMilage: [null],
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
    if (this.form.value.id === 0) {
      this.vehicleACRMService.addNewVehicleACRM(this.form.value).
        subscribe(res => {
          this.isSuccess = res.isSuccess;
          this.message = res.message;
          if (this.isSuccess = true) {
            this.toastrService.success(this.message);
            this.vehicleMessageService.sendMessageToReloadVehicleAC(true);
            this.modal.close('Ok Close');
          } else {
            this.toastrService.error(this.message);
          }
        }, error => {

        });
    }
  }

}
