import { Injectable } from "@angular/core";

@Injectable()
export class InventoryBasicDetailModel {
    productImage:string;
    productId:number;
    categoryName:string;
    subCategoryName:string;
    productName:string;
    supplierName:string;
    qtyInHand:number;
    totalItemRecieved:number;
    totalItemReturn:number;
}