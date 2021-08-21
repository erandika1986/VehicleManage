import { Injectable } from "@angular/core";

@Injectable()
export class SalesOrderFilter {
    selectedStatus :number;
    selectedCustomerId:number;
    selectedSalesPersonId:number;
}