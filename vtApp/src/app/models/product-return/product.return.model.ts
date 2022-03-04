import { Injectable } from "@angular/core";

@Injectable()
export class ProductReturnModel
{
    id:number;
    selectedProductId:number;
    selectedClientId:number;
    selectedSaleOrderId:number;
    selectedWarehouseId:number;
    qty:number;
    batchNo:string;
    dateOfManufacture?:Date;
    dateOfExpiration?:Date;
    returnDate:Date;
    status:number;
    reasonCode:number;
    reason:string;

    createdOn:string;
    createdByUser:string;
    updatedOn:string;
    updatedByUser:string;
}