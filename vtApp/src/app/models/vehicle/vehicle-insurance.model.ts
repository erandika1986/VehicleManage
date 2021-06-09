import { VehicleCommonModel } from './vehicle-common.model';

export class VehicleInsuranceModel extends VehicleCommonModel {
    insuranceDate: string;
    validTill: string;
    imageURL:string;

    insuranceYear:number;
    insuranceMonth:number;
    insuranceDay:number;

    validTillYear:number;
    validTillMonth:number;
    validTillDay:number;
}
