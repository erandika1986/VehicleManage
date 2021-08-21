import { Injectable } from "@angular/core";

@Injectable()
export class BasicSalesOrderDetailModel {
    Id:number;
    orderNumber:string;
    total:number;
    totalQty:number;
    status:string;
    orderDate :string;
    ownderName :string;
    ownerAddress :string;
    route :string;
    createdBy :string;
    createdOn :string;
    updatedBy :string;
    updatedOn :string;
}