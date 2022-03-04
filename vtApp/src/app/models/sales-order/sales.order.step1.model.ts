import { Injectable } from "@angular/core";

@Injectable()
export class SalesOrderStep1Model {
    id :number;
    
    orderDate:Date;
    orderNumber:string;
    orderDateYear:number;
    orderDateMonth:number;
    orderDateDay:number;
    orderDateHour:number;
    orderDateMin:number;

    deliverDate?:Date;
    deliverDateYear:number;
    deliverDateMonth:number;
    deliverDateDay:number;
    deliverDateHour:number;
    deliverDateMin:number;

    ownerId :number;
    routeId:number;
    status :number;
}