import { Injectable } from '@angular/core';
import { PaginatedItemsModel } from '../common/paginated.items.model';
import { VehicleDifferentialOilChangeMilageModel } from './vehicle-differential-oil-change-milage.model';

@Injectable()
export class VehicleDOCMPaginatedItemsModel extends PaginatedItemsModel {

    data: VehicleDifferentialOilChangeMilageModel[];
}
