import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { VehicleGearBoxOilMilageModel } from 'models/vehicle/vehicle-gear-box-oil-milage.model';
import { VehicleModel } from 'models/vehicle/vehicle.model';
import { VehicleTypeModel } from 'models/vehicle/vehicle-type.model';
import { VehicleGearBoxOilChangeMilageService } from 'services/vehicle/vehicle-gear-box-oil-change-milage.service';
import { VehicleMessageService } from 'services/vehicle/vehicle-message.service';
import { VehicleService } from 'services/vehicle/vehicle.service';

@Component({
  selector: 'app-next-gear-box-oil-milage-details-form',
  templateUrl: './next-gear-box-oil-milage-details-form.component.html',
  styleUrls: ['./next-gear-box-oil-milage-details-form.component.scss']
})
export class NextGearBoxOilMilageDetailsFormComponent implements OnInit {

  recordId = 0;
  vehicleId = 0;
  totalRecordCount = 0;
  isReadOnly: boolean = false;

  isSuccess: boolean;
  message: string;

  vehicleGOCM: VehicleGearBoxOilMilageModel;

  form: FormGroup;

  vehicle: VehicleModel;
  vehicleType: VehicleTypeModel;

  gocm = 0;

  @Input() public data:any;

  constructor(private vehicleGOCMService: VehicleGearBoxOilChangeMilageService,
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
      this.gocm = this.vehicleType.gearBoxChangeMilage;
      this.getLatestRecord();
    });
  }

  getLatestRecord() {
    this.vehicleGOCMService.getLatestRecordForVehicle(this.vehicleId).subscribe(response => {
      if (response.nextGearBoxOilChangeMilage != 0) {
        this.form.get('actualGearBoxOilChangeMilage').setValue(response.nextGearBoxOilChangeMilage);
        let nextMilage = response.nextGearBoxOilChangeMilage + this.gocm;
        this.form.get('nextGearBoxOilChangeMilage').setValue(nextMilage);
      }
    }, error => {
      this.toastrService.error("Unknown error has been occured. Please try again.");
    });
  }

  onChangeGBOM(event:any) {

    if (event.key === "0" || event.key === "1" || event.key === "2" || event.key === "3" ||
      event.key === "4" || event.key === "5" || event.key === "6" || event.key === "7" ||
      event.key === "8" || event.key === "9" || event.key === "Backspace" || event.key === "Delete") {

      let nextMilage = this.form.get('actualGearBoxOilChangeMilage').value + this.gocm;
      this.form.get('nextGearBoxOilChangeMilage').setValue(nextMilage);
    }
  }



  getById(id: number) {
    this.vehicleGOCMService.getVehicleRLGBOCMId(id).subscribe(res => {
      this.vehicleGOCM = res;
      this.form.get('id').setValue(this.vehicleGOCM.id);
      this.form.get('actualGearBoxOilChangeMilage').setValue(this.vehicleGOCM.actualGearBoxOilChangeMilage);
      this.form.get('nextGearBoxOilChangeMilage').setValue(this.vehicleGOCM.nextGearBoxOilChangeMilage);
      this.form.get('createdOn').setValue(this.vehicleGOCM.createdOn);
      this.form.get('updatedOn').setValue(this.vehicleGOCM.updatedOn);
      this.form.get('createdBy').setValue(this.vehicleGOCM.createdBy);
      this.form.get('updatedBy').setValue(this.vehicleGOCM.updatedBy);
      this.form.get('isActive').setValue(this.vehicleGOCM.isActive);
      this.form.get('vehicleId').setValue(this.vehicleGOCM.vehicleId);

      if (this.isReadOnly) {
        this.form.get('actualGearBoxOilChangeMilage').disable();
        this.form.get('nextGearBoxOilChangeMilage').disable();
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
      actualGearBoxOilChangeMilage: [null],
      nextGearBoxOilChangeMilage: [null],
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
      this.vehicleGOCMService.addNewVehicleGBOCM(this.form.value).
        subscribe(res => {
          this.isSuccess = res.isSuccess;
          this.message = res.message;
          if (this.isSuccess = true) {
            this.toastrService.success(this.message);
            this.vehicleMessageService.sendMessageToReloadVehicleGBOM(true);
            this.modal.close('Ok Close');
          } else {
            this.toastrService.error(this.message);
          }
        });
    }
  }

}
