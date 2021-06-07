import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'air-cleaner-list',
  templateUrl: './air-cleaner-list.component.html',
  styleUrls: ['./air-cleaner-list.component.scss']
})
export class AirCleanerListComponent implements OnInit {

  @Input() vehicleId = 0; // decorate the property with @Input()
  
  constructor() { }

  ngOnInit(): void {
  }

}
