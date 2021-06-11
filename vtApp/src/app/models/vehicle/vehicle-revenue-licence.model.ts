import { VehicleCommonModel } from './vehicle-common.model';

export class VehicleRevenueLicenceModel extends VehicleCommonModel {
    revenueLicenceDate: string;
    validTill: string;
    imageURL:string;
    imageName:string;

    revenueLicenceYear:number;
    revenueLicenceMonth:number;
    revenueLicenceDay:number;

    validTillYear:number;
    validTillMonth:number;
    validTillDay:number;

    //For Progress Bar
    isUploading:boolean;
}

export class VehicleRevenueLicenceReactiveForm {
    id: number;
    vehicleId: number;
    createdOn: string;
    createdBy: number;
    updatedOn: string;
    updatedBy: number;
    isActive: boolean;
    registrationNo:string;
    revenueLicenceDate:Date;
    validTillDate:Date;
}
