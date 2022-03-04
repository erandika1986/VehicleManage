import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductReturnListComponent } from './product-return-list/product-return-list.component';
import { RouterModule, Routes } from '@angular/router';
import { FuseSharedModule } from '@fuse/shared.module';
import { FuseSidebarModule, FuseWidgetModule } from '@fuse/components';
import { MaterialModule } from 'app/MaterialModule';
import { SharedModule } from 'app/shared/shared.module';
import { MainComponent } from './sidebars/main/main.component';
import { ProductReturnDetailComponent } from './product-return-detail/product-return-detail.component';
import { ProductReturnsComponent } from './product-returns.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'list',
    pathMatch: 'full'
  },
  {
    path: 'list',
    component: ProductReturnsComponent

  }
];

@NgModule({
  declarations: [
    ProductReturnListComponent,
    MainComponent,
    ProductReturnDetailComponent,
    ProductReturnsComponent
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
export class ProductReturnModule { }
