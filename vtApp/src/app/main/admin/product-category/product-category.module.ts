import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { FuseSharedModule } from '@fuse/shared.module';
import { FuseWidgetModule } from '@fuse/components';
import { SharedModule } from 'app/shared/shared.module';
import { ProductCategoryListComponent } from './product-category-list/product-category-list.component';
import { ProductCategoryDetailComponent } from './product-category-detail/product-category-detail.component';
import { MaterialModule } from 'app/MaterialModule';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'list',
    pathMatch: 'full'
  },
  {
    path: 'list',
    component: ProductCategoryListComponent

  }
];

@NgModule({
  declarations: [ProductCategoryListComponent, ProductCategoryDetailComponent],
  imports: [
    RouterModule.forChild(routes),
    CommonModule,
    FuseSharedModule,
    FuseWidgetModule,
    SharedModule,
    MaterialModule
  ]
})
export class ProductCategoryModule { }
