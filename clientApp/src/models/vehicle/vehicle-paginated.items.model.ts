import { Injectable } from '@angular/core';
import { PaginatedItemsModel } from '../common/paginated.items.model';
import { VehicleModel } from './vehicle.model';

@Injectable()
export class VehiclePaginatedItemsModel extends PaginatedItemsModel {

    data: VehicleModel[]=[];
}
