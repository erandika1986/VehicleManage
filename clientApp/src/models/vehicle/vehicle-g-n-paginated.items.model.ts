import { Injectable } from '@angular/core';
import { PaginatedItemsModel } from '../common/paginated.items.model';
import { VehicleGreeceNipleModel } from './vehicle-greece-niple';

@Injectable()
export class VehicleGNPaginatedItemsModel extends PaginatedItemsModel {

    data: VehicleGreeceNipleModel[]=[];
}
