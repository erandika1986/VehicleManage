import { Component, OnInit } from '@angular/core';
import { DailyBeatService } from 'src/app/services/daily-beats/daily-beat.service';
import { DropDownModel } from 'src/app/models/common/drop-down.modal';
import { VehicleBeatFilterModel } from 'src/app/models/dialy-beat/vehicle-beat-filter.model';
import { DailyVehicleBeatModel } from 'src/app/models/dialy-beat/daily-vehicle-beat.model';
import { PrimeNGConfig } from 'primeng/api';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationService } from 'primeng/api';
import { Message } from 'primeng/api';

@Component({
  selector: 'app-beats',
  templateUrl: './beats.component.html',
  styleUrls: ['./beats.component.scss'],
  providers: [ConfirmationService]
})
export class BeatsComponent implements OnInit {

  selectedData: Date;
  beats: DailyVehicleBeatModel[];
  displayModal: boolean;

  dailyBeatForm: FormGroup;
  selectDailyBeat: DailyVehicleBeatModel;

  vehicles: DropDownModel[];
  routes: DropDownModel[];
  statuses: DropDownModel[];

  msgs: Message[] = [];

  constructor(private beatService: DailyBeatService,
    private primengConfig: PrimeNGConfig,
    private confirmationService: ConfirmationService,
    private formBuilder: FormBuilder,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService) {
    this.beats = [];
    this.vehicles = [];
    this.routes = [];
    this.statuses = [];

    this.selectedData = new Date();
    this.selectDailyBeat = new DailyVehicleBeatModel();
  }

  ngOnInit() {
    this.primengConfig.ripple = true;


    this.dailyBeatForm = this.formBuilder.group({
      id: [0],
      date: [this.selectedData, Validators.compose([Validators.required])],
      endMilage: [0],
      isActive: [true],
      routeId: [0],
      startingMilage: [0],
      status: [0],
      vehicleId: [0],
    });

    this.getMasterData();
  }

  getMasterData() {

    this.spinner.show();
    this.beatService.getMasterData()
      .subscribe(response => {

        this.vehicles = response.vehicles;
        this.routes = response.routes;
        this.statuses = response.status;

        this.getVehicleBeatDetailForSelectedDate();

      }, error => {
        this.spinner.hide();
      });
  }

  getVehicleBeatDetailForSelectedDate() {

    let filter: VehicleBeatFilterModel = new VehicleBeatFilterModel();
    filter.date = this.selectedData.getFullYear() + '-' + (this.selectedData.getMonth() + 1) + '-' + this.selectedData.getDate();
    //let dateStrToSend = this.selectedData.getFullYear() + '-' + (this.selectedData.getMonth() + 1) + '-' + this.selectedData.getDate();

    this.beatService.getAllVehicleBeatRecord(filter)
      .subscribe(response => {

        this.beats = response;
        this.spinner.hide();

      }, error => {
        this.spinner.hide();
      });
  }



  getDailyBeatById(id: number) {
    this.spinner.show();
    this.beatService.getVehicleBeatRecordById(id)
      .subscribe(response => {

        this.displayModal = true;
        this.selectDailyBeat = response;
        this.spinner.hide();
      }, error => {
        this.spinner.hide();
      });
  }

  dateFilterChanged() {

    this.spinner.show();
    this.getVehicleBeatDetailForSelectedDate();
  }

  addNew() {
    this.displayModal = true;
    this.selectDailyBeat = new DailyVehicleBeatModel();
    this.selectDailyBeat.id = 0;
    this.selectDailyBeat.date = this.selectedData;
    this.selectDailyBeat.endMilage = 0;
    this.selectDailyBeat.isActive = true;
    this.selectDailyBeat.routeId = this.routes[0].id;
    this.selectDailyBeat.startingMilage = 0;
    this.selectDailyBeat.status = this.statuses[0].id;
    this.selectDailyBeat.vehicleId = this.vehicles[0].id;

    //this.dailyBeatForm.get('id').setValue(response.id);
    this.dailyBeatForm.get('date').setValue(this.selectedData);
    this.dailyBeatForm.get('endMilage').setValue(this.selectDailyBeat.endMilage);
    this.dailyBeatForm.get('isActive').setValue(true);
    this.dailyBeatForm.get('routeId').setValue(this.selectDailyBeat.routeId);
    this.dailyBeatForm.get('startingMilage').setValue(this.selectDailyBeat.startingMilage);
    this.dailyBeatForm.get('status').setValue(this.selectDailyBeat.status);
    this.dailyBeatForm.get('vehicleId').setValue(this.selectDailyBeat.vehicleId);
  }

  delete(item: DailyVehicleBeatModel) {

    console.log(item);

    this.confirmationService.confirm({
      message: 'Do you want to delete this record?',
      header: 'Delete Confirmation',
      icon: 'pi pi-info-circle',
      accept: () => {
        this.spinner.show();
        this.beatService.delete(item.id)
          .subscribe(response => {
            this.spinner.hide();
            if (response.isSuccess) {
              this.toastr.success(response.message, "Success");
              this.getVehicleBeatDetailForSelectedDate();
            }
            else {
              this.toastr.error(response.message, "Error");
            }
          }, error => {
            this.spinner.hide();
          });
      },
      reject: () => {

      }
    });
  }

  edit(item: DailyVehicleBeatModel) {

    console.log(item);

    this.displayModal = true;

    this.selectDailyBeat = item;

    this.dailyBeatForm.get('id').setValue(item.id);
    this.dailyBeatForm.get('date').setValue(new Date(this.selectDailyBeat.date));
    this.dailyBeatForm.get('endMilage').setValue(item.endMilage);
    this.dailyBeatForm.get('isActive').setValue(this.selectDailyBeat.isActive);
    this.dailyBeatForm.get('routeId').setValue(this.selectDailyBeat.routeId);
    this.dailyBeatForm.get('startingMilage').setValue(item.startingMilage);
    this.dailyBeatForm.get('status').setValue(this.selectDailyBeat.status);
    this.dailyBeatForm.get('vehicleId').setValue(this.selectDailyBeat.vehicleId);
  }

  saveDailyBeat() {
    this.spinner.show();
    this.beatService.saveDailyVehicleBeatRecord(this.dailyBeatForm.getRawValue())
      .subscribe(response => {
        this.spinner.hide();
        this.displayModal = false;
        this.getVehicleBeatDetailForSelectedDate();
        if (response.isSuccess) {
          this.toastr.success(response.message, "Success");
        }
        else {
          this.toastr.error(response.message, "Error");
        }
      }, error => {
        this.spinner.hide();
      })
  }


}
