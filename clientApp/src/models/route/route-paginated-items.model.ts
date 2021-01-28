import { Injectable } from '@angular/core';
import { PaginatedItemsModel } from '../common/paginated.items.model';
import { RouteModel } from './route.model';

@Injectable()
export class RoutePaginatedItemsModel extends PaginatedItemsModel {

    data: RouteModel[]=[];
}