import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'differential-oil-list',
  templateUrl: './differential-oil-list.component.html',
  styleUrls: ['./differential-oil-list.component.scss']
})
export class DifferentialOilListComponent implements OnInit {

  @Input() vehicleId = 0; // decorate the property with @Input()
  
  constructor() { }

  ngOnInit(): void {
  }

}
