import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { VehicleTypesComponent } from './vehicle-types/vehicle-types.component';
import { VehiclesComponent } from './vehicles/vehicles.component';
import { VehicleDetailComponent } from './vehicle-detail/vehicle-detail.component';
import { VehicleRoutingModule } from './vehicle-routing.module';
import { NgbAlertModule, NgbCarouselModule } from '@ng-bootstrap/ng-bootstrap';
import { VehicleTypeFormComponent } from './vehicle-type-form/vehicle-type-form.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { VehicleFormComponent } from './vehicle-form/vehicle-form.component';

import { MatTabsModule } from '@angular/material/tabs';
// import { NextDifferentialOilChangeMilageDetailsComponent } from './next-differential-oil-change-milage-details/next-differential-oil-change-milage-details.component';
// import { NextDifferentialOilChangeMilageDetailsFormComponent } from './next-differential-oil-change-milage-details/next-differential-oil-change-milage-details-form/next-differential-oil-change-milage-details-form.component';
// import { NextFitnessReportDetailsComponent } from './next-fitness-report-details/next-fitness-report-details.component';
// import { NextFitnessReportDetailsFormComponent } from './next-fitness-report-details/next-fitness-report-details-form/next-fitness-report-details-form.component';
// import { NextGreeceNipleDetailsComponent } from './next-greece-niple-details/next-greece-niple-details.component';
// import { NextGreeceNipleDetailsFormComponent } from './next-greece-niple-details/next-greece-niple-details-form/next-greece-niple-details-form.component';
// import { NextInsurenceRenewalDetailsComponent } from './next-insurence-renewal-details/next-insurence-renewal-details.component';
// import { NextInsurenceRenewalDetailsFormComponent } from './next-insurence-renewal-details/next-insurence-renewal-details-form/next-insurence-renewal-details-form.component';
// import { NextRevenueLicenceDetailsComponent } from './next-revenue-licence-details/next-revenue-licence-details.component';
// import { NextRevenueLicenceDetailsFormComponent } from './next-revenue-licence-details/next-revenue-licence-details-form/next-revenue-licence-details-form.component';
// import { NextEmissionTestDetailsComponent } from './next-emission-test-details/next-emission-test-details.component';
// import { NextEmissionTestDetailsFormComponent } from './next-emission-test-details/next-emission-test-details-form/next-emission-test-details-form.component';
// import { NextAirCleanerDetailsComponent } from './next-air-cleaner-details/next-air-cleaner-details.component';
// import { NextAirCleanerDetailsFormComponent } from './next-air-cleaner-details/next-air-cleaner-details-form/next-air-cleaner-details-form.component';
// import { NextEngineOilMilageDetailsComponent } from './next-engine-oil-milage-details/next-engine-oil-milage-details.component';
// import { NextEngineOilMilageDetailsFormComponent } from './next-engine-oil-milage-details/next-engine-oil-milage-details-form/next-engine-oil-milage-details-form.component';
// import { NextFuelFilterMilageDetailsComponent } from './next-fuel-filter-milage-details/next-fuel-filter-milage-details.component';
// import { NextFuelFilterMilageDetailsFormComponent } from './next-fuel-filter-milage-details/next-fuel-filter-milage-details-form/next-fuel-filter-milage-details-form.component';
// import { NextGearBoxOilMilageDetailsComponent } from './next-gear-box-oil-milage-details/next-gear-box-oil-milage-details.component';
// import { NextGearBoxOilMilageDetailsFormComponent } from './next-gear-box-oil-milage-details/next-gear-box-oil-milage-details-form/next-gear-box-oil-milage-details-form.component';

import { NextDifferentialOilChangeMilageDetailsComponent } from './next-differential-oil-change-milage-details/next-differential-oil-change-milage-details.component';
import { NextDifferentialOilChangeMilageDetailsFormComponent } from './next-differential-oil-change-milage-details/next-differential-oil-change-milage-details-form/next-differential-oil-change-milage-details-form.component';
import { NextFitnessReportDetailsComponent } from './next-fitness-report-details/next-fitness-report-details.component';
import { NextFitnessReportDetailsFormComponent } from './next-fitness-report-details/next-fitness-report-details-form/next-fitness-report-details-form.component';
import { NextGreeceNipleDetailsComponent } from './next-greece-niple-details/next-greece-niple-details.component';
import { NextGreeceNipleDetailsFormComponent } from './next-greece-niple-details/next-greece-niple-details-form/next-greece-niple-details-form.component';
import { NextInsurenceRenewalDetailsComponent } from './next-insurence-renewal-details/next-insurence-renewal-details.component';
import { NextInsurenceRenewalDetailsFormComponent } from './next-insurence-renewal-details/next-insurence-renewal-details-form/next-insurence-renewal-details-form.component';
import { NextRevenueLicenceDetailsComponent } from './next-revenue-licence-details/next-revenue-licence-details.component';
import { NextRevenueLicenceDetailsFormComponent } from './next-revenue-licence-details/next-revenue-licence-details-form/next-revenue-licence-details-form.component';
import { NextEmissionTestDetailsComponent } from './next-emission-test-details/next-emission-test-details.component';
import { NextEmissionTestDetailsFormComponent } from './next-emission-test-details/next-emission-test-details-form/next-emission-test-details-form.component';
import { NextAirCleanerDetailsComponent } from './next-air-cleaner-details/next-air-cleaner-details.component';
import { NextAirCleanerDetailsFormComponent } from './next-air-cleaner-details/next-air-cleaner-details-form/next-air-cleaner-details-form.component';
import { NextEngineOilMilageDetailsComponent } from './next-engine-oil-milage-details/next-engine-oil-milage-details.component';
import { NextEngineOilMilageDetailsFormComponent } from './next-engine-oil-milage-details/next-engine-oil-milage-details-form/next-engine-oil-milage-details-form.component';
import { NextFuelFilterMilageDetailsComponent } from './next-fuel-filter-milage-details/next-fuel-filter-milage-details.component';
import { NextFuelFilterMilageDetailsFormComponent } from './next-fuel-filter-milage-details/next-fuel-filter-milage-details-form/next-fuel-filter-milage-details-form.component';
import { NextGearBoxOilMilageDetailsComponent } from './next-gear-box-oil-milage-details/next-gear-box-oil-milage-details.component';
import { NextGearBoxOilMilageDetailsFormComponent } from './next-gear-box-oil-milage-details/next-gear-box-oil-milage-details-form/next-gear-box-oil-milage-details-form.component';

import { MatDialogModule } from '@angular/material/dialog';
import { StatModule } from 'shared';
import { SharedModule } from 'primeng/api';






@NgModule({
  declarations: [
    VehicleTypesComponent,
    VehiclesComponent,
    VehicleDetailComponent,
    VehicleTypeFormComponent,
    VehicleFormComponent,
    NextDifferentialOilChangeMilageDetailsComponent,
    NextDifferentialOilChangeMilageDetailsFormComponent,
    NextFitnessReportDetailsComponent,
    NextFitnessReportDetailsFormComponent,
    NextGreeceNipleDetailsComponent,
    NextGreeceNipleDetailsFormComponent,
    NextInsurenceRenewalDetailsComponent,
    NextInsurenceRenewalDetailsFormComponent,
    NextRevenueLicenceDetailsComponent,
    NextRevenueLicenceDetailsFormComponent,
    NextEmissionTestDetailsComponent,
    NextEmissionTestDetailsFormComponent,
    NextAirCleanerDetailsComponent,
    NextAirCleanerDetailsFormComponent,
    NextEngineOilMilageDetailsComponent,
    NextEngineOilMilageDetailsFormComponent,
    NextFuelFilterMilageDetailsComponent,
    NextFuelFilterMilageDetailsFormComponent,
    NextGearBoxOilMilageDetailsComponent,
    NextGearBoxOilMilageDetailsFormComponent,

  ],
  imports: [
    CommonModule,
    NgbCarouselModule,
    NgbAlertModule,
    StatModule,
    VehicleRoutingModule,
    MatDialogModule,
    NgbModule,
    FormsModule,
    ReactiveFormsModule,
    MatTabsModule,
    SharedModule
  ],
  providers: [],
  entryComponents: [
    VehicleTypeFormComponent,
    NextFitnessReportDetailsFormComponent,
    NextDifferentialOilChangeMilageDetailsFormComponent,
    NextInsurenceRenewalDetailsFormComponent,
    NextGreeceNipleDetailsFormComponent,
    NextRevenueLicenceDetailsFormComponent,
    NextEmissionTestDetailsFormComponent,
    NextAirCleanerDetailsFormComponent,
    NextEngineOilMilageDetailsFormComponent,
    NextFuelFilterMilageDetailsFormComponent,
    NextGearBoxOilMilageDetailsFormComponent,

  ]
})
export class VehicleModule { }
