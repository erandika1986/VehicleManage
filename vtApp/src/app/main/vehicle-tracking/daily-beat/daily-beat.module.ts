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
import { FuseSidebarModule, FuseWidgetModule } from '@fuse/components';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatMenuModule } from '@angular/material/menu';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { SharedModule } from 'app/shared/shared.module';
import { DailyBeatListComponent } from './daily-beat-list/daily-beat-list.component';
import { DailyBeatOrderDetailComponent } from './daily-beat-order-detail/daily-beat-order-detail.component';
import { DailyBeatEditModelComponent } from './daily-beat-edit-model/daily-beat-edit-model.component';
import { DailyBeatsComponent } from './daily-beats.component';
import { MainComponent } from './sidebars/main/main.component';
import {MatToolbarModule} from '@angular/material/toolbar'; 

const routes: Routes = [
  {
    path: '',
    redirectTo: 'list',
    pathMatch: 'full'
  },
  {
    path: 'list',
    component: DailyBeatsComponent

  }
];

@NgModule({
  declarations: [DailyBeatsComponent, DailyBeatListComponent, DailyBeatOrderDetailComponent, DailyBeatEditModelComponent,  MainComponent],
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
    FuseSharedModule,
    FuseWidgetModule,
    SharedModule,    
    FuseSidebarModule,
    MatToolbarModule
  ]
})
export class DailyBeatModule { }
