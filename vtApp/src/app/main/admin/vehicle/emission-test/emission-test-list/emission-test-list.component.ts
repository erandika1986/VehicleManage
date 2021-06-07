import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'emission-test-list',
  templateUrl: './emission-test-list.component.html',
  styleUrls: ['./emission-test-list.component.scss']
})
export class EmissionTestListComponent implements OnInit {

  @Input() vehicleId = 0; // decorate the property with @Input()
  
  constructor() { }

  ngOnInit(): void {
  }

}
