import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';


import { FuseSharedModule } from '@fuse/shared.module';
import { FuseWidgetModule } from '@fuse/components';

import { VehicleListComponent } from './vehicle-list/vehicle-list.component';
import { VehicleDetailComponent } from './vehicle-detail/vehicle-detail.component';
import { InsuranceListComponent } from './Insurance/insurance-list/insurance-list.component';
import { InsuranceDetailComponent } from './Insurance/insurance-detail/insurance-detail.component';
import { RevenueLicenceDetailComponent } from './revenue-licence/revenue-licence-detail/revenue-licence-detail.component';
import { RevenueLicenceListComponent } from './revenue-licence/revenue-licence-list/revenue-licence-list.component';
import { GreeceNipleListComponent } from './greece-niple/greece-niple-list/greece-niple-list.component';
import { GreeceNipleDetailComponent } from './greece-niple/greece-niple-detail/greece-niple-detail.component';
import { GearBoxListComponent } from './gear-box-oil/gear-box-list/gear-box-list.component';
import { GearBoxDetailComponent } from './gear-box-oil/gear-box-detail/gear-box-detail.component';
import { FuelFilterListComponent } from './fuel-filter/fuel-filter-list/fuel-filter-list.component';
import { FuelFilterDetailComponent } from './fuel-filter/fuel-filter-detail/fuel-filter-detail.component';
import { FitnessReportListComponent } from './fitness-report/fitness-report-list/fitness-report-list.component';
import { FitnessReportDetailComponent } from './fitness-report/fitness-report-detail/fitness-report-detail.component';
import { EngineOilListComponent } from './engine-oil/engine-oil-list/engine-oil-list.component';
import { EngineOilDetailComponent } from './engine-oil/engine-oil-detail/engine-oil-detail.component';
import { EmissionTestListComponent } from './emission-test/emission-test-list/emission-test-list.component';
import { EmissionTestDetailComponent } from './emission-test/emission-test-detail/emission-test-detail.component';
import { DifferentialOilListComponent } from './differential-oil/differential-oil-list/differential-oil-list.component';
import { DifferentialOilDetailComponent } from './differential-oil/differential-oil-detail/differential-oil-detail.component';
import { AirCleanerListComponent } from './air-cleaner/air-cleaner-list/air-cleaner-list.component';
import { AirCleanerDetailComponent } from './air-cleaner/air-cleaner-detail/air-cleaner-detail.component';
import { VehicleInsuranceService } from 'app/services/vehicle/vehicle-insurance.service';
import { MaterialModule } from 'app/MaterialModule';


const routes: Routes = [
  {
    path: '',
    redirectTo: 'list',
    pathMatch: 'full'
  },
  {
    path: 'list',
    component: VehicleListComponent

  },
  {
    path: 'list/:id',
    component: VehicleDetailComponent,
    /*       resolve: {
            data: EcommerceProductService
          } */
  }
];

@NgModule({
  declarations: [VehicleListComponent, VehicleDetailComponent, InsuranceListComponent, InsuranceDetailComponent, RevenueLicenceDetailComponent, RevenueLicenceListComponent, GreeceNipleListComponent, GreeceNipleDetailComponent, GearBoxListComponent, GearBoxDetailComponent, FuelFilterListComponent, FuelFilterDetailComponent, FitnessReportListComponent, FitnessReportDetailComponent, EngineOilListComponent, EngineOilDetailComponent, EmissionTestListComponent, EmissionTestDetailComponent, DifferentialOilListComponent, DifferentialOilDetailComponent, AirCleanerListComponent, AirCleanerDetailComponent],
  imports: [
    RouterModule.forChild(routes),

    
    CommonModule,
    FuseSharedModule,
    FuseWidgetModule,
    //FuseSharedModule,
    FuseWidgetModule,
    MaterialModule
  ],
  providers: [
    VehicleInsuranceService

  ],
  entryComponents: [
    InsuranceDetailComponent
  ]
})
export class VehicleModule { }
