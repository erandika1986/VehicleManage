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

  date1: Date;


  constructor(private beatService: DailyBeatService) {

    this.date1 = new Date();
  }

  ngOnInit() {


  }





}
