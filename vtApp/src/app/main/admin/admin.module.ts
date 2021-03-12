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
    path: 'engine-oil',
    loadChildren: () => import('./engine-oil/engine-oil.module').then(m => m.EngineOilModule)
  }
  ,
  {
    path: 'gear-box-oil',
    loadChildren: () => import('./gear-box-oil/gear-box-oil.module').then(m => m.GearBoxOilModule)
  },
  {
    path: 'break-oil',
    loadChildren: () => import('./break-oil/break-oil.module').then(m => m.BreakOilModule)
  },
  {
    path: 'differential-oil',
    loadChildren: () => import('./differential-oil/differential-oil.module').then(m => m.DifferentialOilModule)
  },
  {
    path: 'engine-coolant',
    loadChildren: () => import('./engine-coolant/engine-coolant.module').then(m => m.EngineCoolantModule)
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
