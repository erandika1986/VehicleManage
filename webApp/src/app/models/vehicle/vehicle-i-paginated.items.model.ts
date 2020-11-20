import { Injectable } from '@angular/core';
import { PaginatedItemsModel } from '../common/paginated.items.model';
import { VehicleInsuranceModel } from './vehicle-insurance.model';

@Injectable()
export class VehicleIPaginatedItemsModel extends PaginatedItemsModel {

    data: VehicleInsuranceModel[];
}
