import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'fuel-filter-list',
  templateUrl: './fuel-filter-list.component.html',
  styleUrls: ['./fuel-filter-list.component.scss']
})
export class FuelFilterListComponent implements OnInit {

  @Input() vehicleId = 0; // decorate the property with @Input()
  
  constructor() { }

  ngOnInit(): void {
  }

}
