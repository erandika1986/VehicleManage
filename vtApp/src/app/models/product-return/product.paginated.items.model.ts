import { Injectable } from "@angular/core";
import { PaginatedItemsModel } from "../common/paginated.items.model";
import { BasicProductReturnModel } from "./basic.product.return.model";

@Injectable()
export class ProductPaginatedItemsModel extends PaginatedItemsModel {

    data: BasicProductReturnModel[];
}