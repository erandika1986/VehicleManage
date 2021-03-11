import { AfterViewInit, Component, ElementRef, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { fuseAnimations } from '@fuse/animations';
import { FuseProgressBarService } from '@fuse/components/progress-bar/progress-bar.service';

@Component({
  selector: 'vehicle-type-list',
  templateUrl: './vehicle-type-list.component.html',
  styleUrls: ['./vehicle-type-list.component.scss'],
  animations: fuseAnimations,
  encapsulation: ViewEncapsulation.None
})
export class VehicleTypeListComponent implements OnInit, AfterViewInit {

  totalNumberOfRecords: number;

  dataSource = new MatTableDataSource([]);

  displayedColumns = ["buttons", "id", "name", "engineOilChangeMilage", "fuelFilterChangeMilage", "gearBoxChangeMilage", "differentialOilChangeMilage", "engineOilNumber", "fuelFilterNumber", "gearBoxOilNumber", "differentialOilNumber", "airCleanerAge", "greeceNipleAge", "insuranceAge", "fitnessReportAge", "emitionTestAge", "revenueLicenceAge", "fuelTypeName"];

  @ViewChild(MatPaginator) paginator: MatPaginator;

  @ViewChild(MatSort) sort: MatSort;

  @ViewChild('input') input: ElementRef;

  constructor(
    private _route: ActivatedRoute,
    private _fuseProgressBarService: FuseProgressBarService,
    public _router: Router
  ) { }

  ngOnInit(): void {
  }

  ngAfterViewInit() {

  }

  addNewVehicleType() {

  }

  loadVehicleTypes() {

  }


  applyFilter(filterValue: string) {
    filterValue = filterValue.trim(); // Remove whitespace
    filterValue = filterValue.toLowerCase(); // Datasource defaults to lowercase matches
    this.dataSource.filter = filterValue;
  }
}
