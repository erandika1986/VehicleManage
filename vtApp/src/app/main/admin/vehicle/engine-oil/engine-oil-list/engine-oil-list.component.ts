import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'engine-oil-list',
  templateUrl: './engine-oil-list.component.html',
  styleUrls: ['./engine-oil-list.component.scss']
})
export class EngineOilListComponent implements OnInit {

  @Input() vehicleId = 0; // decorate the property with @Input()
  
  constructor() { }

  ngOnInit(): void {
  }

}
