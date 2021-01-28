import { Injectable } from '@angular/core';
import { PaginatedItemsModel } from '../common/paginated.items.model';
import { VehicleRevenueLicenceModel } from './vehicle-revenue-licence.model';

@Injectable()
export class VehicleRLPaginatedItemsModel extends PaginatedItemsModel {

    data: VehicleRevenueLicenceModel[]=[];
}
