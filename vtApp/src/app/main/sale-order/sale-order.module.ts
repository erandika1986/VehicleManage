import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SaleOrderListComponent } from './sale-order-list/sale-order-list.component';
import { SaleOrderDetailComponent } from './sale-order-detail/sale-order-detail.component';
import { RouterModule, Routes } from '@angular/router';
import { FuseSharedModule } from '@fuse/shared.module';
import { FuseSidebarModule, FuseWidgetModule } from '@fuse/components';
import { MaterialModule } from 'app/MaterialModule';
import { SharedModule } from 'app/shared/shared.module';
import { MainComponent } from './sidebars/main/main.component';
import { SaleOrdersComponent } from './sale-orders.component';
import { ProductSearchComponent } from './product-search/product-search.component';
import { ProductAvailabiltyComponent } from './product-availabilty/product-availabilty.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'list',
    pathMatch: 'full'
  },
  {
    path: 'list',
    component: SaleOrdersComponent

  },
  {
    path: 'list/:id',
    component: SaleOrderDetailComponent,
  }
];

@NgModule({
  declarations: [
    SaleOrderListComponent,
    SaleOrderDetailComponent,
    MainComponent,
    SaleOrdersComponent,
    ProductSearchComponent,
    ProductAvailabiltyComponent
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
export class SaleOrderModule { }
