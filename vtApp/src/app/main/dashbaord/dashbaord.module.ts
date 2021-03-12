import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { FuseSharedModule } from '@fuse/shared.module';

const routes = [
  {
    path: '',
    redirectTo: 'sales-dashboard',
    pathMatch: 'full'
  },
  {
    path: 'inventory-dashboard',
    loadChildren: () => import('./inventory-dashboard/inventory-dashboard.module').then(m => m.InventoryDashboardModule)
  },
  {
    path: 'sales-dashboard',
    loadChildren: () => import('./sales-dashboard/sales-dashboard.module').then(m => m.SalesDashboardModule)
  },
  {
    path: 'vehicle-dashboard',
    loadChildren: () => import('./vehicle-dashboard/vehicle-dashboard.module').then(m => m.VehicleDashboardModule)
  }
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes),
    FuseSharedModule
  ]
})
export class DashbaordModule { }
