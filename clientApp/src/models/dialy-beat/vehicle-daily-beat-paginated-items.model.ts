import { Injectable } from '@angular/core';
import { PaginatedItemsModel } from '../common/paginated.items.model';
import { DailyVehicleBeatModel } from './daily-vehicle-beat.model';

@Injectable()
export class VehicleDailyBeatPaginatedItemsModel extends PaginatedItemsModel {

    data: DailyVehicleBeatModel[];
}