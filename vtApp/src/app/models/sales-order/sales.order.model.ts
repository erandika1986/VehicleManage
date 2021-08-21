import { Injectable } from "@angular/core";
import { SalesOrderItemModel } from "./sales.order.item.model";

@Injectable()
export class SalesOrderModel {
    id :number;
    orderNumber:string;
    orderDate:Date;
    deliverDate:Date;
    ownerId :number;
    status :number;
    subTotal :number;
    discount :number;
    taxRate :number;
    totalTaxAmount :number;
    shippingCharge :number;
    totalAmount :number;
    isActive :boolean
    items:SalesOrderItemModel[];
}