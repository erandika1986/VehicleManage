import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { VehicleEngineOilMilageModel } from 'models/vehicle/vehicle-engine-oil-milage.model';
import { VehicleModel } from 'models/vehicle/vehicle.model';
import { VehicleTypeModel } from 'models/vehicle/vehicle-type.model';
import { VehicleEngineOilChangeMilageService } from 'services/vehicle/vehicle-engine-oil-change-milage.service';
import { VehicleMessageService } from 'services/vehicle/vehicle-message.service';
import { VehicleService } from 'services/vehicle/vehicle.service';

@Component({
  selector: 'app-next-engine-oil-milage-details-form',
  templateUrl: './next-engine-oil-milage-details-form.component.html',
  styleUrls: ['./next-engine-oil-milage-details-form.component.scss']
})
export class NextEngineOilMilageDetailsFormComponent implements OnInit {

  recordId = 0;
  vehicleId = 0;
  totalRecordCount = 0;
  isReadOnly: boolean = false;

  isSuccess: boolean;
  message: string;

  vehicleEOM: VehicleEngineOilMilageModel;

  form: FormGroup;

  vehicle: VehicleModel;
  vehicleType: VehicleTypeModel;

  eom = 0;

  @Input() public data:any;

  constructor(private vehicleEOMService: VehicleEngineOilChangeMilageService,
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
      console.log(res);
      this.getVehicleTypeDetails(this.vehicle.vehicelType);
    });
  }

  getVehicleTypeDetails(id: number) {
    this.vehicleService.getVehicleTypeById(id).subscribe(res => {
      this.vehicleType = res;
      this.eom = this.vehicleType.engineOilChangeMilage;
      this.getLatestRecord();
    });
  }

  onChangeEOM(event:any) {

    if (event.key === "0" || event.key === "1" || event.key === "2" || event.key === "3" ||
      event.key === "4" || event.key === "5" || event.key === "6" || event.key === "7" ||
      event.key === "8" || event.key === "9" || event.key === "Backspace" || event.key === "Delete") {

      let nextMilage = this.form.get('actualOilChangeMilage').value + this.eom;
      this.form.get('nextOilChangeMilage').setValue(nextMilage);
    }
  }

  getLatestRecord() {
    this.vehicleEOMService.getLatestRecordForVehicle(this.vehicleId).subscribe(response => {
      if (response.nextOilChangeMilage != 0) {
        this.form.get('actualOilChangeMilage').setValue(response.nextOilChangeMilage);
        let nextMilage = response.nextOilChangeMilage + this.eom;
        this.form.get('nextOilChangeMilage').setValue(nextMilage);
      }
    }, error => {
      this.toastrService.error("Unknown error has been occured. Please try again.");
    });
  }

  getById(id: number) {
    this.vehicleEOMService.getVehicleEOCMById(id).subscribe(res => {
      this.vehicleEOM = res;
      this.form.get('id').setValue(this.vehicleEOM.id);
      this.form.get('actualOilChangeMilage').setValue(this.vehicleEOM.actualOilChangeMilage);
      this.form.get('nextOilChangeMilage').setValue(this.vehicleEOM.nextOilChangeMilage);
      this.form.get('createdOn').setValue(this.vehicleEOM.createdOn);
      this.form.get('updatedOn').setValue(this.vehicleEOM.updatedOn);
      this.form.get('createdBy').setValue(this.vehicleEOM.createdBy);
      this.form.get('updatedBy').setValue(this.vehicleEOM.updatedBy);
      this.form.get('isActive').setValue(this.vehicleEOM.isActive);
      this.form.get('vehicleId').setValue(this.vehicleEOM.vehicleId);

      if (this.isReadOnly) {
        this.form.get('actualOilChangeMilage').disable();
        this.form.get('nextOilChangeMilage').disable();
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
    this.form = this.formBuilder.group({
      id: [0],
      vehicleId: [0],
      actualOilChangeMilage: [null],
      nextOilChangeMilage: [null],
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
      this.vehicleEOMService.addNewVehicleEOCM(this.form.value).
        subscribe(res => {
          this.isSuccess = res.isSuccess;
          this.message = res.message;
          if (this.isSuccess = true) {
            this.toastrService.success(this.message);
            this.vehicleMessageService.sendMessageToReloadVehicleEOM(true);
            this.modal.close('Ok Close');
          } else {
            this.toastrService.error(this.message);
          }
        });
    }
  }

}
