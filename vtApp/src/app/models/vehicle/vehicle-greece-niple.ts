import { VehicleCommonModel } from './vehicle-common.model';

export class VehicleGreeceNipleModel extends VehicleCommonModel {
    greeceNipleReplaceDate: string;
    nextGreeceNipleReplaceDate: string;

    greeceNipleReplacYear:number;
    greeceNipleReplacMonth:number;
    greeceNipleReplacDay:number;

    nextGreeceNipleReplaceYear:number;
    nextGreeceNipleReplaceMonth:number;
    nextGreeceNipleReplaceDay:number;
}


export class VehicleGreeceNipleReactiveForm {
    id: number;
    vehicleId: number;
    createdOn: string;
    createdBy: number;
    updatedOn: string;
    updatedBy: number;
    isActive: boolean;
    registrationNo:string;
    greeceNipleReplaceDate:Date;
    nextGreeceNipleReplaceDate:Date;
}