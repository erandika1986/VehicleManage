import { Injectable } from "@angular/core";

@Injectable()
export class SalesOrderItemModel {
    id :number;
    salesOrderId :number;
    productId :number;
    qty :number;
    unitPrice :number;
    total :number;
}