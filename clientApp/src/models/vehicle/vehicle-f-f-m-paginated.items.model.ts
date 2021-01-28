import { Injectable } from '@angular/core';
import { PaginatedItemsModel } from '../common/paginated.items.model';
import { VehicleFuelFilterMilageModel } from './vehicle-fuel-filter-milage.model';

@Injectable()
export class VehicleFFMPaginatedItemsModel extends PaginatedItemsModel {

    data: VehicleFuelFilterMilageModel[]=[];
}
