import { Injectable } from "@angular/core";
import { InventoryModel } from "./inventory.model";

@Injectable()
export class POInventoryReceievedDetailModel {

    id:number;
    warehouseId:number;
    puchaseOrderId:number;
    inventories:InventoryModel[];
}