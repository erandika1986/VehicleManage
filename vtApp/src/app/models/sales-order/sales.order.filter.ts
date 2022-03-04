import { Injectable } from "@angular/core";

@Injectable()
export class SalesOrderFilter {
    selectedStatus :number;
    selectedRouteId:number;
    selectedCustomerId:number;
    selectedSalesPersonId:number;
}