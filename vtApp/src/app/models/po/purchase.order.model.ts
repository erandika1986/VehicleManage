import { PurchaseOrderItem } from "./purchase.order.item.model";

export class PurchaseOrder{
    id:number;
    date:Date;
    poNumber:string;
    selectedSupplierId:number;
    selectedWarehouseId:number;
    subTotal:number;
    discount:number;
    taxRate:number;
    totalTaxAmout:number;
    shippingCharge:number;
    total:number;
    status:number;
    remarks:number;

    createdBy:string;
    createdOn:string;
    updatedBy:string;
    updatedOn:string;

    items:PurchaseOrderItem[];
}