import { Injectable } from "@angular/core";

@Injectable()
export class SalesOrderStep1Model {
    id :number;
    orderNumber:string;
    orderDate:Date;
    deliverDate:Date;
    ownerId :number;
    status :number;
}