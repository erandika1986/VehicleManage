import { Injectable } from '@angular/core';
import { PaginatedItemsModel } from '../common/paginated.items.model';
import { VehicleGearBoxOilMilageModel } from './vehicle-gear-box-oil-milage.model';

@Injectable()
export class VehicleGBOCMPaginatedItemsModel extends PaginatedItemsModel {

    data: VehicleGearBoxOilMilageModel[]=[];
}
