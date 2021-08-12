import { Injectable } from "@angular/core";
import { DropDownModel } from "../common/drop-down.modal";

@Injectable()
export class InventoryMasterDataModel {
    suppliers:DropDownModel[];
    warehouses:DropDownModel[];
    productCategories:DropDownModel[];
}