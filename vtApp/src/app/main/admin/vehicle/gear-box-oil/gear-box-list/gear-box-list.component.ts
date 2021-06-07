import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'gear-box-list',
  templateUrl: './gear-box-list.component.html',
  styleUrls: ['./gear-box-list.component.scss']
})
export class GearBoxListComponent implements OnInit {

  @Input() vehicleId = 0; // decorate the property with @Input()
  
  constructor() { }

  ngOnInit(): void {
  }

}
