import { Injectable } from "@angular/core";

@Injectable()
export class SalesOrderStep3Model {
    id :number;
    subTotal :number;
    discount :number;
    taxRate :number;
    totalTaxAmount :number;
    shippingCharge :number;
    totalAmount :number;
    remarks:string;
}