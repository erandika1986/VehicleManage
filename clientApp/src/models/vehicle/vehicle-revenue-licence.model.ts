import { VehicleCommonModel } from './vehicle-common.model';

export class VehicleRevenueLicenceModel extends VehicleCommonModel {
    nextRevenueLicenceDate: Date= new Date();
    actualRevenueLicenceDate: Date= new Date();
}
