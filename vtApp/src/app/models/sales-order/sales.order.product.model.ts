import { Injectable } from "@angular/core";

@Injectable()
export class SalesOrderProductModel {
    salesOrderId :number;
    warehouseId:number;
    productId:number;
    productName:string;
    categoryName:string;
    subCategoryName:string;
    untiPrice:number;
    qty :number;
}