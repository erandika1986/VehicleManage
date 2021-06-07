import { Component, Input, OnInit } from '@angular/core';
import { VehicleInsuranceService } from 'app/services/vehicle/vehicle-insurance.service';

@Component({
  selector: 'revenue-licence-list',
  templateUrl: './revenue-licence-list.component.html',
  styleUrls: ['./revenue-licence-list.component.scss']
})
export class RevenueLicenceListComponent implements OnInit {

  @Input() vehicleId = 0; // decorate the property with @Input()
  
  constructor() { }

  ngOnInit(): void {
  }



}
