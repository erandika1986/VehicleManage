import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductReturnListComponent } from './product-return-list/product-return-list.component';
import { RouterModule, Routes } from '@angular/router';
import { FuseSharedModule } from '@fuse/shared.module';
import { FuseSidebarModule, FuseWidgetModule } from '@fuse/components';
import { MaterialModule } from 'app/MaterialModule';
import { SharedModule } from 'app/shared/shared.module';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'list',
    pathMatch: 'full'
  },
  {
    path: 'list',
    component: ProductReturnListComponent

  }
];

@NgModule({
  declarations: [
    ProductReturnListComponent
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
