import { Component, OnInit } from '@angular/core';
import { DailyBeatService } from 'src/app/services/daily-beats/daily-beat.service';
import { DropDownModel } from 'src/app/models/common/drop-down.modal';
import { VehicleBeatFilterModel } from 'src/app/models/dialy-beat/vehicle-beat-filter.model';

@Component({
  selector: 'app-beats',
  templateUrl: './beats.component.html',
  styleUrls: ['./beats.component.scss']
})
export class BeatsComponent implements OnInit {

  activeVehicles: DropDownModel[]
  selectedVehicle: DropDownModel;

  activeRoutes: DropDownModel[];
  selectedRoute: DropDownModel;

  public fromDate: any;
  public toDate: any;


  constructor(private beatService: DailyBeatService) { }

  ngOnInit() {

    let currentDate = new Date();
    this.fromDate = {
      "year": currentDate.getFullYear(),
      "month": currentDate.getMonth() + 1,
      "day": currentDate.getDate()
    }

    this.toDate = {
      "year": currentDate.getFullYear(),
      "month": currentDate.getMonth() + 1,
      "day": currentDate.getDate()
    }
    this.getMasterDate();
  }

  getMasterDate() {
    this.beatService.getMasterData().subscribe(response => {



      this.activeVehicles = response.vehicles;
      this.selectedVehicle = this.activeVehicles[0];

      console.log(this.selectedVehicle);


      this.activeRoutes = response.routes;
      this.selectedRoute = this.activeRoutes[0];

    }, error => {

    });
  }


  vehicleOnChanged(event: any) {
    this.getVehicleBeatRecords();
  }

  routeOnChanged(event: any) {
    this.getVehicleBeatRecords();
  }

  getVehicleBeatRecords() {
    let filter: VehicleBeatFilterModel = new VehicleBeatFilterModel();
    filter.SelectedRouteId = this.selectedRoute.id;
    filter.SelectedVehicleId = this.selectedVehicle.id;

    this.beatService.getAllVehicleBeatRecord(filter).subscribe(response => {


    }, error => {

    })
  }

  toDateOnChange() {

    console.log('here1');

  }

  fromDateOnChange() {
    console.log('here2');
  }
}
