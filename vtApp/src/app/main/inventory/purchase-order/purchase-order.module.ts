import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PoListComponent } from './po-list/po-list.component';
import { PoDetailComponent } from './po-detail/po-detail.component';
import { RouterModule, Routes } from '@angular/router';
import { FuseSharedModule } from '@fuse/shared.module';
import { FuseSidebarModule, FuseWidgetModule } from '@fuse/components';
import { MaterialModule } from 'app/MaterialModule';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'list',
    pathMatch: 'full'
  },
  {
    path: 'list',
    component: PoListComponent

  },
  {
    path: 'list/:id',
    component: PoDetailComponent,
  }
];

@NgModule({
  declarations: [
    PoListComponent,
    PoDetailComponent
  ],
  imports: [
    RouterModule.forChild(routes),

    CommonModule,
    FuseSharedModule,
    FuseWidgetModule,
    FuseSidebarModule,
    FuseWidgetModule,
    MaterialModule
  ]
})
export class PurchaseOrderModule { }
