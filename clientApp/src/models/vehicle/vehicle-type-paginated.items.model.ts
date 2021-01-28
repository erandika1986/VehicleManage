import { Injectable } from '@angular/core';
import { PaginatedItemsModel } from '../common/paginated.items.model';
import { VehicleTypeModel } from './vehicle-type.model';

@Injectable()
export class VehicleTypePaginatedItemsModel extends PaginatedItemsModel {

    data: VehicleTypeModel[]=[];
}
