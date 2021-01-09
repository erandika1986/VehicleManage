import { Injectable } from '@angular/core';
import { PaginatedItemsModel } from '../common/paginated.items.model';
import { VehicleEmissionTestModel } from './vehicle-emission-test.model';

@Injectable()
export class VehicleETPaginatedItemsModel extends PaginatedItemsModel {

    data: VehicleEmissionTestModel[];
}
