import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { FuseSharedModule } from '@fuse/shared.module';
import { FuseSidebarModule, FuseWidgetModule } from '@fuse/components';
import { SharedModule } from 'app/shared/shared.module';
import { DailyBeatListComponent } from './daily-beat-list/daily-beat-list.component';
import { DailyBeatOrderDetailComponent } from './daily-beat-order-detail/daily-beat-order-detail.component';
import { DailyBeatEditModelComponent } from './daily-beat-edit-model/daily-beat-edit-model.component';
import { DailyBeatsComponent } from './daily-beats.component';
import { MainComponent } from './sidebars/main/main.component';
import {MatToolbarModule} from '@angular/material/toolbar'; 
import { MaterialModule } from 'app/MaterialModule';
import { DailyBeatDetailComponent } from './daily-beat-detail/daily-beat-detail.component';

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
  declarations: [DailyBeatsComponent, DailyBeatListComponent, DailyBeatOrderDetailComponent, DailyBeatEditModelComponent,  MainComponent, DailyBeatDetailComponent],
  imports: [
    RouterModule.forChild(routes),
    CommonModule,
    MaterialModule,
    FuseSharedModule,
    FuseWidgetModule,
    SharedModule,    
    FuseSidebarModule,
    MatToolbarModule
  ],
  entryComponents: [
    DailyBeatEditModelComponent,DailyBeatOrderDetailComponent
]
})
export class DailyBeatModule { }
