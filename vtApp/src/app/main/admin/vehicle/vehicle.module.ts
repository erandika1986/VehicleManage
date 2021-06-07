import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

import { MatButtonModule } from '@angular/material/button';
import { MatChipsModule } from '@angular/material/chips';
import { MatRippleModule } from '@angular/material/core';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSelectModule } from '@angular/material/select';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { MatTabsModule } from '@angular/material/tabs';
import { FuseSharedModule } from '@fuse/shared.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FuseWidgetModule } from '@fuse/components';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatMenuModule } from '@angular/material/menu';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { SharedModule } from 'app/shared/shared.module';
import { VehicleListComponent } from './vehicle-list/vehicle-list.component';
import { VehicleDetailComponent } from './vehicle-detail/vehicle-detail.component';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatToolbarModule } from '@angular/material/toolbar';
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

    MatButtonModule,
    MatChipsModule,
    MatExpansionModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    MatPaginatorModule,
    MatRippleModule,
    MatSelectModule,
    MatSortModule,
    MatSnackBarModule,
    MatTableModule,
    MatTooltipModule,
    MatTabsModule,
    CommonModule,
    MatDatepickerModule,
    FuseSharedModule,
    FuseWidgetModule,
    MatMenuModule,
    MatToolbarModule,
    MatSlideToggleModule,
    SharedModule
  ]
})
export class VehicleModule { }
