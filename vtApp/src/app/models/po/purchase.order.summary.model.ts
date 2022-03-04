export class PurchaseOrderSummary
{
    id:number;
    poNumber:string;
    supplierName:string;
    warehouseName:string;
    subTotal:number;
    discount:number;
    taxRate:number;
    totalTaxAmount:number;
    shippingCharges:number;
    total:number;
    status:number;
}