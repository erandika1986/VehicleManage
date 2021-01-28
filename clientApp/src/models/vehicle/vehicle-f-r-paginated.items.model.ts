import { Injectable } from '@angular/core';
import { PaginatedItemsModel } from '../common/paginated.items.model';
import { VehicleFitnessReportModel } from './vehicle-fitness-report.model';

@Injectable()
export class VehicleFRPaginatedItemsModel extends PaginatedItemsModel {

    data: VehicleFitnessReportModel[]=[];
}
