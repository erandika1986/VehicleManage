import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

const routes = [
  {
    path: '',
    redirectTo: 'purchase-order',
    pathMatch: 'full'
  },
  {
    path: 'purchase-order',
    loadChildren: () => import('./purchase-order/purchase-order.module').then(m => m.PurchaseOrderModule)
  },
  {
    path: 'inventory-detail',
    loadChildren: () => import('./inventory-detail/inventory-detail.module').then(m => m.InventoryDetailModule)
  },
  {
    path: 'product-return',
    loadChildren: () => import('./product-return/product-return.module').then(m => m.ProductReturnModule)
  }
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes),
    CommonModule
  ]
})
export class InventoryModule { }
