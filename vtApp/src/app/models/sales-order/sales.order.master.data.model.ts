import { Injectable } from "@angular/core";
import { DropDownModel } from "../common/drop-down.modal";

@Injectable()
export class SalesOrderMasterDataModel {
    statuses :DropDownModel[];
    customers:DropDownModel[];
    salesPerson:DropDownModel[];
}