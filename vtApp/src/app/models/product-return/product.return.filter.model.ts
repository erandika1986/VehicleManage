import { Injectable } from "@angular/core";

@Injectable()
export class ProductReturnFilterModel
{
    selectedProductCategoryId:number;
    selectedProductSubCategoryId:number;
    selectedProductId:number;
    selectedClientId:number;
    selectedWarehouseId:number;
    selectedProductReturnStatus:number;

    currentPage:number;
    pageSize:number;

}