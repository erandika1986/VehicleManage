import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { FuseSharedModule } from '@fuse/shared.module';

const routes = [
  {
    path: '',
    redirectTo: 'daily-beats',
    pathMatch: 'full'
  },
  {
    path: 'daily-beats',
    loadChildren: () => import('./daily-beat/daily-beat.module').then(m => m.DailyBeatModule)
  },
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes),
    FuseSharedModule
  ]
})
export class VehicleTrackingModule { }
