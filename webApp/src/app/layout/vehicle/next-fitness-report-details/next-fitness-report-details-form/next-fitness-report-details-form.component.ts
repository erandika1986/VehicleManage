import { Component, OnInit, Input } from '@angular/core';
import { VehicleMessageService } from 'src/app/services/vehicle/vehicle-message.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { NgbActiveModal, NgbDate } from '@ng-bootstrap/ng-bootstrap';
import { VehicleFitnessReportModel } from 'src/app/models/vehicle/vehicle-fitness-report.model';
import { VehicleFitnessReportService } from 'src/app/services/vehicle/vehicle-fitness-report.service';
import { VehicleService } from 'src/app/services/vehicle/vehicle.service';
import { VehicleModel } from 'src/app/models/vehicle/vehicle.model';
import { VehicleTypeModel } from 'src/app/models/vehicle/vehicle-type.model';

@Component({
  selector: 'app-next-fitness-report-details-form',
  templateUrl: './next-fitness-report-details-form.component.html',
  styleUrls: ['./next-fitness-report-details-form.component.scss']
})
export class NextFitnessReportDetailsFormComponent implements OnInit {

  recordId = 0;
  vehicleId = 0;
  totalRecordCount = 0;
  isReadOnly = false;

  isSuccess: boolean;
  message: string;

  vehicleFR: VehicleFitnessReportModel;

  form: FormGroup;

  vehicle: VehicleModel;
  vehicleType: VehicleTypeModel;

  frd = 0;

  @Input() public data;

  constructor(private vehicleFRServices: VehicleFitnessReportService,
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
    this.vehicleFRServices.getVehicleFRById(id).subscribe(res => {
      this.vehicleFR = res;

      const adate = new Date(this.vehicleFR.actualFitnessReportDate);
      const ndate = new Date(this.vehicleFR.nextFitnessReportDate);

      const actualDate = {
        'year': adate.getFullYear(),
        'month': adate.getMonth() + 1,
        'day': adate.getDate()
      }

      const nextDate = {
        'year': ndate.getFullYear(),
        'month': ndate.getMonth() + 1,
        'day': ndate.getDate()
      }
      this.form.get('id').setValue(this.vehicleFR.id);
      this.form.get('actualFitnessReportDate').setValue(actualDate);
      this.form.get('nextFitnessReportDate').setValue(nextDate);
      this.form.get('createdOn').setValue(this.vehicleFR.createdOn);
      this.form.get('updatedOn').setValue(this.vehicleFR.updatedOn);
      this.form.get('createdBy').setValue(this.vehicleFR.createdBy);
      this.form.get('updatedBy').setValue(this.vehicleFR.updatedBy);
      this.form.get('isActive').setValue(this.vehicleFR.isActive);
      this.form.get('vehicleId').setValue(this.vehicleFR.vehicleId);

      if (this.isReadOnly) {
        this.form.get('actualFitnessReportDate').disable();
        this.form.get('nextFitnessReportDate').disable();
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
      this.frd = this.vehicleType.fitnessReportAge;
      this.getLatestRecord();
    });
  }

  getLatestRecord() {
    this.vehicleFRServices.getLatestRecordForVehicle(this.vehicleId)
      .subscribe(response => {

        if (response.id > 0) {
          const adate = new Date(response.nextFitnessReportDate);
          const currentDate = new Date(response.nextFitnessReportDate);
          const ndate = new Date(currentDate.setMonth(currentDate.getMonth() + this.frd));


          const actualDate = {
            'year': adate.getFullYear(),
            'month': adate.getMonth() + 1,
            'day': adate.getDate()
          };

          const nextDate = {
            'year': ndate.getFullYear(),
            'month': ndate.getMonth() + 1,
            'day': ndate.getDate()
          };

          this.form.get('actualFitnessReportDate').setValue(actualDate);
          this.form.get('nextFitnessReportDate').setValue(nextDate);
        }
      }, error => {

      });
  }

  onChangeNFRD() {

    const getDate = this.form.controls['actualFitnessReportDate'].value;
    const date = new Date(getDate.year, getDate.month - 1, getDate.day);
    this.form.get('nextFitnessReportDate').setValue(this.form.get('actualFitnessReportDate').value + this.frd);

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
      actualFitnessReportDate: [null],
      nextFitnessReportDate: [null],
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

    const aDate = this.form.controls['actualFitnessReportDate'].value;
    if (aDate !== null) {
      const ad = new Date(aDate.year, aDate.month - 1, aDate.day).toLocaleDateString();
      this.form.get('actualFitnessReportDate').setValue(ad);
    }

    const nDate = this.form.controls['nextFitnessReportDate'].value;
    const nd = new Date(nDate.year, nDate.month - 1, nDate.day).toLocaleDateString();

    this.form.get('nextFitnessReportDate').setValue(nd);
    if (this.form.value.id === 0) {
      this.vehicleFRServices.addNewVehicleFR(this.form.value).
        subscribe(res => {
          this.isSuccess = res.isSuccess;
          this.message = res.message;
          if (this.isSuccess = true) {
            this.toastrService.success(this.message);
            this.vehicleMessageService.sendMessageToReloadVehicleFR(true);
            this.modal.close('Ok Close');
          } else {
            this.toastrService.error(this.message);
          }
        });
    }

  }
}
