import { Injectable } from "@angular/core";
import { SalesOrderItemModel } from "./sales.order.item.model";

@Injectable()
export class ProductAvailabilityModel {
    productId :number;
    productName:string;
    warehouseId :number;
    warhouseName:string;
    availableQty:number;
    unitPrice:number;
    orderedQty:number;
    newlyAddedQty:number;
}