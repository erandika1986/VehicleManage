import { Injectable } from "@angular/core";

@Injectable()
export class BasicProductReturnModel
{
    id:number;
    selectedProduct:string;
    selectedClient:string;
    selectedSaleOrder:string;
    qty:number;
    returnDate:Date;
    status:string;
    reasonCode:string;
    reason:string;

    createdOn:string;
    createdByUser:string;
    updatedOn:string;
    updatedByUser:string;
}