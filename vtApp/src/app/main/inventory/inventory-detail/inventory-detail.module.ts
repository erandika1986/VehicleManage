import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InventoryListComponent } from './inventory-list/inventory-list.component';
import { RouterModule, Routes } from '@angular/router';
import { FuseSharedModule } from '@fuse/shared.module';
import { FuseSidebarModule, FuseWidgetModule } from '@fuse/components';
import { MaterialModule } from 'app/MaterialModule';
import { SharedModule } from 'app/shared/shared.module';
import { AddInventoryComponent } from './add-inventory/add-inventory.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'list',
    pathMatch: 'full'
  },
  {
    path: 'list',
    component: InventoryListComponent

  }
];

@NgModule({
  declarations: [
    InventoryListComponent,
    AddInventoryComponent
  ],
  imports: [
    RouterModule.forChild(routes),
    CommonModule,
    FuseSharedModule,
    FuseWidgetModule,
    FuseSidebarModule,
    MaterialModule,
    SharedModule
  ]
})
export class InventoryDetailModule { }
