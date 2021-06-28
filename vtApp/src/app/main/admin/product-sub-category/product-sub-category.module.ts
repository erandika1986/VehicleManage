import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { FuseSharedModule } from '@fuse/shared.module';
import { FuseWidgetModule } from '@fuse/components';
import { SharedModule } from 'app/shared/shared.module';
import { ProductSubCategoryDetailComponent } from './product-sub-category-detail/product-sub-category-detail.component';
import { ProductSubCategoryListComponent } from './product-sub-category-list/product-sub-category-list.component';
import { MaterialModule } from 'app/MaterialModule';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'list',
    pathMatch: 'full'
  },
  {
    path: 'list',
    component: ProductSubCategoryListComponent

  }
];

@NgModule({
  declarations: [ProductSubCategoryDetailComponent, ProductSubCategoryListComponent],
  imports: [
    RouterModule.forChild(routes),
    CommonModule,
    FuseSharedModule,
    FuseWidgetModule,
    SharedModule,
    MaterialModule
  ]
})
export class ProductSubCategoryModule { }
