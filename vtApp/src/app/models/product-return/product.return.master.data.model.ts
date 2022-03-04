import { DropDownModel } from "../common/drop-down.modal";
import { Injectable } from "@angular/core";

@Injectable()
export class ProductReturnMasterDataModel
{
    productCategories:DropDownModel[];
    productReturnStatus:DropDownModel[];
    productReturnReasonCodes:DropDownModel[];
}