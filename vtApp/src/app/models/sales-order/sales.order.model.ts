import { Injectable } from "@angular/core";
import { SalesOrderItemModel } from "./sales.order.item.model";

@Injectable()
export class SalesOrderModel {
    id :number;
    orderNumber:string;
    orderDate:Date;
    orderDateYear:number;
    orderDateMonth:number;
    orderDateDay:number;
    orderDateHour:number;
    orderDateMin:number;

    deliverDate:Date;
    deliverDateYear:number;
    deliverDateMonth:number;
    deliverDateDay:number;
    deliverDateHour:number;
    deliverDateMin:number;

    ownerId :number;
    routeId:number;
    status :number;
    subTotal :number;
    discount :number;
    taxRate :number;
    totalTaxAmount :number;
    shippingCharge :number;
    totalAmount :number;
    remarks:string;
    isActive :boolean
    items:SalesOrderItemModel[];
}