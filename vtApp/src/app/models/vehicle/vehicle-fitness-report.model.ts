import { VehicleCommonModel } from './vehicle-common.model';

export class VehicleFitnessReportModel extends VehicleCommonModel {
    fitnessReportDate: string;
    validTill: string;
    imageURL:string;
    imageName:string;

    fitnessReportYear:number;
    fitnessReportMonth:number;
    fitnessReportDay:number;

    validTillYear:number;
    validTillMonth:number;
    validTillDay:number;

    //For Progress Bar
    isUploading:boolean;
}

export class VehicleFitnessReportReactiveForm {
    id: number;
    vehicleId: number;
    createdOn: string;
    createdBy: number;
    updatedOn: string;
    updatedBy: number;
    isActive: boolean;
    registrationNo:string;
    fitnessReportDate:Date;
    validTillDate:Date;
}
