import { VehicleCommonModel } from './vehicle-common.model';

export class VehicleInsuranceModel extends VehicleCommonModel {
    nextInsuranceDate: Date;
    actualInsuranceDate: Date;
    imageURL:string;
}
