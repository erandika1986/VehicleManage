import { Injectable } from '@angular/core';
import { PaginatedItemsModel } from '../common/paginated.items.model';
import { VehicleEngineOilMilageModel } from './vehicle-engine-oil-milage.model';

@Injectable()
export class VehicleEOCMPaginatedItemsModel extends PaginatedItemsModel {

    data: VehicleEngineOilMilageModel[];
}
