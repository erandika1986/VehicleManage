import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'fitness-report-list',
  templateUrl: './fitness-report-list.component.html',
  styleUrls: ['./fitness-report-list.component.scss']
})
export class FitnessReportListComponent implements OnInit {

  @Input() vehicleId = 0; // decorate the property with @Input()
  
  constructor() { }

  ngOnInit(): void {
  }

}
