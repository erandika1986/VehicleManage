import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { FuseSharedModule } from '@fuse/shared.module';


const routes = [
  {
    path: '',
    redirectTo: 'vehicle-types',
    pathMatch: 'full'
  },
  {
    path: 'vehicle-types',
    loadChildren: () => import('./vehicle-types/vehicle-types.module').then(m => m.VehicleTypesModule)
  },
  {
    path: 'routes',
    loadChildren: () => import('./routes/routes.module').then(m => m.RoutesModule)
  },
  {
    path: 'vehicle',
    loadChildren: () => import('./vehicle/vehicle.module').then(m => m.VehicleModule)
  },
  {
    path: 'client',
    loadChildren: () => import('./client/client.module').then(m => m.ClientModule)
  },
  {
    path: 'product',
    loadChildren: () => import('./product/product.module').then(m => m.ProductModule)
  },
  {
    path: 'supplier',
    loadChildren: () => import('./supplier/supplier.module').then(m => m.SupplierModule)
  },
  {
    path: 'wharehouse',
    loadChildren: () => import('./wharehouse/wharehouse.module').then(m => m.WharehouseModule)
  },
  {
    path: 'product-category',
    loadChildren: () => import('./product-category/product-category.module').then(m => m.ProductCategoryModule)
  },
  {
    path: 'product-sub-category',
    loadChildren: () => import('./product-sub-category/product-sub-category.module').then(m => m.ProductSubCategoryModule)
  },
  {
    path: 'product-sub-category',
    loadChildren: () => import('./product-sub-category/product-sub-category.module').then(m => m.ProductSubCategoryModule)
  },
  {
    path: 'code',
    loadChildren: () => import('./code/code.module').then(m => m.CodeModule)
  }

];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes),
    FuseSharedModule
  ]
})
export class AdminModule { }
