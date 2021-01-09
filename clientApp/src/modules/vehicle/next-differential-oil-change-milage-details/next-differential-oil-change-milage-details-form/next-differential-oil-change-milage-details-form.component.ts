import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormGroup } from '@angular/forms';
import { VehicleDifferentialOilChangeMilageModel } from 'models/vehicle/vehicle-differential-oil-change-milage.model';
import { VehicleModel } from 'models/vehicle/vehicle.model';
import { VehicleTypeModel } from 'models/vehicle/vehicle-type.model';
import { VehicleDifferentialOilChangeMilageService } from 'services/vehicle/vehicle-differential-oil-change-milage.service';
import { VehicleMessageService } from 'services/vehicle/vehicle-message.service';
import { VehicleService } from 'services/vehicle/vehicle.service';

@Component({
  selector: 'app-next-differential-oil-change-milage-details-form',
  templateUrl: './next-differential-oil-change-milage-details-form.component.html',
  styleUrls: ['./next-differential-oil-change-milage-details-form.component.scss']
})
export class NextDifferentialOilChangeMilageDetailsFormComponent implements OnInit {

  recordId = 0;
  totalRecordCount = 0;
  vehicleId = 0;
  isReadOnly: boolean = false;

  isSuccess: boolean;
  message: string;

  vehicleDOCM: VehicleDifferentialOilChangeMilageModel;

  form: FormGroup;

  vehicle: VehicleModel;
  vehicleType: VehicleTypeModel;

  docm = 0;

  @Input() public data;

  constructor(private vehicleDOCMService: VehicleDifferentialOilChangeMilageService,
    private formBuilder: FormBuilder,
    private toastrService: ToastrService,
    private vehicleMessageService: VehicleMessageService,
    public modal: NgbActiveModal,
    private vehicleService: VehicleService
  ) { }

  ngOnInit() {
    this.setForm();
    this.recordId = this.data.id;
    this.totalRecordCount = this.data.totalRecordCount;
    this.vehicleId = this.data.vehicleId;
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
      this.docm = this.vehicleType.differentialOilChangeMilage;
      this.getLatestRecord();
    });
  }


  getLatestRecord() {
    this.vehicleDOCMService.getLatestRecordForVehicle(this.vehicleId).subscribe(response => {
      if (response.nextDifferentialOilChangeMilage != 0) {
        this.form.get('actualDifferentialOilChangeMilage').setValue(response.nextDifferentialOilChangeMilage);
        let nextMilage = response.nextDifferentialOilChangeMilage + this.docm;
        this.form.get('nextDifferentialOilChangeMilage').setValue(nextMilage);
      }
    }, error => {
      this.toastrService.error("Unknown error has been occured. Please try again.");
    });
  }

  onChangeNDOCM(event) {
    if (event.key === "0" || event.key === "1" || event.key === "2" || event.key === "3" ||
      event.key === "4" || event.key === "5" || event.key === "6" || event.key === "7" ||
      event.key === "8" || event.key === "9" || event.key === "Backspace" || event.key === "Delete") {

      let nextMilage = this.form.get('actualDifferentialOilChangeMilage').value + this.docm;
      this.form.get('nextDifferentialOilChangeMilage').setValue(nextMilage);
    }


  }

  getById(id: number) {
    this.vehicleDOCMService.getVehicleDOCMById(id).subscribe(res => {
      this.vehicleDOCM = res;
      this.form.get('id').setValue(this.vehicleDOCM.id);
      this.form.get('actualDifferentialOilChangeMilage').setValue(this.vehicleDOCM.actualDifferentialOilChangeMilage);
      this.form.get('nextDifferentialOilChangeMilage').setValue(this.vehicleDOCM.nextDifferentialOilChangeMilage);
      this.form.get('createdOn').setValue(this.vehicleDOCM.createdOn);
      this.form.get('updatedOn').setValue(this.vehicleDOCM.updatedOn);
      this.form.get('createdBy').setValue(this.vehicleDOCM.createdBy);
      this.form.get('updatedBy').setValue(this.vehicleDOCM.updatedBy);
      this.form.get('isActive').setValue(this.vehicleDOCM.isActive);
      this.form.get('vehicleId').setValue(this.vehicleDOCM.vehicleId);

      if (this.isReadOnly) {
        this.form.get('actualDifferentialOilChangeMilage').disable();
        this.form.get('nextDifferentialOilChangeMilage').disable();
      }
    });
  }

  loadData() {
    if (this.recordId !== 0) {
      this.getById(this.recordId);
    } else {

    }
  }

  setForm() {
    this.form = this.formBuilder.group({
      id: [0],
      vehicleId: [0],
      actualDifferentialOilChangeMilage: [0],
      nextDifferentialOilChangeMilage: [0],
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
      this.vehicleDOCMService.addNewVehicleDOCM(this.form.value).
        subscribe(res => {
          this.isSuccess = res.isSuccess;
          this.message = res.message;
          if (this.isSuccess = true) {
            this.toastrService.success(this.message);
            this.vehicleMessageService.sendMessageToReloadVehicleDOCM(true);
            this.modal.close('Ok Close');
          } else {
            this.toastrService.error(this.message);
          }
        });
    }
  }

}
