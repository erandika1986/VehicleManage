import { VehicleCommonModel } from './vehicle-common.model';

export class VehicleInsuranceModel extends VehicleCommonModel {
    insuranceDate: string;
    validTill: string;
    imageURL:string;
    imageName:string;

    insuranceYear:number;
    insuranceMonth:number;
    insuranceDay:number;

    validTillYear:number;
    validTillMonth:number;
    validTillDay:number;

    //For Progress Bar
    isUploading:boolean;
}


export class VehicleInsuranceReactiveForm {
    id: number;
    vehicleId: number;
    createdOn: string;
    createdBy: number;
    updatedOn: string;
    updatedBy: number;
    isActive: boolean;
    registrationNo:string;
    insuranceDate:Date;
    validTillDate:Date;
}