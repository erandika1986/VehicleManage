import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'greece-niple-list',
  templateUrl: './greece-niple-list.component.html',
  styleUrls: ['./greece-niple-list.component.scss']
})
export class GreeceNipleListComponent implements OnInit {

  @Input() vehicleId = 0; // decorate the property with @Input()
  
  constructor() { }

  ngOnInit(): void {
  }

}
