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
import { DifferentialOilListComponent } from './differential-oil-list/differential-oil-list.component';
import { DifferentialOilDetailComponent } from './differential-oil-detail/differential-oil-detail.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'list',
    pathMatch: 'full'
  },
  {
    path: 'list',
    component: DifferentialOilListComponent

  },
  {
    path: 'list/:id',
    component: DifferentialOilDetailComponent,
    /*       resolve: {
            data: EcommerceProductService
          } */
  }
];

@NgModule({
  declarations: [DifferentialOilListComponent, DifferentialOilDetailComponent],
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
    MatTabsModule,
    CommonModule,
    MatDatepickerModule,
    FuseSharedModule,
    FuseWidgetModule,
    MatMenuModule,
    MatSlideToggleModule,
    SharedModule
  ]
})
export class DifferentialOilModule { }