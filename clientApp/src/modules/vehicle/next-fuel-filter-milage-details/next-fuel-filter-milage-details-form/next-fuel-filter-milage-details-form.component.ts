import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { VehicleService } from 'src/app/services/vehicle/vehicle.service';
import { ToastrService } from 'ngx-toastr';
import { VehicleMessageService } from 'src/app/services/vehicle/vehicle-message.service';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { VehicleModel } from 'src/app/models/vehicle/vehicle.model';
import { VehicleTypeModel } from 'src/app/models/vehicle/vehicle-type.model';
import { VehicleFuelFilterMilageModel } from 'src/app/models/vehicle/vehicle-fuel-filter-milage.model';
import { VehicleFuelFilterChangeMilageService } from 'src/app/services/vehicle/vehicle-fuel-filter-change-milage.service';

@Component({
  selector: 'app-next-fuel-filter-milage-details-form',
  templateUrl: './next-fuel-filter-milage-details-form.component.html',
  styleUrls: ['./next-fuel-filter-milage-details-form.component.scss']
})
export class NextFuelFilterMilageDetailsFormComponent implements OnInit {

  recordId = 0;
  vehicleId = 0;
  totalRecordCount = 0;
  isReadOnly: boolean = false;

  isSuccess: boolean;
  message: string;

  vehicleFFM: VehicleFuelFilterMilageModel;

  form: FormGroup;

  vehicle: VehicleModel;
  vehicleType: VehicleTypeModel;

  ffm = 0;

  @Input() public data;

  constructor(private vehicleFFMService: VehicleFuelFilterChangeMilageService,
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
    this.totalRecordCount = this.data.totalRecordCount;
    this.isReadOnly = this.data.isReadOnly;
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
      this.ffm = this.vehicleType.fuelFilterChangeMilage;
      this.getLatestRecord();
    });
  }

  getLatestRecord() {
    this.vehicleFFMService.getLatestRecordForVehicle(this.vehicleId).subscribe(response => {
      if (response.nextFuelFilterChangeMilage != 0) {
        this.form.get('actualFuelFilterChangeMilage').setValue(response.nextFuelFilterChangeMilage);
        let nextMilage = response.nextFuelFilterChangeMilage + this.ffm;
        this.form.get('nextFuelFilterChangeMilage').setValue(nextMilage);
      }
    }, error => {
      this.toastrService.error("Unknown error has been occured. Please try again.");
    });
  }

  onChangeFFM(event) {

    if (event.key === "0" || event.key === "1" || event.key === "2" || event.key === "3" ||
      event.key === "4" || event.key === "5" || event.key === "6" || event.key === "7" ||
      event.key === "8" || event.key === "9" || event.key === "Backspace" || event.key === "Delete") {

      let nextMilage = this.form.get('actualFuelFilterChangeMilage').value + this.ffm;
      this.form.get('nextFuelFilterChangeMilage').setValue(nextMilage);
    }
  }

  getById(id: number) {
    this.vehicleFFMService.getVehicleFFMById(id).subscribe(res => {
      this.vehicleFFM = res;
      this.form.get('id').setValue(this.vehicleFFM.id);
      this.form.get('actualFuelFilterChangeMilage').setValue(this.vehicleFFM.actualFuelFilterChangeMilage);
      this.form.get('nextFuelFilterChangeMilage').setValue(this.vehicleFFM.nextFuelFilterChangeMilage);
      this.form.get('createdOn').setValue(this.vehicleFFM.createdOn);
      this.form.get('updatedOn').setValue(this.vehicleFFM.updatedOn);
      this.form.get('createdBy').setValue(this.vehicleFFM.createdBy);
      this.form.get('updatedBy').setValue(this.vehicleFFM.updatedBy);
      this.form.get('isActive').setValue(this.vehicleFFM.isActive);
      this.form.get('vehicleId').setValue(this.vehicleFFM.vehicleId);

      if (this.isReadOnly) {
        this.form.get('actualFuelFilterChangeMilage').disable();
        this.form.get('nextFuelFilterChangeMilage').disable();
      }
    });
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
      actualFuelFilterChangeMilage: [null],
      nextFuelFilterChangeMilage: [null],
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
      this.vehicleFFMService.addNewVehicleFFM(this.form.value).
        subscribe(res => {
          this.isSuccess = res.isSuccess;
          this.message = res.message;
          if (this.isSuccess = true) {
            this.toastrService.success(this.message);
            this.vehicleMessageService.sendMessageToReloadVehicleFFM(true);
            this.modal.close('Ok Close');
          } else {
            this.toastrService.error(this.message);
          }
        });
    }
  }

}
