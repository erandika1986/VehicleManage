import { Injectable } from "@angular/core";

@Injectable()
export class BasicSalesOrderDetailModel {
    id:number;
    orderNumber:string;
    total:number;
    totalQty:number;
    status:string;
    orderDate :string;
    ownerName :string;
    ownerAddress :string;
    route :string;
    createdBy :string;
    createdOn :string;
    updatedBy :string;
    updatedOn :string;
}