import { DropDownModel } from '../common/drop-down.modal';
import { Role } from './role.mode';

export class User {
    id: number;
    firstName: string;
    lastName: string;
    username: string;
    email: string;
    mobileNo: string;
    personalAddress:string;
    password: string;
    image:string;
    timeZoneId:number;
    isActive: boolean;
    nicno:string;
    nicFrontImage:string;
    nicBackImage:string;
    drivingLicenceNo:string;
    drivingLicenceFrontImage:string;
    drivingLicenceBackImage:string;
    roles: number[];
    roleText:string;

    imageName :string;
    nicFrontImageName :string;
    nicBackImageName :string;
    drivingLicenceFrontImageName :string;
    drivingLicenceBackImageName :string;
}