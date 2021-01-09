import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UsersComponent } from './users/users.component';
import { ProductSubCategoryComponent } from './product-sub-category/product-sub-category.component';
import { ProductComponent } from './product/product.component';
import { ProductCategoryComponent } from './product-category/product-category.component';
import { VendorComponent } from './vendor/vendor.component';
import { CustomerComponent } from './customer/customer.component';



@NgModule({
  declarations: [UsersComponent, ProductSubCategoryComponent, ProductComponent, ProductCategoryComponent, VendorComponent, CustomerComponent],
  imports: [
    CommonModule
  ]
})
export class AdminModule { }
