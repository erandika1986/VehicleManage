import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { VehicleGreeceNipleModel } from 'models/vehicle/vehicle-greece-niple';
import { VehicleModel } from 'models/vehicle/vehicle.model';
import { VehicleTypeModel } from 'models/vehicle/vehicle-type.model';
import { VehicleGreeceNipleService } from 'services/vehicle/vehicle-greece-niple.service';
import { VehicleMessageService } from 'services/vehicle/vehicle-message.service';
import { VehicleService } from 'services/vehicle/vehicle.service';

@Component({
  selector: 'app-next-greece-niple-details-form',
  templateUrl: './next-greece-niple-details-form.component.html',
  styleUrls: ['./next-greece-niple-details-form.component.scss']
})
export class NextGreeceNipleDetailsFormComponent implements OnInit {

  recordId = 0;
  vehicleId = 0;
  totalRecordCount = 0;
  isReadOnly: boolean = false;

  isSuccess: boolean;
  message: string;

  vehicleGN: VehicleGreeceNipleModel;

  form: FormGroup;

  vehicle: VehicleModel;
  vehicleType: VehicleTypeModel;

  gnd = 0;


  @Input() public data:any;

  constructor(private vehicleGNServices: VehicleGreeceNipleService,
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
    this.vehicleGNServices.getVehicleGNById(id).subscribe(res => {
      this.vehicleGN = res;
      this.form.get('id').setValue(this.vehicleGN.id);
      this.form.get('actualGreeceNipleReplaceDatee').setValue(this.vehicleGN.actualGreeceNipleReplaceDatee);
      this.form.get('nextGreeceNipleReplaceDate').setValue(this.vehicleGN.nextGreeceNipleReplaceDate);
      this.form.get('createdOn').setValue(this.vehicleGN.createdOn);
      this.form.get('updatedOn').setValue(this.vehicleGN.updatedOn);
      this.form.get('createdBy').setValue(this.vehicleGN.createdBy);
      this.form.get('updatedBy').setValue(this.vehicleGN.updatedBy);
      this.form.get('isActive').setValue(this.vehicleGN.isActive);
      this.form.get('vehicleId').setValue(this.vehicleGN.vehicleId);

      if (this.isReadOnly) {
        this.form.get('actualGreeceNipleReplaceDatee').disable();
        this.form.get('nextGreeceNipleReplaceDate').disable();
      }

    });
  }

  onChangeGND() {

    const getDate = this.form.controls['actualFitnessReportDate'].value;
    const date = new Date(getDate.year, getDate.month - 1, getDate.day);
    this.form.get('nextFitnessReportDate').setValue(this.form.get('actualFitnessReportDate').value + this.gnd);

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
      this.gnd = this.vehicleType.greeceNipleAge;
      this.getLatestRecord();
    });
  }

  onChangeNGD() {

    const getDate = this.form.controls['actualGreeceNipleReplaceDatee'].value;
    const date = new Date(getDate.year, getDate.month - 1, getDate.day);
    this.form.get('nextGreeceNipleReplaceDate').setValue(this.form.get('actualGreeceNipleReplaceDatee').value + this.gnd);
  }

  getLatestRecord() {
    this.vehicleGNServices.getLatestRecordForVehicle(this.vehicleId)
      .subscribe(response => {

        if (response.id > 0) {
          let adate = new Date(response.nextGreeceNipleReplaceDate);
          let currentDate = new Date(response.nextGreeceNipleReplaceDate);
          let ndate = new Date(currentDate.setMonth(currentDate.getMonth() + this.gnd));


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

          this.form.get('actualGreeceNipleReplaceDatee').setValue(actualDate);
          this.form.get('nextGreeceNipleReplaceDate').setValue(nextDate);
        }
      }, error => {

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
      actualGreeceNipleReplaceDatee: [null],
      nextGreeceNipleReplaceDate: [null],
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


    const aDate = this.form.controls['actualGreeceNipleReplaceDatee'].value;
    if (aDate !== null) {
      const ad = new Date(aDate.year, aDate.month - 1, aDate.day).toLocaleDateString();
      this.form.get('actualGreeceNipleReplaceDatee').setValue(ad);
    }

    const nDate = this.form.controls['nextGreeceNipleReplaceDate'].value;
    const nd = new Date(nDate.year, nDate.month - 1, nDate.day).toLocaleDateString();
    this.form.get('nextGreeceNipleReplaceDate').setValue(nd);

    if (this.form.value.id === 0) {
      this.vehicleGNServices.addNewVehicleGN(this.form.value).
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
