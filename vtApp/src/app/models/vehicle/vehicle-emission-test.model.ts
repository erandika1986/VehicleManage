import { VehicleCommonModel } from './vehicle-common.model';

export class VehicleEmissionTestModel extends VehicleCommonModel {
    emissiontTestDate: string;
    validTill: string;
    imageURL:string;
    imageName:string;

    emissionTestYear:number;
    emissionTestMonth:number;
    emissionTestDay:number;

    validTillYear:number;
    validTillMonth:number;
    validTillDay:number;

    //For Progress Bar
    isUploading:boolean;
}

export class VehicleEmissionTestReactiveForm {
    id: number;
    vehicleId: number;
    createdOn: string;
    createdBy: number;
    updatedOn: string;
    updatedBy: number;
    isActive: boolean;
    registrationNo:string;
    emissiontTestDate:Date;
    validTillDate:Date;
}
