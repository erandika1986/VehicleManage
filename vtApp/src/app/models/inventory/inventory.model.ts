import { Injectable } from "@angular/core";

@Injectable()
export class InventoryModel {
    id:number;
    dateRecieved:Date;
    dateOfManufacture:Date;
    dateOfExpiration:Date;
    productCategoryName:string;
    productSubCategoryName:string;
    supplierName:string;
    productName:string;
    productId:number;
    totalOrderedQty:number;
    recievedQty:number;
    isActive:boolean;
}