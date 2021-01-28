import { VehicleCommonModel } from './vehicle-common.model';

export class VehicleFitnessReportModel extends VehicleCommonModel {
    nextFitnessReportDate: Date=new Date();
    actualFitnessReportDate: Date= new Date();
}
