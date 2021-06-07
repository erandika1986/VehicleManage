import { AfterViewInit, Component, Input, OnChanges, OnInit, SimpleChanges, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { fuseAnimations } from '@fuse/animations';
import { FuseProgressBarService } from '@fuse/components/progress-bar/progress-bar.service';
import { VehicleInsuranceService } from 'app/services/vehicle/vehicle-insurance.service';

@Component({
  selector: 'insurance-list',
  templateUrl: './insurance-list.component.html',
  styleUrls: ['./insurance-list.component.scss'],
  animations: fuseAnimations,
  encapsulation: ViewEncapsulation.None
})
export class InsuranceListComponent implements OnInit, AfterViewInit,OnChanges   {

  @Input() vehicleId:number = 0; // decorate the property with @Input()

  @ViewChild(MatPaginator) paginator: MatPaginator;

  @ViewChild(MatSort) sort: MatSort;

  dataSource = new MatTableDataSource([]);

  totalNumberOfRecords: number;

  displayedColumns = ['buttons','imageURL', 'nextInsuranceDate','actualInsuranceDate'];
  
  constructor(
    private _insuranceService:VehicleInsuranceService,
    private _fuseProgressBarService: FuseProgressBarService
    ) { }

  ngOnInit(): void {

  }

  ngAfterViewInit() {


  }

  ngOnChanges(changes: SimpleChanges) {

    if (changes['vehicleId']) {
      this.getVehicleInsuranceList();
    }
 }

  getVehicleInsuranceList()
  {
    this._fuseProgressBarService.show();
    this._insuranceService.getAllVehicleInsuranceDetails(this.vehicleId)
      .subscribe(response=>{
        this._fuseProgressBarService.hide();
        this.dataSource = new MatTableDataSource(response);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
        
      },error=>{
        this._fuseProgressBarService.hide();
      });
  }

  edit(item:any)
  {

  }

}
