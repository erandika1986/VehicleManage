import { Injectable } from '@angular/core';
import { PaginatedItemsModel } from '../common/paginated.items.model';
import { VehicleAirCleanerModel } from './vehicle-air-cleaner.model';

@Injectable()
export class VehicleACRMPaginatedItemsModel extends PaginatedItemsModel {

    data: VehicleAirCleanerModel[];
}
