import { VehicleCommonModel } from './vehicle-common.model';

export class VehicleInsuranceModel extends VehicleCommonModel {
    nextInsuranceDate: Date=new Date();
    actualInsuranceDate: Date= new Date();
}
